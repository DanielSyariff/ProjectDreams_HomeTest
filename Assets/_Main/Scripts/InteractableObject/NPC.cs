using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Bubble Settings")]
    public GameObject bubbleTextPrefab;             // Prefab untuk Bubble Text
    public Transform bubblePosition;               // Posisi Bubble (biasanya di atas kepala NPC)
    public float detectionRange = 3f;              // Jarak deteksi player
    public LayerMask playerLayer;                  // Layer untuk player
    public string npcMessage = "Hello! Press [E] to talk."; // Pesan default NPC

    [Header("Interaction Settings")]
    public bool canInteract = true;                // Apakah NPC bisa diinteraksi

    private GameObject currentBubble;

    private void Update()
    {
        HandleBubble();

        if (PlayerInRange() && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    #region Bubble Logic
    private void HandleBubble()
    {
        bool playerNearby = PlayerInRange();

        if (playerNearby && currentBubble == null)
        {
            ShowBubble();
        }
        else if (!playerNearby && currentBubble != null)
        {
            HideBubble();
        }
    }

    private bool PlayerInRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        return playerCollider != null;
    }

    private void ShowBubble()
    {
        currentBubble = Instantiate(bubbleTextPrefab, bubblePosition.position, Quaternion.identity, transform);
        NPCBubbleUI bubbleUI = currentBubble.GetComponent<NPCBubbleUI>();
        if (bubbleUI != null)
        {
            bubbleUI.SetBubbleText(npcMessage);
        }
    }

    private void HideBubble()
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
            currentBubble = null;
        }
    }
    #endregion

    #region Interaction Logic
    public void Interact()
    {
        if (canInteract)
        {
            Debug.Log("NPC: Glad to see you, traveler!");
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
