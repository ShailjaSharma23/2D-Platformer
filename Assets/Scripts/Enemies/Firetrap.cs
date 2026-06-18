using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{

    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        // StartCoroutine(ActivateFiretrap());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }


    private IEnumerator ActivateFiretrap()
    {
        spriteRend.color = Color.red;
        triggered = true;
        yield return new WaitForSeconds(activationDelay);

        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);

        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
