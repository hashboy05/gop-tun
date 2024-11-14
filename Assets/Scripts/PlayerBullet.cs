using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // reduces enemy plane damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            if (gameObject.name == "bullet" && GameManager.isTutorial){
                Damage damageComponent = collision.gameObject.GetComponent<Damage>();
                GameManager.enemyPlanes[collision.gameObject] -= 40;
                GameManager.healthBars[collision.gameObject].DamageUnit(40);
                damageComponent.UpdateHealthbar(collision.gameObject);

                if (GameManager.healthBars[collision.gameObject].Health <= 0)
                {
                    if (GameManager.enemyPlanes[collision.gameObject] <= 0)
                    {
                        bool hasPower = false;
                        Vector3 position = Vector3.zero;
                        // checks if the enemy plane contains a power
                        foreach (Transform child in collision.gameObject.transform)
                        {
                            if (child.name == "PowerOutline")
                            {
                                hasPower = true;
                                position = collision.gameObject.transform.position;
                                break; // Exit loop once found
                            }
                        }
                        GameManager.enemyPlanes.Remove(collision.gameObject);
                        Destroy(collision.gameObject);
                        damageComponent.Explode();

                        if (hasPower){
                            GameObject power = Instantiate(GameManager.powerPrefabsList[0], position, Quaternion.identity);
                            power.name = GameManager.powerPrefabsList[0].name;
                        }
                    }
                }
            }
            else if (gameObject.name == "missile" && GameManager.isTutorial){
                Damage damageComponent = collision.gameObject.GetComponent<Damage>();
                GameManager.enemyPlanes[collision.gameObject] -= 100;
                GameManager.healthBars[collision.gameObject].DamageUnit(100);
                damageComponent.UpdateHealthbar(collision.gameObject);

                if (GameManager.healthBars[collision.gameObject].Health <= 0)
                {
                    if (GameManager.enemyPlanes[collision.gameObject] <= 0)
                    {
                        bool hasPower = false;
                        Vector3 position = Vector3.zero;
                        // checks if the enemy plane contains a power
                        foreach (Transform child in collision.gameObject.transform)
                        {
                            if (child.name == "PowerOutline")
                            {
                                hasPower = true;
                                position = collision.gameObject.transform.position;
                                break; // Exit loop once found
                            }
                        }
                        GameManager.enemyPlanes.Remove(collision.gameObject);
                        Destroy(collision.gameObject);
                        damageComponent.Explode();

                        if (hasPower){
                            GameObject power = Instantiate(GameManager.powerPrefabsList[0], position, Quaternion.identity);
                            power.name = GameManager.powerPrefabsList[0].name;
                        }
                    }
                }
            }
            // checks if the player is shooting bullet or missile
            else if (gameObject.name == "bullet"){
                Destroy(gameObject);
                Damage.ReduceDamage(40, collision.gameObject);
            }
            else if (gameObject.name == "missile"){
                Destroy(gameObject);
                Damage.ReduceDamage(100, collision.gameObject);
            }
        }
    }
}
