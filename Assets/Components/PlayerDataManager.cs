using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PlayerDataManager : MonoBehaviourPunCallbacks
{
    public static PlayerDataManager Instance { get; private set; }

    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnDataChanged();
        }
    }

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

    private void Start()
    {
        NotifyObservers();
    }

    public void SetPlayerData(string nickName,string weaponType, int health)
    {
        Debug.Log($"SetPlayerData called with UserData: Nickname = {nickName}, Health = {health}, WeaponType = {weaponType}");

        Player localPlayer = PhotonNetwork.LocalPlayer;


        Hashtable playerProperties = new Hashtable
        {
            { "playerName", nickName },
            { "weaponType", weaponType},
            { "health", health }
        };

        NotifyObservers();

        localPlayer.NickName = nickName;

        localPlayer.SetCustomProperties(playerProperties);
    }

    public List<UserData> GetAllPlayerData()
    {
        List<UserData> allPlayerData = new List<UserData>();

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GetPlayerData(player);
        }

        return allPlayerData;
    }

    public void GetPlayerData(Player player)
    {
        // Oyuncunun özel bilgilerini almak için CustomProperties özelliðini kullanýyoruz.
        if (player.CustomProperties.ContainsKey("playerName") &&
            player.CustomProperties.ContainsKey("weaponType") &&
            player.CustomProperties.ContainsKey("health"))
        {
            string playerName = player.CustomProperties["playerName"] as string;
            string weaponType = player.CustomProperties["weaponType"] as string;
            int health = (int)player.CustomProperties["health"];

            Debug.Log($"Player Name: {playerName}, Weapon Type: {weaponType}, Health: {health}");
        }
        else
        {
            Debug.LogWarning("Player data not found.");
        }
    }

    public string GetPlayerName(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerName"))
        {
            return player.CustomProperties["playerName"] as string;
        }
        else
        {
            Debug.LogWarning("Player name not found.");
            return string.Empty;
        }
    }

    public WeaponType GetWeaponType(Player player)
    {
        if (player.CustomProperties.ContainsKey("weaponType"))
        {
            return WeaponTypeManager.Instance.GetWeaponTypeByName(player.CustomProperties["weaponType"] as string);
        }
        else
        {
            Debug.LogWarning("Weapon type not found.");
            return null;
        }
    }

    public string GetWeaponName(Player player)
    {
        if (player.CustomProperties.ContainsKey("weaponType"))
        {
            return player.CustomProperties["weaponType"] as string;
        }
        else
        {
            Debug.LogWarning("Weapon type not found.");
            return null;
        }
    }

    public int GetHealth(Player player)
    {
        if (player.CustomProperties.ContainsKey("health"))
        {
            return (int)player.CustomProperties["health"];
        }
        else
        {
            Debug.LogWarning("Health not found.");
            return 0;
        }
    }

    public void SetPlayerName(Player player, string newName)
    {
        Hashtable playerProperties = player.CustomProperties;
        playerProperties["playerName"] = newName;
        player.SetCustomProperties(playerProperties);
        player.NickName = newName; // Ayrýca Photon'daki oyuncu adýný da güncellemek için

        NotifyObservers();
    }

    public void SetWeaponType(Player player, string newWeaponType)
    {
        Hashtable playerProperties = player.CustomProperties;
        playerProperties["weaponType"] = newWeaponType;
        player.SetCustomProperties(playerProperties);

        NotifyObservers();

    }

    public void SetHealth(Player player, int health)
    {
        int currentHealth = 0;

        // Mevcut saðlýk deðerini al
        if (player.CustomProperties.ContainsKey("health"))
        {
            currentHealth = (int)player.CustomProperties["health"];
        }

        // Yeni saðlýk deðerini hesapla
        int newHealth = currentHealth + health;

        // Yeni saðlýk deðerini ayarla
        Hashtable playerProperties = player.CustomProperties;
        playerProperties["health"] = newHealth;
        player.SetCustomProperties(playerProperties);

        NotifyObservers();

    }

}
