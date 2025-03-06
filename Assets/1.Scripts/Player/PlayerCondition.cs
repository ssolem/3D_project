using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public ConditionUI conditionUI;

    ConditionBar hp { get {  return conditionUI.hp; } }
    ConditionBar sp { get { return conditionUI.sp; } }

    

    void Start()
    {
        
    }

    void Update()
    {
        HealStamina(sp.passiveValue *Time.deltaTime);
    }

    public void HealStamina(float value)
    {
        sp.Add(value);
    }

    public void HealHealth(float value)
    {
        sp.Add(value);
    }

    public void LoseStamina(float value)
    {
        sp.Subtract(value);
    }

    public void LoseHealth(float value)
    {
        hp.Subtract(value);
    }
}
