using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeManager : MonoBehaviour, IObserver
{
    public static WeaponTypeManager Instance;

    public List<WeaponType> WeaponTypes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Oyun boyunca saklanmas�n� sa�la
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdatePlayersWeaponSprite();
        PlayerDataManager.Instance.RegisterObserver(this);
    }

    public void OnDataChanged()
    {
        UpdatePlayersWeaponSprite();
    }

    public WeaponType GetWeaponTypeByName(string weaponName)
    {
        foreach (WeaponType weaponType in WeaponTypes)
        {
            if (weaponType.name == weaponName)
            {
                return weaponType;
            }
        }
        return null;
    }


    public void UpdatePlayersWeaponSprite()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("weaponType"))
            {
                GameObject playerGameObject = GetPlayerGameObject(player);
                WeaponType weaponType = PlayerDataManager.Instance.GetWeaponType(player);

                if (playerGameObject != null)
                {
                    playerGameObject.GetComponent<SpriteRenderer>().sprite = weaponType.weaponSprite;
                }
            }
        }
    }

    private GameObject GetPlayerGameObject(Player player)
    {

        // BU FONKS�YON OYUNCUNUN S�LAH OBJES�N� D�ND�R�YOR SONRASINDA PROBLEM �IKAB�L�R


        // T�m oyuncular�n PhotonView bile�enlerini bul
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        foreach (PhotonView view in photonViews)
        {
            if (view.Owner == player)
            {
                Debug.Log("getplayergame object");
                PlayerDataManager.Instance.GetPlayerData(player);
                // E�er bu PhotonView, arad���m�z oyuncuya aitse, GameObject'ini d�nd�r
                return view.gameObject;
            }
        }
        return null;
    }
}
