using UnityEngine;
using UnityEngine.UI;

public class BulletPower : MonoBehaviour
{
    Slider _bulletSlider;
    private void Start()
    {
        _bulletSlider = GetComponent<Slider>();
        setRate(0);
    }
    public void setRate(int bullet)
    {
        _bulletSlider.value = bullet;
    }
    public float getRate(){
        return _bulletSlider.value;
    }
    public void adjustRate(int bullet)
    {
        _bulletSlider.value += bullet;
    }
}
