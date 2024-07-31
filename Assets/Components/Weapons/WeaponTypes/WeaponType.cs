using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType")]
public class WeaponType : ScriptableObject
{
    public float bulletCapacity;
    public float reloadSpeed;
    public Sprite weaponSprite;
    public string weaponName;
    
    public GameObject bulletPrefab;
}
