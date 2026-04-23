using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Movement player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            player.SetGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            player.SetGrounded(false);
        }
    }
}