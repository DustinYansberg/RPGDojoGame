using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxHealth(Unit unit)
    {
        slider.value = unit.CurrentHP;
        slider.maxValue = unit.MaxHP;
    }


}
