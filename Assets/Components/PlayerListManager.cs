using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListManager : MonoBehaviour
{
    //public GameObject playerListPanel;
    int playerNumber;
    UserData playerdata;
    // Start is called before the first frame update
    void Start()
    {
        playerdata = PlayerDataManager.Instance.GetPlayerData(PhotonNetwork.NickName);
        playerNumber = PhotonNetwork.PlayerList.Length;
    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    playerListPanel.SetActive(!playerListPanel.activeSelf);  // Tab tu�una bas�ld���nda paneli a�/kapa
        //    if (PhotonNetwork.PlayerList.Length != playerNumber)
        //    {
        //        UpdatePlayerList();

        //    }
        //}

    }

    void UpdatePlayerList()
    {

    }
}
