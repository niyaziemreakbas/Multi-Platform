using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListRowChanger : MonoBehaviour
{
    public TextMeshProUGUI nickName;

    public TextMeshProUGUI health;

    public TextMeshProUGUI weaponName;

    UserData userData;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerProperties();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        UpdatePlayerProperties();
    }

    void UpdatePlayerProperties()
    {
        weaponName.text = userData.weaponType.weaponName;
        health.text = userData.health.ToString();
        nickName.text = userData.weaponType.weaponName;

    }
}
