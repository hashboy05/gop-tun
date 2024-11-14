public class UnitHealth
{
    private int _currentHealth;
    private int _currentMaxHealth;

    public int Health
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public int MaxHealth
    {
        get { return _currentMaxHealth; }
        set { _currentMaxHealth = value; }
    }

    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    public void DamageUnit(int dmgAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dmgAmount;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
        }
    }

    public void HealUnit(int healAmount)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }

        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }
}