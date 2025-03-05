using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Settings
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    #endregion

    #region Private Variables
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool isFacingRight = true;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleInteractionInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Movement Logic
    private void HandleMovementInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
    #endregion

    #region Interaction Logic
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, 1f, interactableLayer);

        foreach (Collider2D collider in interactables)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    #endregion
}
