using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombatTrigger : MonoBehaviour
{
    public WorldUIManager worldUIManager;
    [Header("Combat Speed Settings")]
    public float normalCombatSpeed = 1f;
    public float advantageCombatSpeed = 1.5f;
    public float ambushCombatSpeed = 0.5f;

    [Header("Initial Damage Settings")]
    public int initialAdvantageDamage = 10;
    public int initialAmbushDamage = 10;

    private Transform player;

    public void TriggerCombat(Transform attacker)
    {
        Vector2 directionToAttacker = (attacker.position - transform.position).normalized;

        // Cek apakah serangan datang dari Player layer
        if (attacker.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            float angleToAttacker = Vector2.Angle(transform.right, directionToAttacker);

            if (angleToAttacker < 45f)
            {
                NormalCombat(); // Serangan depan
            }
            else if (angleToAttacker > 135f)
            {
                AdvantageCombat(); // Serangan belakang
            }
            else
            {
                NormalCombat(); // Serangan dari samping
            }
        }
        else
        {
            // Kalau layer bukan Player, langsung Ambush
            AmbushCombat();
        }
    }


    private bool IsPlayerFacingEnemy(Transform player)
    {
        Vector2 playerForward = player.right.normalized;
        Vector2 directionToEnemy = (transform.position - player.position).normalized;

        float dotProduct = Vector2.Dot(playerForward, directionToEnemy);
        return dotProduct > 0.5f; // Menghadapi musuh
    }

    private void NormalCombat()
    {
        Debug.Log("Normal Combat Triggered! ➜ Speed: " + normalCombatSpeed);
        StartCoroutine(EnteringCombatMode("Normal Combat", 0));
    }

    private void AdvantageCombat()
    {
        Debug.Log("Advantage Combat Triggered! ➜ Speed: " + advantageCombatSpeed + ", Initial Damage: " + initialAdvantageDamage);
        StartCoroutine(EnteringCombatMode("Advantage Combat", 1));
    }

    private void AmbushCombat()
    {
        Debug.Log("Ambush Combat Triggered! ➜ Speed: " + ambushCombatSpeed + ", Initial Damage from Enemy: " + initialAmbushDamage);
        StartCoroutine(EnteringCombatMode("Ambush", 2));
    }
    private IEnumerator EnteringCombatMode(string text, int paramValue)
    {
        PlayerPrefs.SetInt("BonusCombat", paramValue);
        WorldUIManager.instance.ShowCombatTrigger(text);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CombatScene", LoadSceneMode.Single);
        Debug.Log("Entering Combat Mode");
    }
}
