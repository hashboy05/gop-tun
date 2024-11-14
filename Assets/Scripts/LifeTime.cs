using UnityEngine;

public class LifeTime : MonoBehaviour
{
    private float lifeTime = 0.3f;
    // explosion prefab destroys after 0.3 secs
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
