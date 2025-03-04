using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListUI : MonoBehaviour, IObserver
{
    public Transform playerRowContainer;

    public GameObject playerListPanel;

    public GameObject RowObjectPrefab;

    private void Start()
    {
        PlayerDataManager.Instance.RegisterObserver(this);

        UpdatePlayerList();

    }

    public void OnDataChanged()
    {
        UpdatePlayerList();
    }

    private void Awake()
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerListPanel.SetActive(!playerListPanel.activeSelf);  // Tab tuþuna basýldýðýnda paneli aç/kapa
        }
    }

    void UpdatePlayerList()
    {
        // Mevcut listeyi temizle
        foreach (Transform child in playerRowContainer)
        {
            Destroy(child.gameObject);
        }

        // Her bir oyuncu için yeni bir satýr oluþtur
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject newRow = Instantiate(RowObjectPrefab, playerRowContainer);
            RowObject rowScript = newRow.GetComponent<RowObject>();
            rowScript.UpdatePlayerProperties(player);
        }

    }
}
