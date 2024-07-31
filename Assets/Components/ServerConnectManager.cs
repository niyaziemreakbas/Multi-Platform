using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using TMPro;

public class ServerConnectManager : MonoBehaviourPunCallbacks
{
    UserData playerData;

    public TextMeshProUGUI healthText;
    
    // Start is called before the first frame update
    void Start()
    {
        playerData = PlayerDataManager.Instance.GetPlayerData(PhotonNetwork.NickName);
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        healthText.text = playerData.health.ToString();
        // Debug.Log(PhotonNetwork.PlayerList.Length);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("servera girildi0");

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("lobiye girildi0");

        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 10, IsOpen = true, IsVisible = true }, TypedLobby.Default);       
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("oyuncunun ismi : " + PhotonNetwork.LocalPlayer);
        Debug.Log("Odaya girildi0");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        WeaponType weaponType = null;
        string nickName = "Unknown";


        Debug.Log("New Player Joined");
        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, null);
        Transform weapon = player.transform.Find("Weapon");

        nickName = playerData.nickName;

        weaponType = playerData.weaponType;
        Debug.Log(nickName + "'s weapon type is: " + weaponType);

        if (weapon != null)
        {
            SpriteRenderer weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
            if (weaponSpriteRenderer != null)
            {

                weaponSpriteRenderer.sprite = weaponType.weaponSprite;
                Debug.Log("Weapon sprite has been changed");
            }
            else
            {
                Debug.LogWarning("Weapon object does not have a SpriteRenderer component.");
            }
        }   
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("odaya girilemedi");
        base.OnJoinRandomFailed(returnCode, message);
    }



}
