using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    private Dictionary<string, UserData> playerDataDictionary = new Dictionary<string, UserData>();

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayerData(string playerId, UserData playerData)
    {
        if (!playerDataDictionary.ContainsKey(playerId))
        {
            playerDataDictionary.Add(playerId, playerData);
        }
    }

    public UserData GetPlayerData(string playerId)
    {
        playerDataDictionary.TryGetValue(playerId, out UserData playerData);
        //Debug.Log($"Key : {playerId} Value : {playerData}");
        return playerData;
    }

    //public void WritePlayerData(string playerId)
    //{
    //    Debug.Log("Writing Data.. ");
    //    Debug.Log("Oyuncu niki yazýlýyor  : " + PhotonNetwork.LocalPlayer.ToString());

    //    playerDataDictionary.TryGetValue(playerId, out UserData playerData);
    //    Debug.Log($"Health : {playerData.health} Nickname : {playerData.nickName}");
    //}
}
