using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // private void Update()
    // {
    //     if(hit) return;
    //     float movementSpeed = speed * Time.deltaTime*direction;
    //     transform.Translate(movementSpeed, 0, 0);
    // }

    private void Update()
{
    if(hit) return;

    transform.position += Vector3.right * direction * 5f * Time.deltaTime;

    lifetime+= Time.deltaTime;
    if(lifetime>5) gameObject.SetActive(false);
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
  

    public void SetDirection(float _direction)
    {
        lifetime=0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        anim.ResetTrigger("explode");
        anim.Play("Idle");

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
