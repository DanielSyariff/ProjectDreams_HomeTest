using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform spawnArea;
    public float patrolRange = 5f;
    public float patrolSpeed = 2f;
    public float waitTime = 2f;

    [Header("Detection Settings")]
    public float visionRange = 5f;
    public float attackRange = 1f;
    public LayerMask playerLayer;

    [Header("Attack Settings")]
    public float attackCooldown = 1f;
    public int attackDamage = 10;

    private Transform player;
    private Vector2 targetPoint;
    private float attackTimer;
    private float waitTimer;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // Dapatkan SpriteRenderer
        SetNewPatrolPoint();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (PlayerInRange(visionRange))
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    #region Patrol Logic
    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

        // Flip arah saat patrol
        FlipDirection(targetPoint - (Vector2)transform.position);

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                SetNewPatrolPoint();
                waitTimer = 0f;
            }
        }
    }

    private void SetNewPatrolPoint()
    {
        if (spawnArea != null)
        {
            Bounds bounds = spawnArea.GetComponent<Collider2D>().bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            targetPoint = new Vector2(x, y);
        }
        else
        {
            targetPoint = transform.position; // Kalau ga ada area, diem aja
        }
    }
    #endregion

    #region Chase Logic
    private void ChasePlayer()
    {
        if (player == null) return;

        if (!WorldUIManager.instance.onPause)
        {
            // Kejar Player
            transform.position = Vector2.MoveTowards(transform.position, player.position, patrolSpeed * 1.5f * Time.deltaTime);

            // Flip arah saat ngejar Player
            FlipDirection(player.position - transform.position);

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
    }
    #endregion

    #region Attack Logic
    private void AttackPlayer()
    {
        if (attackTimer <= 0)
        {
            // Trigger Combat Mode
            EnemyCombatTrigger combatTrigger = GetComponent<EnemyCombatTrigger>();
            if (combatTrigger != null)
            {
                combatTrigger.TriggerCombat(this.transform);
            }

            Debug.Log("Enemy attacked the player!");
            attackTimer = attackCooldown;
        }
    }
    #endregion

    #region Detection Logic
    private bool PlayerInRange(float range)
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (playerCollider != null)
        {
            player = playerCollider.transform;
            return true;
        }
        else
        {
            player = null;
            return false;
        }
    }
    #endregion

    #region Flip Logic
    private void FlipDirection(Vector2 direction)
    {
        // Kalau ke kanan, flipX = false, kalau ke kiri flipX = true
        spriteRenderer.flipX = direction.x < 0;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (spawnArea != null)
        {
            Gizmos.DrawWireCube(spawnArea.position, spawnArea.GetComponent<Collider2D>().bounds.size);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
