using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GunClasses
{
    Rifle,
    Shotgun,
    Handgun,
    Laser
}
public abstract class Gun
{
    public float FireRate;
    public float NextFire;
    public float Damage;
    public float Speed;
    public int ClipSize;
    public int Ammo;
    public int SpreadCount;

    public Gun(float fireRate, float damage, float nextFire, float speed, int clipSize, int ammo, int spreadCount)
    {
        FireRate = fireRate;
        Damage = damage;
        ClipSize = clipSize;
        Speed = speed;
        Ammo = ammo;
        SpreadCount = spreadCount;
        NextFire = nextFire;
    }
}

public class Rifle : Gun
{
    public Rifle(
    float fireRate = 0.1f,
    float damage = 5,
    float nextFire = 0.0f,
    float speed = 100f,
    int clipSize = 20,
    int ammo = 20,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {

    }
}

public class Shotgun : Gun
{
    public Shotgun(
    float fireRate = 2f,
    float damage = 15,
    float nextFire = 0.0f,
    float speed = 75,
    int clipSize = 1,
    int ammo = 1,
    int spreadCount = 5) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {


    }

}
public class Handgun : Gun
{
    public Handgun(
    float fireRate = 0.75f,
    float damage = 35,
    float nextFire = 0.0f,
    float speed = 100f,
    int clipSize = 6,
    int ammo = 6,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {

    }
}
public class Laser : Gun
{
    public Laser(
    float fireRate = 2.5f,
    float damage = 100,
    float nextFire = 0.0f,
    float speed = 200f,
    int clipSize = 1,
    int ammo = 1,
    int spreadCount = 1) :
    base(fireRate, damage, nextFire, speed, clipSize, ammo, spreadCount)
    {

    }

}