﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnurMihyaz;
using System.Runtime.Remoting.Messaging;

public class User : MonoBehaviour
{
    public Client Client;
    IMove _movement;
    IAttack _attack;

    private void Start()
    {
        _movement = GetComponent<IMove>();
        _attack = GetComponent<IAttack>();
    }

    private void Update()
    {
        if(GetName() != null)
        {
            Client.SendMessageToServer(
                _movement.Move().ToString() + "#" + _movement.Rotate().ToString() + "#" + Shooting() + "#" + Reloading()
                + "#" + GetName() + "#" + GetClass(), "Move");
        }

    }

    private string Shooting() => _attack.isShooting ? _attack.Shoot() : _attack.StopShooting();

    private string Reloading() => _attack.isReloading ? _attack.Reload() : _attack.StopReload();
    
    private string GetName() => Client.MyName;

    private string GetClass() => Client.MyClass;

    public void StartShooting()  =>  _attack.isShooting = true;
    
    public void StopShooting()   =>  _attack.isShooting = false;

    public void StartReloading() =>  _attack.isReloading = true;

    public void StopReloading()  =>  _attack.isReloading = false;

}
