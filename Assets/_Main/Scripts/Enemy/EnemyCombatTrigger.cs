using UnityEngine;

public class EnemyCombatTrigger : MonoBehaviour
{
    public float normalCombatSpeed = 1f;
    public float advantageCombatSpeed = 1.5f;
    public float ambushCombatSpeed = 0.5f;

    public int initialAdvantageDamage = 10;
    public int initialAmbushDamage = 10;

    public void TriggerCombat(Transform player)
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector2.Dot(transform.right, directionToPlayer);

        if (angle > 0.5f)
        {
            NormalCombat();
        }
        else if (angle < -0.5f)
        {
            AdvantageCombat();
        }
        else
        {
            AmbushCombat();
        }
    }

    private void NormalCombat()
    {
        Debug.Log("Normal Combat Triggered! ➜ Speed: " + normalCombatSpeed);
    }

    private void AdvantageCombat()
    {
        Debug.Log("Advantage Combat Triggered! ➜ Speed: " + advantageCombatSpeed + ", Initial Damage: " + initialAdvantageDamage);
    }

    private void AmbushCombat()
    {
        Debug.Log("Ambush Combat Triggered! ➜ Speed: " + ambushCombatSpeed + ", Initial Damage from Enemy: " + initialAmbushDamage);
    }
}
