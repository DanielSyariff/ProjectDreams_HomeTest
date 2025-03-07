using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Settings
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
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
}
