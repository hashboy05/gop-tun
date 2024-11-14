using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Healthbar _healthbar;
    public GameObject explosionPrefab;
    private static bool hasPower = false;

    public static void ReduceDamage(int bulletDamage, GameObject gameObject)
    {
        Damage damageComponent = gameObject.GetComponent<Damage>();
        GameManager.enemyPlanes[gameObject] -= bulletDamage;
        GameManager.healthBars[gameObject].DamageUnit(bulletDamage);
        damageComponent.UpdateHealthbar(gameObject);

        if (GameManager.healthBars[gameObject].Health <= 0)
        {
            if (GameManager.enemyPlanes[gameObject] <= 0)
            {
                Vector3 position = Vector3.zero;
                // checks if the enemy plane contains a power
                foreach (Transform child in gameObject.transform)
                {
                    if (child.name == "PowerOutline")
                    {
                        hasPower = true;
                        position = gameObject.transform.position;
                        break; // Exit loop once found
                    }
                }
                GameManager.enemyPlanes.Remove(gameObject);
                Destroy(gameObject);
                damageComponent.Explode();
                ScoreManager.instance.AddPoint(100);
                
                if (hasPower){
                    int randomIndex = Random.Range(0, 3);
                    GameObject power = Instantiate(GameManager.powerPrefabsList[randomIndex], position, Quaternion.identity);
                    power.name = GameManager.powerPrefabsList[randomIndex].name;
                }
                hasPower = false;
            }
        }
    }
    public void Explode()
    {
        GameObject explosionSound = GameObject.Find("PlaneExplosion");
        explosionSound.GetComponent<AudioSource>().Play();
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
    public void UpdateHealthbar(GameObject gameObject)
    {
        if (_healthbar != null)
        {
            _healthbar.setHealth(GameManager.healthBars[gameObject].Health);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage damageComponent = gameObject.GetComponent<Damage>();
            Destroy(gameObject);
            damageComponent.Explode();
            PlayerBehaviour.PlayerTakeDmg(100);
        }
    }
}