using UnityEngine;

public class Power : MonoBehaviour
{
    // Reference to BulletPower component
    private BulletPower _bulletPower;

    private void Start()
    {
        // Find the BulletPower component in the bulletpower object by name
        GameObject bulletPower = GameObject.Find("BulletPower");
        if (bulletPower != null)
        {
            _bulletPower = bulletPower.GetComponent<BulletPower>();
        }
        else
        {
            Debug.LogWarning("BulletPower GameObject not found!");
        }
    }

    // Reduces enemy plane damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Checks the power type
            if (gameObject.name == "multiplier")
            {
                _bulletPower.adjustRate(1);
            }
            else if (gameObject.name == "healer")
            {
                GameManager.gameManager.ResetHealth();
            }
            else if (gameObject.name == "reload")
            {
                GunController.missileCount = 2;
            }
            Destroy(gameObject);
        }
    }
}