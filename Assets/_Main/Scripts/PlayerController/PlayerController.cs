using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Settings
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    #endregion

    #region Attack Settings
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask enemyLayer;

    public EnemyCombatTrigger enemyTrigger;

    [Header("Attack Key")]
    [SerializeField] private KeyCode attackKey = KeyCode.J;
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
        HandleAttackInput();
        UpdateAttackPointPosition();
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

    #region Attack Logic
    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(attackKey))
        {
            PerformAttack();
        }
    }

    private void UpdateAttackPointPosition()
    {
        if (movementInput != Vector2.zero)
        {
            Vector2 direction = movementInput.normalized;

            attackPoint.localPosition = new Vector2(direction.x, direction.y) * 1f; // Atur jarak offset
        }
    }




    private void PerformAttack()
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);

        if (enemyCollider != null)
        {
            EnemyCombatTrigger enemyTrigger = enemyCollider.GetComponent<EnemyCombatTrigger>();

            if (enemyTrigger != null)
            {
                Debug.Log("Player Attacked Enemy!");
                enemyTrigger.TriggerCombat(transform);
            }
            else
            {
                Debug.Log("Enemy hit, but no EnemyCombatTrigger found.");
            }
        }
        else
        {
            Debug.Log("Attack missed! No enemy in range.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion
}
