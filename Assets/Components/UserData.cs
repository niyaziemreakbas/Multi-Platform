using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData 
{
    private List<IObserver> observers = new List<IObserver>();

    public int health;

    public string weaponType;

    public string nickName;

    public UserData(string name, int health, string weaponType)
    {
        nickName = name;
        this.health = health;
        this.weaponType = weaponType;
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnDataChanged();
        }
    }
    
    public void ChangeHealth(int newHealth)
    {
        health += newHealth;
        NotifyObservers();
    }

    public void SetNickName(string newNickName)
    {
        nickName = newNickName;
        NotifyObservers();
    }
}
