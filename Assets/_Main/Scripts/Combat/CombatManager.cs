using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    [Header("Combat Settings")]
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
        // Urutkan turn berdasarkan speed
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

    IEnumerator<WaitForSeconds> EnemyTurn(Character enemy)
    {
        yield return new WaitForSeconds(1f);
        enemy.Attack(players[Random.Range(0, players.Count)]);
        turnIndex++;
        NextTurn();
    }

    public void PlayerAction(string action)
    {
        Character player = turnOrder[turnIndex];

        switch (action)
        {
            case "Attack":
                player.Attack(enemies[0]); // Serang musuh pertama
                break;
            case "Spell":
                player.CastSpell(enemies[0]);
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
