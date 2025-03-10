using UnityEngine;
using UnityEngine.UI;

public class ConditionBar : MonoBehaviour
{
    public float maxValue;
    public float currentValue;
    public float startValue;
    public float passiveValue;
    public Image bar;

    private void Awake()
    {

    }
    void Start()
    {
        currentValue = startValue;
    }

    void Update()
    {
        bar.fillAmount = BarPercentage();
    }

    private float BarPercentage()
    {
        return currentValue / maxValue;
    }

    public void Add(float value)
    {
        currentValue = currentValue + value < maxValue ? currentValue + value : maxValue;
    }

    public void Subtract(float value)
    {
        currentValue = currentValue - value > 0 ? currentValue - value : 0;
    }

    public void AddPassive(float value)
    {
        passiveValue = passiveValue + value;
    }

    public void SubtractPassive(float value)
    {
        passiveValue = passiveValue - value;
    }
}

