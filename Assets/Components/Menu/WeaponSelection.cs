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
    //public WeaponType[] weaponTypes; // Tüm silahlar

    int playerHealth = 3;

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

        PlayerDataManager.Instance.SetPlayerData(nickName.text, WeaponTypeManager.Instance.WeaponTypes[currentIndex].weaponName, playerHealth);

        SceneManager.LoadScene("Game");
    }

    public void UpdateWeaponImage()
    {
        weaponImage.sprite = WeaponTypeManager.Instance.WeaponTypes[currentIndex].weaponSprite;
    }

    public void NextWeapon()
    {
        currentIndex++;
        if (currentIndex >= WeaponTypeManager.Instance.WeaponTypes.Count)
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
            currentIndex = WeaponTypeManager.Instance.WeaponTypes.Count - 1;
        }
        UpdateWeaponImage();
    }
}
