using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Bubble Settings")]
    public GameObject bubbleTextPrefab;             
    public Transform bubblePosition;               
    public float detectionRange = 3f;              
    public LayerMask playerLayer;                  
    public string npcMessage = "Hello! Press [E] to talk."; 

    [Header("Interaction Settings")]
    public bool canInteract = true;                

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
