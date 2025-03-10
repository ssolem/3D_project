using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera firstPer;
    public CinemachineVirtualCamera thirdPer;
    public CinemachineVirtualCamera invenPer;
    public LayerMask player;
    public LayerMask every;
    public float cullTimePlayer;
    public float cullTimeEvery;

    public void ChangePlayerPer()
    {
        if (invenPer.gameObject.activeSelf)
            return;
        if (firstPer.gameObject.activeSelf)
        {
            firstPer.gameObject.SetActive(false);
            Invoke("CullMaskEvery", cullTimeEvery);
            return;
        }

        firstPer.gameObject.SetActive(true);
        Invoke("CullMaskPlayer", cullTimePlayer);

    }

    public void ChangeToInvenPer()
    {
        invenPer.gameObject.SetActive(true);
        if(Camera.main.cullingMask != every)
        {
            Invoke("CullMaskEvery", cullTimeEvery);
        }
    }

    public void ChangeFromInvenPer()
    {
        invenPer.gameObject.SetActive(false);
        if(firstPer.gameObject.activeSelf)
        {
            Invoke("CullMaskPlayer", cullTimePlayer);
        }
    }

    void CullMaskPlayer()
    {
        Camera.main.cullingMask = ~player;
    }

    void CullMaskEvery()
    {
        Camera.main.cullingMask = every;
    }
}
