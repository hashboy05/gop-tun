using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.PlayerTakeDmg(10);
            Destroy(gameObject);
        }
    }
}