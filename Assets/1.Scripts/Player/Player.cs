using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public CameraControl camControl;

    public ItemData itemData;
    public Action interact;
    public Action addItem;


    private void Awake()
    {
        GameManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        camControl = Camera.main.GetComponent<CameraControl>();
    }
}
