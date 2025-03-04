using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class Weapon : MonoBehaviourPunCallbacks
{
    public KeyCode fireKey;
    public Transform firePos;
    bool fireInput;
    
    Player localPlayer;

    private void Start()
    {

        localPlayer = PhotonNetwork.LocalPlayer;

    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            fireInput = Input.GetKeyDown(fireKey);
            if (fireInput)
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        GameObject tempBullet = PhotonNetwork.Instantiate(PlayerDataManager.Instance.GetWeaponName(localPlayer), firePos.position, firePos.rotation);
    }
}
