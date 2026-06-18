using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prevRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;


void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.CompareTag("Player"))
    {
        if (collision.transform.position.x < transform.position.x)
        {
            cam.MoveToNewRoom(nextRoom);
        }
        else
        {
            cam.MoveToNewRoom(prevRoom);
        }
    }
}
}