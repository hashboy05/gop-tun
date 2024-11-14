using UnityEngine;

public class IllegalZone : MonoBehaviour
{
    // reduce points when enemy plane cross the player plane
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            Damage.ReduceDamage(100, collision.gameObject);
            ScoreManager.instance.DeductPoint(150);
        }
    }

}
