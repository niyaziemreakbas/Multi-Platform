using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RowObject : MonoBehaviour 
{
    public TextMeshProUGUI nickName;

    public TextMeshProUGUI health;

    public Image weaponImage;

     

    public void UpdatePlayerProperties(Player player)
    {
        WeaponType weaponType = PlayerDataManager.Instance.GetWeaponType(player);

        weaponImage.sprite = weaponType.weaponSprite;
        health.text = PlayerDataManager.Instance.GetHealth(player).ToString();
        nickName.text = PlayerDataManager.Instance.GetPlayerName(player);

    }
}
