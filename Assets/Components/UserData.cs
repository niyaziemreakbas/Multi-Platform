using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UserData")]
public class UserData : ScriptableObject
{
    public int health;

    public WeaponType weaponType;

    public string nickName;
}
