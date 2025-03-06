using UnityEngine;
using UnityEngine.UI;

public class ConditionBar : MonoBehaviour
{
    public float maxValue;
    private float currentValue;
    public float startValue;
    public float passiveValue;
    public Image bar;

    private void Awake()
    {
        bar = GetComponent<Image>();
    }
    void Start()
    {
        currentValue = startValue;
    }

    // Update is called once per frame
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
        currentValue = currentValue - value > 0 ? currentValue + value : 0;
    }
}
