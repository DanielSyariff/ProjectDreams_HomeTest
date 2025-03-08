using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHP;
    public int currentHP;
    public int speed;
    public int damage;
    public Animator animator;

    public bool isDead => currentHP <= 0;

    public void SetupData(string enemyName, int hp, int spd, int dmg, RuntimeAnimatorController anim)
    {
        characterName = enemyName;
        maxHP = hp;
        currentHP = maxHP;
        damage = dmg;
        speed = spd;
        animator.runtimeAnimatorController = anim;
    }

    public void SetupBonusData(float dmgMultiplier, float spdMultiplier)
    {
        damage = Mathf.RoundToInt(damage * Mathf.Pow(dmgMultiplier, 2));
        speed = Mathf.RoundToInt(speed * Mathf.Pow(spdMultiplier, 2));

        Debug.Log("Bonus Applied! New Damage: " + damage + ", New Speed: " + speed);
    }


    public void Attack(Character target)
    {
        damage = Random.Range(5, damage);
        target.TakeDamage(damage);
        Debug.Log($"{characterName} attacked {target.characterName} for {damage} damage!");
    }

    public void CastSpell(Character target)
    {
        damage = Random.Range(10, damage);
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
            this.gameObject.SetActive(false);
        }
    }
}
