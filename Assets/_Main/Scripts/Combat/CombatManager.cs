using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            if (PlayerPrefs.GetInt("BonusCombat") == 0) //Normal
            {
                players[i].SetupBonusData(1, 1);
            }
            else if (PlayerPrefs.GetInt("BonusCombat") == 1) //Advantage
            {
                players[i].SetupBonusData(1, 1.5f);
            }
            else //Ambush
            {
                players[i].SetupBonusData(1, 0.5f);
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

    IEnumerator EnemyTurn(Character enemy)
    {
        yield return new WaitForSeconds(1f);

        if (players.Count > 0)
        {
            Character target = players[Random.Range(0, players.Count)];
            enemy.Attack(target);
        }

        turnIndex++;
        NextTurn();
    }

    public void PlayerPrepareAction(string action, bool isMultipleTarget)
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
                    player.Attack(target);
                }
                break;
            case "Spell":
                foreach (Character target in targets)
                {
                    player.CastSpell(target);
                }
                break;
            case "Defend":
                player.Defend();
                break;
            case "Run":
                uiManager.ShowGameOver("You ran away!");
                return;
        }

        turnIndex++;
        uiManager.HideActionPanel();
        NextTurn();
    }

    bool CheckVictory()
    {
        if (enemies.TrueForAll(e => e.isDead))
        {
            uiManager.ShowGameOver("You won!");
            return true;
        }
        else if (players.TrueForAll(p => p.isDead))
        {
            uiManager.ShowGameOver("You lost...");
            return true;
        }
        return false;
    }
}
