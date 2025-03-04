using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HealthSystem : MonoBehaviourPunCallbacks
{

    public Vector3[] spawnPoints;

    private Vector3 spawnPoint;

    Rigidbody2D playerRb;

    Player localPlayer = PhotonNetwork.LocalPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = PhotonNetwork.LocalPlayer;


        playerRb = GetComponent<Rigidbody2D>();
        SpawnPlayer();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death") && photonView.IsMine)
        {
            CheckDeath();
        }
    }
    private void CheckDeath()
    {
        Debug.Log("Knocked Out");
        PlayerDataManager.Instance.SetHealth(localPlayer, -1);

        if (PlayerDataManager.Instance.GetHealth(localPlayer) > 0)
        {
            SpawnPlayer();
        }
        else
        {
            Debug.Log("Died");
        }
    }

    //private void UpdateHealth(int amount)
    //{
    //    playerdata.health += amount;
    //}

    private void SpawnPlayer()
    {
        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        playerRb.transform.position = spawnPoint;
    }

}
