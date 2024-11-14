using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialMessage;
    [SerializeField] private GameObject tutorialPage;
    [SerializeField] private GameObject skip;
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private GameObject playerPlane;
    [SerializeField] private GameObject enemyPlane;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject healImage;
    [SerializeField] private GameObject reloadImage;
    [SerializeField] private Sprite powerOutline;
    [SerializeField] private TextMeshProUGUI msCount;
    private GameObject enemy;
    public static int count = 0;
    public static int missileCount = 2;
    private bool aClicked = false;
    private bool dClicked = false;
    private bool lClicked = false;
    private bool rClicked = false;
    private bool spawned = false;
    private bool displayDone = false;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isTutorial){
            if (count==1){
                RectTransform rectTransform = arrow.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(-171, -253);
                arrow.transform.rotation = Quaternion.Euler(0,0,-39.437f);
                arrow.SetActive(true);
            }
            else if (count==2){
                RectTransform rectTransform = arrow.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(-699, -240);
                arrow.transform.rotation = Quaternion.Euler(0,0,-113.66f);
            }
            else if (count==3){
                RectTransform rectTransform = arrow.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(502, -233);
                arrow.transform.rotation = Quaternion.Euler(0,0,-39.437f);
            }
            else if (count==4){
                arrow.SetActive(false);
                DetectKeyInputs();
            }
            else if (count==6){
                DetectGun();
            }
            else if (count==7){
                DetectMissile();
            }
            else if (count==8){
                playerPlane.transform.position = new Vector3(0, -3.51f, 0);
            }
            else if (count==9){
                DetectGun();
                DetectMissile();
                SpawnEnemyPlane(false);
            }
            else if (count==11){
                spawned = false;
                SpawnEnemyPlane(true);
            }
            else if (count==12){
                DetectGun();
                DetectMissile();
                spawned = true;
                if (enemy == null && spawned){
                    Skip();
                }
            }
            else if (count==13){
                RectTransform rectTransform = arrow.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(502, -233);
                arrow.transform.rotation = Quaternion.Euler(0,0,-39.437f);
                arrow.SetActive(true);
            }
            else if (count==14){
                arrow.SetActive(false);
                DetectPower();
            }
            else if (count==15){
                DetectDoubleGun();
            }
            else if (count==16){
                healImage.SetActive(true);
                reloadImage.SetActive(true);
            }
            else if (count==17){
                healImage.SetActive(false);
                reloadImage.SetActive(false);
            }
            else{
                arrow.SetActive(false);
            }
            // Update the missile count on screen
            if (msCount != null)
            {
                msCount.text = missileCount + "/2";
            }
            // next button
            if (skip.activeSelf){
                if (Input.GetKeyDown(KeyCode.Return)){
                    skip.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
        else{
            tutorialPage.SetActive(false);
            arrow.SetActive(false);
            healImage.SetActive(false);
            reloadImage.SetActive(false);
            count = 0;
            missileCount = 2;
            aClicked = false;
            dClicked = false;
            lClicked = false;
            rClicked = false;
            spawned = false;
            GameManager.tutorialMessages.Clear();
        }
    }
    public void StartTutorial(){
        StartCoroutine(TypewriterEffect(GameManager.tutorialMessages[count]));
    }
    private IEnumerator TypewriterEffect(string message)
    {
        tutorialMessage.text = ""; // Clear previous text
        skip.SetActive(false);
        foreach (char letter in message.ToCharArray())
        {
            tutorialMessage.text += letter; // Add one letter at a time
            
            yield return new WaitForSeconds(0.03f); // Wait for a short duration before adding the next letter
        }
        displayDone = true;
        CheckSkipButtonVisibility();
    }
    private void CheckSkipButtonVisibility()
    {
        if (count == 4 || count == 6 || count == 7 || count == 9 || count == 12 || count == 14 || count == 15)
        {
            skip.SetActive(false);
        }
        else
        {
            skip.SetActive(true);
        }
    }
    public void Skip(){
        displayDone = false;
        count++;
        if (count>=GameManager.tutorialMessages.Count){
            GameManager.isTutorial = false;
            tutorialPage.SetActive(false);
            count = 0;
            missileCount = 2;
            aClicked = false;
            dClicked = false;
            lClicked = false;
            rClicked = false;
            spawned = false;
            GameManager.tutorialMessages.Clear();
            GameManager.gameManager.MainMenu();
        }
        else{
            StartCoroutine(TypewriterEffect(GameManager.tutorialMessages[count]));
        }
    }
    private void DetectKeyInputs()
    {
        if (displayDone){
            // Check if the user has pressed A and D or < and >
            if (Input.GetKeyDown(KeyCode.A))
            {
                aClicked = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dClicked = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lClicked = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rClicked = true;
            }
            if ((aClicked && dClicked) || (lClicked && rClicked)){
                Skip(); // Proceed to the next tutorial message
            }
        }
    }
    private void DetectGun()
    {
        if (displayDone){
            // check for gun
            if (Input.GetKeyDown(KeyCode.N))
            {
                GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
                newBullet.name = bulletPrefab.name;
                newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * 12f;
                Destroy(newBullet, 0.3f);
                if (count==6){
                    Skip();
                }
            }
        }
    }
    private void DetectMissile()
    {
        if (displayDone){
            // Check for missile
            if (Input.GetKeyDown(KeyCode.M))
            {
                // checks the missile count and changes the position
                if (missileCount == 2)
                {
                    GameObject newBullet = Instantiate(missilePrefab, gun.position + new Vector3(0.33f, -0.6f, 0), Quaternion.identity);
                    newBullet.name = missilePrefab.name;
                    newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * (12f * 1.2f);
                    Destroy(newBullet, 0.3f);
                }
                else if (missileCount == 1)
                {
                    GameObject newBullet = Instantiate(missilePrefab, gun.position + new Vector3(-0.33f, -0.6f, 0), Quaternion.identity);
                    newBullet.name = missilePrefab.name;
                    newBullet.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * (12f * 1.2f);
                    Destroy(newBullet, 0.3f);
                }
                if (missileCount > 0)
                {
                    missileCount--;
                }
                if (count==7){
                    Skip();
                }
            }
        }
    }
    private void SpawnEnemyPlane(bool withOutline)
    {
        // spawn the current prefab
        if (enemy == null && !spawned){
            spawned = true;
            enemy = Instantiate(enemyPlane, new Vector3(0, 0.75f, 0), Quaternion.Euler(0, 0, 180));
            enemy.name = enemyPlane.name;

            if (withOutline){
                // Create a new GameObject for the outline
                GameObject outlineInstance = new("PowerOutline");
                SpriteRenderer spriteRenderer = outlineInstance.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = powerOutline; // Set the sprite

                // Set the position and parent of the outline instance
                outlineInstance.transform.position = enemy.transform.position;
                outlineInstance.transform.SetParent(enemy.transform); // Set as child of randomChild

                // Set scale to (1, 1, 1)
                outlineInstance.transform.localScale = Vector3.one; // Ensure uniform scale
            }

            GameManager.enemyPlanes.Add(enemy, 100);
            UnitHealth _enemyHealth = new(100, 100);
            GameManager.healthBars.Add(enemy, _enemyHealth);

            // Freeze the Y-axis in its Rigidbody
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if (enemy == null && spawned){
            Skip();
        }
    }
    private void DetectPower()
    {
        if (displayDone){
            // Check if the user has pressed A and D or < and >
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // bulletPowerOn = true;
                if (GameManager.gameManager._bulletPower.getRate() > 0)
                {
                    GameManager.gameManager._bulletPower.adjustRate(-1);
                }
                Skip();
            }
        }
    }
    private void DetectDoubleGun(){
        if (displayDone){
            if (Input.GetKeyDown(KeyCode.N)){
                GameObject newBullet1 = Instantiate(bulletPrefab, gun.position + new Vector3(0.33f, -0.6f, 0), Quaternion.identity);
                GameObject newBullet2 = Instantiate(bulletPrefab, gun.position + new Vector3(-0.33f, -0.6f, 0), Quaternion.identity);
                newBullet1.name = bulletPrefab.name;
                newBullet2.name = bulletPrefab.name;
                newBullet1.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * 12f;
                newBullet2.GetComponent<Rigidbody2D>().velocity = Vector3.up.normalized * 12f;
                Destroy(newBullet1, 0.4f);
                Destroy(newBullet2, 0.4f);
                Skip();
            }
        }
    }
}
