using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHP;
    public int currentHP;
    public int speed;

    public bool isDead => currentHP <= 0;

    public void Attack(Character target)
    {
        int damage = Random.Range(5, 15);
        target.TakeDamage(damage);
        Debug.Log($"{characterName} attacked {target.characterName} for {damage} damage!");
    }

    public void CastSpell(Character target)
    {
        int damage = Random.Range(10, 20);
        target.TakeDamage(damage);
        Debug.Log($"{characterName} cast a spell on {target.characterName} for {damage} damage!");
    }

    public void Defend()
    {
        Debug.Log($"{characterName} is defending!");
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (isDead)
        {
            Debug.Log($"{characterName} has died.");
        }
    }
}
