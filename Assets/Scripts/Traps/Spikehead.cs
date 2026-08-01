using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTime;
    private Vector3 destination;

    private bool attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip spikeheadSound;

    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Debug.Log($"{name} enabled", this);
        Stop();
    }
    

    private void Update()
    {
        // move spikehead inly if attacking
        if(attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTime += Time.deltaTime;
            if(checkTime >= checkDelay)
            {
                checkTime = 0;
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        
        // check if spikehead sees player
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null)
            {
                attacking = true;
                destination = directions[i];
                checkTime = 0;
            }
        }
        
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        base.OnTriggerEnter2D(collision);
        // stop after hitting something
        SoundManager.instance.PlaySound(spikeheadSound);
        Stop();
    }
}
