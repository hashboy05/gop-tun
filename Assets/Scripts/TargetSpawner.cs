using UnityEngine;
using System.Collections.Generic;
public class TargetSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] targetPrefab;
    [SerializeField] private Sprite powerOutline;
    public static int currentIndex = 0; // Track which prefab to spawn next
    public static List<int> num = new();

    void Update()
    {
        // checks if the play button is pressed and the game is not paused
        if (GameManager.playPressed && !GameManager.isPaused)
        {
            // make sure we have prefabs to spawn
            if (targetPrefab.Length > 0)
            {
                if (GameManager.enemyPlanes.Count == 0)
                {
                    // Check if all prefabs have been used
                    if (num.Count == targetPrefab.Length)
                    {
                        num.Clear();
                    }
                    // destroys the previous formation gameobject in the hierarchy
                    for (int i = 0; i < targetPrefab.Length; i++)
                    {
                        if (GameObject.Find(targetPrefab[i].name) != null)
                        {
                            Destroy(GameObject.Find(targetPrefab[i].name));
                            break;
                        }
                    }
                    while (true)
                    {
                        currentIndex = Random.Range(0, targetPrefab.Length);
                        if (!num.Contains(currentIndex))
                        {
                            num.Add(currentIndex);
                            break;
                        }
                    }
                    // spawn the current prefab
                    GameObject newTarget = Instantiate(targetPrefab[currentIndex]);
                    newTarget.name = targetPrefab[currentIndex].name;

                    if (newTarget.transform.childCount > 0)
                    {
                        if (GunController.shootInterval > 1){
                            GunController.shootInterval -= 0.15f;
                        }
                        foreach (Transform child in newTarget.transform)
                        {
                            GameManager.enemyPlanes.Add(child.gameObject, 100);
                            UnitHealth _enemyHealth = new(100, 100);
                            GameManager.healthBars.Add(child.gameObject, _enemyHealth);
                        }
                    }

                    // Add powerOutline as a sprite to a random child
                    int randomIndex = Random.Range(0, newTarget.transform.childCount);
                    Transform randomChild = newTarget.transform.GetChild(randomIndex);

                    // Create a new GameObject for the outline
                    GameObject outlineInstance = new("PowerOutline");
                    SpriteRenderer spriteRenderer = outlineInstance.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = powerOutline; // Set the sprite

                    // Set the position and parent of the outline instance
                    outlineInstance.transform.position = randomChild.position;
                    outlineInstance.transform.SetParent(randomChild); // Set as child of randomChild

                    // Set scale to (1, 1, 1)
                    outlineInstance.transform.localScale = Vector3.one; // Ensure uniform scale

                    newTarget.transform.position = new Vector2(0, transform.position.y);
                }
            }
        }
    }
}