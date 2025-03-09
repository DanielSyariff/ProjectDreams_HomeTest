using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CombatManager : MonoBehaviour
{
    [Header("Combat Settings")]
    public EnemyLineupData lineUpData;
    public List<Character> players;
    public List<Character> enemies;

    public UIManager uiManager;

    private int turnIndex = 0;
    private List<Character> turnOrder;

    void Start()
    {
        AudioManager.instance.BattleMusic();
        StartCombat();
    }

    void StartCombat()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetupData(lineUpData.lineup[i].name, lineUpData.lineup[i].maxHp, lineUpData.lineup[i].speed, lineUpData.lineup[i].damage, lineUpData.lineup[i].animator);
        }

        Debug.Log("Bonus Combat : " + PlayerPrefs.GetInt("BonusCombat"));

        for (int i = 0; i < players.Count; i++)
        {
            if (PlayerPrefs.GetInt("BonusCombat") == 0) // Normal
            {
                players[i].SetupBonusData(1, 1);
            }
            else if (PlayerPrefs.GetInt("BonusCombat") == 1) // Advantage
            {
                players[i].SetupBonusData(1, 1.5f);
            }
            else // Ambush
            {
                players[i].SetupBonusData(1, 0.5f);
                uiManager.HideActionPanel();
            }
        }

        turnOrder = new List<Character>();
        turnOrder.AddRange(players);
        turnOrder.AddRange(enemies);
        turnOrder.Sort((a, b) => b.speed.CompareTo(a.speed));

        uiManager.HideGameOver();
        NextTurn();
    }

    void NextTurn()
    {
        if (CheckVictory()) return;

        if (turnIndex >= turnOrder.Count) turnIndex = 0;

        Character currentCharacter = turnOrder[turnIndex];

        if (currentCharacter.isDead)
        {
            turnIndex++;
            NextTurn();
            return;
        }

        if (players.Contains(currentCharacter))
        {
            uiManager.ShowActionPanel();
        }
        else
        {
            StartCoroutine(EnemyTurn(currentCharacter));
        }
    }

    IEnumerator BackToExploringScene()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.WorldMusic();
        uiManager.GoToExploring();
    }

    IEnumerator EnemyTurn(Character enemy)
    {
        yield return new WaitForSeconds(1f);

        if (players.Count > 0)
        {
            Character target = players[Random.Range(0, players.Count)];
            MoveToPlayerAndAttack(enemy, target); // Panggil fungsi gerakan ke player
        }
        else
        {
            turnIndex++;
            NextTurn();
        }
    }

    void MoveToPlayerAndAttack(Character enemy, Character target)
    {
        Vector3 initialPosition = enemy.transform.position;

        // Hitung arah maju ke player
        Vector3 directionToPlayer = (target.transform.position - enemy.transform.position).normalized;
        Vector3 attackPosition = target.transform.position - directionToPlayer * 1.5f;

        // Gerakkan enemy ke posisi serang
        enemy.transform.DOMove(attackPosition, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            enemy.Attack(target);

            // Kembali ke posisi awal setelah menyerang
            enemy.transform.DOMove(initialPosition, 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                turnIndex++;
                NextTurn();
            });
        });
    }


    public void PlayerPrepareAction(bool isMultipleTarget)
    {
        Character player = turnOrder[turnIndex];

        uiManager.HideActionPanel();
        if (isMultipleTarget)
        {
            uiManager.ShowTargetSelection(enemies, true);
        }
        else
        {
            uiManager.ShowTargetSelection(enemies, false);
        }
    }

    public void PlayerAction(string action, List<Character> targets)
    {
        Character player = turnOrder[turnIndex];

        if (targets == null || targets.Count == 0) return;

        switch (action)
        {
            case "Attack":
                foreach (Character target in targets)
                {
                    MoveToEnemyAndAttack(player, target);
                }
                break;
            case "Spell":
                foreach (Character target in targets)
                {
                    player.transform.DOShakePosition(0.5f, 0.3f, 10, 90, false, true).OnComplete(() =>
                    {
                        player.CastSpell(target);
                    });
                }

                turnIndex++;
                uiManager.HideActionPanel();
                NextTurn();

                break;
            case "Defend":
                player.Defend();
                turnIndex++;
                uiManager.HideActionPanel();
                NextTurn();
                break;
            case "Run":
                uiManager.ShowGameOver("You ran away!");
                StartCoroutine(BackToExploringScene());
                return;
        }
    }

    void MoveToEnemyAndAttack(Character player, Character target)
    {
        Vector3 initialPosition = player.transform.position;
        Vector3 attackPosition = target.transform.position + (player.transform.position - target.transform.position).normalized * 1.5f;

        player.transform.DOMove(attackPosition, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            player.Attack(target);

            player.transform.DOMove(initialPosition, 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                turnIndex++;
                uiManager.HideActionPanel();
                NextTurn();
            });
        });
    }

    void MoveToEnemyAndCastSpell(Character player, Character target)
    {
        // Efek bergetar saat cast spell
        
    }


    bool CheckVictory()
    {
        if (enemies.TrueForAll(e => e.isDead))
        {
            uiManager.ShowGameOver("You won!");
            StartCoroutine(BackToExploringScene());
            return true;
        }
        else if (players.TrueForAll(p => p.isDead))
        {
            uiManager.ShowGameOver("You lost...");
            uiManager.ShowLosePanelButton();
            return true;
        }
        return false;
    }
}
