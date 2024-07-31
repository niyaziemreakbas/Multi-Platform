using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    UserData playerData;

    public WeaponType[] weaponTypes; // Tüm silahlar

    public int playerHealth = 3;

    public Image weaponImage; // Silah resmini göstermek için UI Image
    private int currentIndex = 0; // Þu anda seçili olan silahýn indeksi
    public TMP_InputField nickName;

    private void Start()
    {
        UpdateWeaponImage();
    }

    public void ApprovePreferences()
    {
        if(nickName == null)
        {
            Debug.Log("Enter A Nickname");
            return;
        }

        SetPlayerAttributes(nickName.text, playerHealth, weaponTypes[currentIndex]);

        SceneManager.LoadScene("Game");
    }
    public void SetPlayerAttributes(string name, int health, WeaponType weaponType)
    {
        playerData = ScriptableObject.CreateInstance<UserData>();
        playerData.nickName = name;
        playerData.health = health;
        playerData.weaponType=weaponType;

        PhotonNetwork.NickName = name;
        PlayerDataManager.Instance.AddPlayerData(name, playerData);
        Debug.Log($"Player : {name} added to Dictioanary");
        Debug.Log($"Its Health : {health}");
    }

    public void UpdateWeaponImage()
    {
        weaponImage.sprite = weaponTypes[currentIndex].weaponSprite;
    }

    public void NextWeapon()
    {
        currentIndex++;
        if (currentIndex >= weaponTypes.Length)
        {
            currentIndex = 0;
        }
        UpdateWeaponImage();
    }

    public void PreviousWeapon()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = weaponTypes.Length - 1;
        }
        UpdateWeaponImage();
    }
}
