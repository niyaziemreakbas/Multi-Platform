using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ServerConnectManager : MonoBehaviourPunCallbacks
{
    Player localPlayer;

    string nickName = "Unknown";


    // Start is called before the first frame update
    void Start()
    {
        localPlayer = PhotonNetwork.LocalPlayer;
        
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
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

        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, null);

        WeaponTypeManager.Instance.UpdatePlayersWeaponSprite();

        nickName = PlayerDataManager.Instance.GetPlayerName(localPlayer);

        Debug.Log("New Player Joined : " + nickName);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("odaya girilemedi");
        base.OnJoinRandomFailed(returnCode, message);
    }



}
