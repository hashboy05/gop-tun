using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [Header("Bullets(and Missiles)")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private TextMeshProUGUI msCount;
    public static int missileCount = 2;
    private float shootTimer = 0f; // Timer to track shooting interval
    public static float shootInterval = 2.5f; // Time interval for shooting downwards
    void Update()
    {
        // checks if the play button is pressed and the game is not paused
        if (GameManager.playPressed && !GameManager.isPaused)
        {
            // Get the mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;

            // Rotate the gun to face the mouse position
            gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

            if (transform.tag == "Player")
            {
                // Shoot bullets towards the north direction (upwards)
                Shoot();
            }
            else if (transform.tag == "Target")
            {
                // Update the shoot timer
                shootTimer += Time.deltaTime;
                // Check if enough time has passed to shoot downwards
                if (shootTimer >= shootInterval)
                {
                    GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
                    newBullet.name = bulletPrefab.name;
                    newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.down.normalized * bulletSpeed; // Change direction to Vector3.down for shooting downwards
                    Destroy(newBullet, 2);
                    shootTimer = 0f; // Reset the timer after shooting
                }
            }
            // Update the missile count on screen
            if (msCount != null)
            {
                msCount.text = missileCount + "/2";
            }
        }
    }

    public void Shoot()
    {
        // N key for bullets, M for missiles
        if (Input.GetKeyDown(KeyCode.N))
        {
            // checks if the bullet power is activated or not
            if (!GameManager.bulletPowerOn)
            {
                GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
                newBullet.name = bulletPrefab.name;
                newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * bulletSpeed;
                Destroy(newBullet, 1);
            }
            else
            {
                GameObject newBullet1 = Instantiate(bulletPrefab, gun.position + new Vector3(0.33f, -0.6f, 0), Quaternion.identity);
                GameObject newBullet2 = Instantiate(bulletPrefab, gun.position + new Vector3(-0.33f, -0.6f, 0), Quaternion.identity);
                newBullet1.name = bulletPrefab.name;
                newBullet2.name = bulletPrefab.name;
                newBullet1.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * bulletSpeed;
                newBullet2.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * bulletSpeed;
                Destroy(newBullet1, 1);
                Destroy(newBullet2, 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            // checks the missile count and changes the position
            if (missileCount == 2)
            {
                GameObject newBullet = Instantiate(missilePrefab, gun.position + new Vector3(0.33f, -0.6f, 0), Quaternion.identity);
                newBullet.name = missilePrefab.name;
                newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * (bulletSpeed * 1.2f);
                Destroy(newBullet, 1);
            }
            else if (missileCount == 1)
            {
                GameObject newBullet = Instantiate(missilePrefab, gun.position + new Vector3(-0.33f, -0.6f, 0), Quaternion.identity);
                newBullet.name = missilePrefab.name;
                newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * (bulletSpeed * 1.2f);
                Destroy(newBullet, 1);
            }
            if (missileCount > 0)
            {
                missileCount--;
            }
        }
    }
}
