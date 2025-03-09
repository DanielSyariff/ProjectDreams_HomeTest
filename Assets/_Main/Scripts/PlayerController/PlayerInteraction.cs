using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactionRadius = 1f;

    [Header("UI Settings")]
    [SerializeField] private GameObject interactionButtonPrefab; 

    private GameObject interactionButton;
    private Transform currentInteractable;

    private void Start()
    {
        if (interactionButtonPrefab != null)
        {
            interactionButton = Instantiate(interactionButtonPrefab, transform.position, Quaternion.identity);
            interactionButton.SetActive(false);
        }
    }

    private void Update()
    {
        HandleInteractionInput();
        CheckForInteractables();
    }

    #region Interaction Logic
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            IInteractable interactable = currentInteractable.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void CheckForInteractables()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayer);

        if (interactables.Length > 0)
        {
            currentInteractable = interactables[0].transform;
            ShowInteractionButton();
        }
        else
        {
            currentInteractable = null;
            HideInteractionButton();
        }
    }
    #endregion

    #region UI Management
    private void ShowInteractionButton()
    {
        if (interactionButton != null && currentInteractable != null)
        {
            interactionButton.SetActive(true);
            Vector3 buttonPosition = currentInteractable.position + Vector3.up * 1.5f;
            interactionButton.transform.position = buttonPosition;
        }
    }

    private void HideInteractionButton()
    {
        if (interactionButton != null)
        {
            interactionButton.SetActive(false);
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
