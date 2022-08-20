using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    public Slider slider;

    public void Init(float maxValue, float initValue)
    {
        slider.maxValue = maxValue;
        slider.value = initValue;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
