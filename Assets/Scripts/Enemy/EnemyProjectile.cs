using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }
    private void Update()
    {
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        hit = true;
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first

        if(anim != null)
            anim.SetTrigger("explode"); // when the object is fireball explode it
        else
            Deactivate(); // when this hits any object deactivate arrow
    }

    private void Deactivate()
    {
        boxCollider.enabled = false;
        gameObject.SetActive(false);
    }
}