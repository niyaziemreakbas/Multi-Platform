using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Photon.Realtime;
using Photon.Pun;
public class Weapon : MonoBehaviourPunCallbacks
{
    public KeyCode fireKey;
    public WeaponType weaponType;
    public Transform firePos;
    bool fireInput;
    
    
    GameObject bulletPrefab;


    private void Start()
    {
        bulletPrefab = weaponType.bulletPrefab;

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
        GameObject tempBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
