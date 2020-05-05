using OnurMihyaz;
using UnityEngine;

public class ControllerSystem : MonoBehaviour, IAttack
{
    public Gun Gun;

    public bool isShooting { get; set; }
    public bool isReloading { get; set; }

    public string Reload()
    {
        StartCoroutine(MihyazDelay.Delay(1f, () =>
        {
            isReloading = false;
            Gun.Ammo = Gun.ClipSize;
        }));
        return "";
    }

    public void Shoot()
    {
        isShooting = true;
    }

    public string StopReload()
    {
        throw new System.NotImplementedException();
    }

    public string StopShooting()
    {
        throw new System.NotImplementedException();
    }

    public void StopShoot()
    {
        isShooting = false;
    }

    string IAttack.Shoot()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (!isReloading && Gun != null)
        {
            if (Gun.Ammo <= 0 && Gun.Ammo != Gun.ClipSize)
            {
                isReloading = true;
                Reload();
                return;
            }

            if (isShooting && Time.time > Gun.NextFire)
            {
                Gun.NextFire = Time.time + Gun.FireRate;
                SoundManager.Instance.PlayShot();

                --Gun.Ammo;
            }
        }
    }
}
