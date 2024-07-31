using System.Collections;
using System.Collections.Generic;
using Unity.UI;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HealthSystem : MonoBehaviourPunCallbacks
{

    UserData playerdata;

    public Vector3[] spawnPoints;

    private Vector3 spawnPoint;

    Rigidbody2D playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerdata = PlayerDataManager.Instance.GetPlayerData(PhotonNetwork.NickName);
        playerRb = GetComponent<Rigidbody2D>();
        SpawnPlayer();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            CheckDeath();
        }
    }
    private void CheckDeath()
    {
        Debug.Log("Knocked Out");
        UpdateHealth(-1);
        if (playerdata.health > 0)
        {
            SpawnPlayer();
        }
        else
        {
            Debug.Log("Died");
        }
    }

    private void UpdateHealth(int amount)
    {
        playerdata.health += amount;
    }

    private void SpawnPlayer()
    {
        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        playerRb.transform.position = spawnPoint;
    }

}
