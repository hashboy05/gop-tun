using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private GameObject _explosionPrefab;

    public static void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DamageUnit(dmg);
        if (GameManager.gameManager._playerBehaviour != null)
        {
            GameManager.gameManager._playerBehaviour.UpdateHealthbar();
            if (GameManager.gameManager._playerHealth.Health <= 0)
            {
                if (ScoreManager.score > ScoreManager.highscore){
                    ScoreManager.highscore = ScoreManager.score;
                }
                GameManager.gameManager._playerBehaviour.Explode();
                GameManager.gameManager.OpenEndScreen();
            }
        }
    }

    // heals the player
    public void PlayerHeal(int heal)
    {
        GameManager.gameManager._playerHealth.HealUnit(heal);
        UpdateHealthbar();
    }

    // updates the health bar on screen
    public void UpdateHealthbar()
    {
        if (_healthbar != null)
        {
            _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
        }
    }

    // displays explosion on screen
    private void Explode()
    {
        gameObject.SetActive(false);
        Instantiate(_explosionPrefab, transform.position, transform.rotation);
    }
}