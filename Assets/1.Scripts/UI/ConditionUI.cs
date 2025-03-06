using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionUI : MonoBehaviour
{
    public ConditionBar hp;
    public ConditionBar sp;

    void Start()
    {
        GameManager.Instance.Player.condition.conditionUI = this;
    }

    void Update()
    {
        
    }
}
