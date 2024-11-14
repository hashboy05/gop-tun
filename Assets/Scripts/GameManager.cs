using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject quit;

    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject resume;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject mission;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private GameObject quitPage;
    [SerializeField] private GameObject missileCount;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject bulletPowerBar;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject highscore;
    [SerializeField] private GameObject playerPlane;
    [SerializeField] private GameObject bgMusic;
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private GameObject[] powerPrefabs;
    public static bool playPressed; // checks if the play button is clicked
    public static bool isPaused; // checks if the game is paused
    public static bool isTutorial; // checks if the game is tutorial
    public static bool bulletPowerOn;
    public static GameManager gameManager { get; private set; }
    public UnitHealth _playerHealth;
    public PlayerBehaviour _playerBehaviour;
    public BulletPower _bulletPower;
    public static Dictionary<GameObject, int> enemyPlanes = new(); // stores the enemy planes
    public static Dictionary<GameObject, UnitHealth> healthBars = new(); // stores the enemy planes' healthbar display
    public static Dictionary<int, GameObject> powerPrefabsList = new(); // stores the player plane powerups
    public static Dictionary<int, string> tutorialMessages = new(); // stores the tutorial messages

    private void Awake()
    {
        play.SetActive(true);
        tutorial.SetActive(true);
        settings.SetActive(true);
        credits.SetActive(true);
        quit.SetActive(true);
        pause.SetActive(false);
        resume.SetActive(false);
        mainMenu.SetActive(false);
        restart.SetActive(false);
        gameOver.SetActive(false);
        mission.SetActive(false);
        logo.SetActive(true);
        missileCount.SetActive(false);
        healthBar.SetActive(false);
        bulletPowerBar.SetActive(false);
        score.SetActive(false);
        highscore.SetActive(true);
        playPressed = false;
        isPaused = false;
        isTutorial = false;
        bulletPowerOn = false;
        playerPlane.GetComponent<AudioSource>().Pause();
        bgMusic.GetComponent<AudioSource>().Play();
        Time.timeScale = 0f;
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
            _playerHealth = new UnitHealth(100, 100);

            // Add all power prefabs to the powerPrefabsList dictionary
            for (int i = 0; i < powerPrefabs.Length; i++)
            {
                powerPrefabsList.Add(i, powerPrefabs[i]);
            }
        }
    }

    private void Start()
    {
        _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        _bulletPower = bulletPowerBar.GetComponent<BulletPower>();
    }
    public void Quit()
    {
        Time.timeScale = 0f;
        Application.Quit();
    }

    void Update()
    {
        // play button
        if (Input.GetKeyDown(KeyCode.P) && !playPressed && !isPaused && !isTutorial){
            play.GetComponent<Button>().onClick.Invoke();
        }
        // tutorial button
        if (Input.GetKeyDown(KeyCode.T) && !playPressed && !isPaused && !isTutorial){
            tutorial.GetComponent<Button>().onClick.Invoke();
        }
        // settings button
        if (Input.GetKeyDown(KeyCode.E) && !playPressed && !isPaused && !isTutorial){
            if (settingsPage.activeSelf){
                settingsPage.SetActive(false);
                highscore.SetActive(true);
            }
            else{
                settings.GetComponent<Button>().onClick.Invoke();
            }
        }
        // credits button
        if (Input.GetKeyDown(KeyCode.C) && !playPressed && !isPaused && !isTutorial){
            if (creditsPage.activeSelf){
                creditsPage.SetActive(false);
                highscore.SetActive(true);
            }
            else{
                credits.GetComponent<Button>().onClick.Invoke();
            }
        }
        // quit button
        if (Input.GetKeyDown(KeyCode.Q) && !playPressed && !isPaused && !isTutorial){
            if (quitPage.activeSelf){
                quitPage.SetActive(false);
                highscore.SetActive(true);
            }
            else{
                quit.GetComponent<Button>().onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && quitPage.activeSelf){
            Quit();
        }
        // escape button for pausing game
        if (Input.GetKeyDown(KeyCode.Escape) && (playPressed || isTutorial))
        {
            if (isPaused)
            {
                resume.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                pause.GetComponent<Button>().onClick.Invoke();
            }
        }
        // restart button
        if (restart.activeSelf){
            if (Input.GetKeyDown(KeyCode.R)){
                restart.GetComponent<Button>().onClick.Invoke();
            }
        }
        // mainMenu button
        if (mainMenu.activeSelf){
            if (Input.GetKeyDown(KeyCode.Y)){
                mainMenu.GetComponent<Button>().onClick.Invoke();
            }
        }

        // space bar for bullet power
        if (Input.GetKeyDown(KeyCode.Space) && playPressed && !bulletPowerOn)
        {
            if (_bulletPower.getRate() > 0)
            {
                _bulletPower.adjustRate(-1);
                StartCoroutine(ActivateBulletPower());
            }
        }
    }

    private IEnumerator ActivateBulletPower()
    {
        bulletPowerOn = true; // Set to true
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        bulletPowerOn = false; // Set back to false after 5 seconds
    }

    public void Play()
    {
        play.SetActive(false);
        tutorial.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
        quit.SetActive(false);
        pause.SetActive(true);
        resume.SetActive(false);
        mainMenu.SetActive(false);
        logo.SetActive(false);
        highscore.SetActive(false);
        missileCount.SetActive(true);
        healthBar.SetActive(true);
        bulletPowerBar.SetActive(true);
        RectTransform rectTransform = score.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(669, 482);
        score.SetActive(true);
        highscore.SetActive(false);
        TargetSpawner.currentIndex = 0;
        playPressed = true;
        isPaused = false;
        playerPlane.GetComponent<AudioSource>().Play();
        bgMusic.GetComponent<AudioSource>().Stop();
        gameMusic.GetComponent<AudioSource>().Play();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pause.SetActive(false);
        resume.SetActive(true);
        mainMenu.SetActive(true);
        isPaused = true;
        playerPlane.GetComponent<AudioSource>().Pause();
        if (!isTutorial){
            gameMusic.GetComponent<AudioSource>().Pause();
        }
        Time.timeScale = 0f;
    }
    // calls when the resume button is clicked
    public void Resume()
    {
        pause.SetActive(true);
        resume.SetActive(false);
        mainMenu.SetActive(false);
        isPaused = false;
        playerPlane.GetComponent<AudioSource>().Play();
        if (!isTutorial){
            gameMusic.GetComponent<AudioSource>().Play();
        }
        Time.timeScale = 1f;
    }
    // calls when the main menu button is clicked
    public void MainMenu()
    {
        play.SetActive(true);
        tutorial.SetActive(true);
        settings.SetActive(true);
        credits.SetActive(true);
        quit.SetActive(true);
        pause.SetActive(false);
        resume.SetActive(false);
        mainMenu.SetActive(false);
        restart.SetActive(false);
        gameOver.SetActive(false);
        mission.SetActive(false);
        logo.SetActive(true);
        missileCount.SetActive(false);
        healthBar.SetActive(false);
        bulletPowerBar.SetActive(false);
        score.SetActive(false);
        highscore.SetActive(true);
        playPressed = false;
        isPaused = false;
        isTutorial = false;
        playerPlane.GetComponent<AudioSource>().Pause();
        bgMusic.GetComponent<AudioSource>().Play();
        gameMusic.GetComponent<AudioSource>().Stop();
        Reset();
        Time.timeScale = 0f;
    }

    // is called when the player plane explodes
    public void OpenEndScreen()
    {
        restart.SetActive(true); // Activate the restart screen
        mainMenu.SetActive(true); // Activate the main menu
        gameOver.SetActive(true);
        pause.SetActive(false);
        RectTransform rectTransform = score.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(94, 165);
        score.SetActive(true);
        highscore.SetActive(true);
        playerPlane.GetComponent<AudioSource>().Pause();
        gameMusic.GetComponent<AudioSource>().Stop();
        StartCoroutine(OpenEndScreenCoroutine());
    }
    private IEnumerator OpenEndScreenCoroutine()
    {
        yield return new WaitForSeconds(0.3f); // Wait for 0.3 second
        Time.timeScale = 0; // Set time scale to 0
    }
    // restart game
    public void Restart()
    {
        restart.SetActive(false);
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        ScoreManager.instance.restartGame();
        Time.timeScale = 1;
        Reset();
        Play();
    }
    // resets everything in the game
    public void Reset(){
        // removes every gameobject in the game
        GameObject[] planes = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject obj in planes)
        {
            Damage.ReduceDamage(100, obj);
            Destroy(obj);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in bullets)
        {
            Destroy(obj);
        }
        GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (GameObject obj in explosions)
        {
            Destroy(obj);
        }
        GameObject[] powers = GameObject.FindGameObjectsWithTag("Power");
        foreach (GameObject obj in powers)
        {
            Destroy(obj);
        }

        // Clear the dictionaries
        enemyPlanes.Clear();
        healthBars.Clear();
        TargetSpawner.num.Clear();

        // reposition the player plane
        playerPlane.SetActive(true);
        playerPlane.transform.position = new Vector3(0f, -3.51f, 0f);

        ResetHealth();
        ScoreManager.instance.restartGame();
        GunController.missileCount = 2;
        GunController.shootInterval = 2.5f;
        _bulletPower.setRate(0);
        bulletPowerOn = false;
    }
    public void ResetHealth()
    {
        _playerHealth.Health = 100;
        gameManager._playerBehaviour?.UpdateHealthbar();
    }
    public void Tutorial(){
        logo.SetActive(false);
        highscore.SetActive(false);
        play.SetActive(false);
        tutorial.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
        healthBar.SetActive(true);
        bulletPowerBar.SetActive(true);
        missileCount.SetActive(true);
        quit.SetActive(false);
        pause.SetActive(true);

        // 4, 6, 7, 9, 11, 14
        tutorialMessages.Add(0,"Greetings, aviator! This is your captain speaking. Welcome to the Gop Tun academy, not Top Gun! Today you will learn about the basic controls and your mission in this game.");
        tutorialMessages.Add(1,"Starting off, do you see a plane at the bottom of the screen? That's right, You will be piloting this plane!");
        tutorialMessages.Add(2,"Notice the bars and images on the bottom left and right of the screen? The bottom left bar represents the health of your plane.");
        tutorialMessages.Add(3,"On the bottom right, from top to bottom, it's the missile count and 2x bullet rate bar.");
        tutorialMessages.Add(4,"Let's start off with flying the plane! Move left and right by clicking on either 'A' and 'D' or '<' and '>'...");
        tutorialMessages.Add(5,"Well done! Now you know how to fly the plane. I know you can't wait to shoot down some jets, so let's move on, shall we?");
        tutorialMessages.Add(6,"There are two types of weapons this plane carries: guns and missiles. You need at most 3 gun bullets to shoot down the enemy plane. To shoot the guns, press 'N'...");
        tutorialMessages.Add(7,"Then your plane is equipped with 2 missiles. One missile can immediately destroy a plane. To deploy a missile, press 'M'...");
        tutorialMessages.Add(8,"Excellent! Now you know how to use the weapons.");
        tutorialMessages.Add(9,"You are almost done! Let's start a trial. Here spawns an enemy plane. You task is to shoot it down before it shoots you back. Use your controls and shoot down the plane, aviator! (press 'N' or 'M')...");
        tutorialMessages.Add(10,"Well done! You really are a speedy learner!");
        tutorialMessages.Add(11,"Now here spawns another enemy plane. Do you notice the yellow dash around it?");
        tutorialMessages.Add(12,"The yellow dash marks that when you shoot down an enemy plane, a powerup will spawn and you can get the powerup by hitting your plane with it! Shoot it down! (press 'N' or 'M')..");
        tutorialMessages.Add(13,"It seems like you got a 2x bullet rate. You can also notice that the green bar on the bottom right of the screen increases by one.");
        tutorialMessages.Add(14,"To use the powerup, press the Space Bar to activate the 2x bullet rate...");
        tutorialMessages.Add(15,"Keep in mind this powerup lasts for 5 seconds only so use it wisely! Shoot the gun! (press 'N')");
        tutorialMessages.Add(16,"There are also two other powerups which are: 'heal' which can restore your health bar to 100% again, and 'missiles' to reload your missiles fully.");
        tutorialMessages.Add(17,"Now with all the instructions done, here is your mission. Your mission is to destroy as many enemy planes as possible.");
        tutorialMessages.Add(18,"Here are three things you need to avoid: \ndo not let the enemy plane reach the bottom of the screen, \ndo not collide with an enemy plane, \nand most importantly..., \ndo not let the enemy planes shoot you down!");
        tutorialMessages.Add(19,"Congratulations, aviator! You are now a Gop Tun graduate! I wish you the best of luck in your mission...");
        tutorialMessages.Add(20,"...and remember, DON'T THINK, JUST DO!");
        isTutorial = true;
        playerPlane.GetComponent<AudioSource>().Play();
        bgMusic.GetComponent<AudioSource>().Stop();
        Time.timeScale = 1f;
    }
}