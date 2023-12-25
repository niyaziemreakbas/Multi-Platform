using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    
[CreateAssetMenu(menuName = "Bullet")]

public class Bullet : ScriptableObject
{
    public GameObject bulletPrefab;

    public float bulletSpeed;

    public float pushPower;

    public float fireSpeed;

    public float reloadSpeed;
}
