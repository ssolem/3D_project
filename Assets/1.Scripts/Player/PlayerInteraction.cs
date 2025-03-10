using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance;
    public LayerMask facedLayerMask;

    public GameObject facedGameObject;
    private Items facedItem;
    public Camera _camera;
    public TextMeshProUGUI infoText;

    void Start()
    {
        _camera = Camera.main;
        GameManager.Instance.Player.interact += Interact;
    }

    void Update()
    {
        PlayerCenterRay();
    }

    void PlayerCenterRay()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width /2 , Screen.height /2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance, facedLayerMask))
        {
            if(hit.collider.gameObject != facedGameObject)
            {
                facedGameObject = hit.collider.gameObject;           
                facedItem = facedGameObject.GetComponent<Items>();
                //Á¤º¸ ¶ç¿ì±â
                FacedInfoUI();
            }
        }
        else
        {
            facedGameObject = null;
            facedItem = null;
            infoText.gameObject.SetActive(false);
        }
    }

    void FacedInfoUI()
    {
        infoText.gameObject.SetActive(true);
        infoText.text = $"{facedItem.itemData.itemName}\n{facedItem.itemData.itemDescription}";
    }

    public void Interact()
    {
        if(facedGameObject != null)
        {
            facedItem.SendToInventory();
            facedGameObject = null;
            facedItem = null;
            infoText.gameObject.SetActive(false);
        }
    }
}
