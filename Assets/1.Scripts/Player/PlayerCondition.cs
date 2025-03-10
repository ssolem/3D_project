using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public ConditionUI conditionUI;

    ConditionBar hp { get {  return conditionUI.hp; } }
    ConditionBar sp { get { return conditionUI.sp; } }

    public float passiveHeight;

    void Start()
    {
        
    }

    void Update()
    {
        HealStamina(sp.passiveValue *Time.deltaTime);
        PassiveDamage();
    }

    public void AddPassiveHealth(float value)
    {
        hp.AddPassive(value);
    }

    public void SubtractPassiveHealth(float value)
    {
        hp.SubtractPassive(value);
    }

    public void AddPassiveStamina(float value)
    {
        sp.AddPassive(value);
    }

    public void SubtractPassiveStamina(float value)
    {
        sp.SubtractPassive(value);
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

    public bool EnoughStamina(float value)
    {
        if (sp.currentValue < value) return false;
        return true;
    }

    public void LoseHealth(float value)
    {
        hp.Subtract(value);
    }

    public void Die()
    {
        Debug.Log("die");
    }

    void PassiveDamage()
    {
        Transform trans = GameManager.Instance.Player.transform;
        if(trans.position.y > passiveHeight)
        {
            LoseHealth(hp.passiveValue * Time.deltaTime);
        }
    }
}
