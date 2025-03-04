using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LocalPlayerUI : MonoBehaviour, IObserver
{
    public TextMeshProUGUI healthText;

    //UserData playerData;
    Player localPlayer = PhotonNetwork.LocalPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = PhotonNetwork.LocalPlayer;

        PlayerDataManager.Instance.RegisterObserver(this);

        UpdateHealth(); 
    }

    public void OnDataChanged()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthText.text = PlayerDataManager.Instance.GetHealth(localPlayer).ToString();
    }
}
