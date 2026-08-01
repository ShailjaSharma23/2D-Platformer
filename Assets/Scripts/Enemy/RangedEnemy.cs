using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Projectile Parameters")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound;


    // References
    private Animator anim;
    private float cooldownTimer;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();

    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // attack only if player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                anim.SetTrigger("rangedAttack");
                cooldownTimer = 0;
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }
    
    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        int fireballIdx = FindFireball();
        fireballs[fireballIdx].transform.position = firepoint.position;
        
        EnemyProjectile projectile = fireballs[fireballIdx].GetComponent<EnemyProjectile>();
        if (projectile != null)
        {
            projectile.SetDamage(damage);
        }
        projectile.ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, new Vector2(transform.localScale.x, 0), 0, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log(gameObject.name + " PlayerInSight detected: " + hit.collider.gameObject.name + " (Layer: " + hit.collider.gameObject.layer + ")");
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}
