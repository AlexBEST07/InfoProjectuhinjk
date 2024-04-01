using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeEntity : AttackingEntity {

    #region Setup

    public override void Awake() {        
        ammo = MAX_AMMO;
        if(GetComponent<InputHandler>() != null ) {
            HUDManager.Instance.UpdateAmmo(ammo, MAX_AMMO);
        }
    }

    #endregion

    #region RangeAttack

    [Header("RangeAttacks")]

    #region PrimaryButton

    public Transform bulletSpawn;

    [Header("Primary Attack")]

    public GameObject normalBullet;

    public float normalFireRate;
    public float normalDamage;
    public float normalSpread;

    public override void PrimaryButton() {
        Shoot(normalFireRate, normalBullet, normalDamage, normalSpread);
    }

    #endregion

    #region SecondaryButton

    [Header("Secondary Attack")]

    public GameObject secondaryBullet;

    public float secondaryFireRate;
    public float secondaryDamage;

    public override void SecondaryButton() {
        //will be overwritten anyways
    }

    #endregion

    #region Shoot

    float nextTimeToFire = 0f;

    public void Shoot(float fireRate, GameObject bullet, float damage = 0f, float spread = 0f) {        
        
        if (ammo > 0 && Time.time >= nextTimeToFire && !_isReloading) {
            if (GetComponent<SniperEntity>()) {
                GetComponent<SniperEntity>().hideSniperLaser = true;
            }
            
            nextTimeToFire = Time.time + 1f / fireRate;

            var shotBullet = Instantiate(bullet, bulletSpawn.position, transform.rotation);
            shotBullet.GetComponent<BulletComponent>().bulletDamage = damage;
            shotBullet.GetComponent<BulletComponent>().bulletSpread = spread;

            if (GetComponent<InputHandler>()) {
                AudioSystem.Instance.PlaySound(Sound.PlayerShoot);
                HUDManager.Instance.UpdateAmmo(ammo, MAX_AMMO);
            }

            ammo--;

            if (GetComponent<SniperEntity>()) {
                GetComponent<SniperEntity>().hideSniperLaser = false;
            }
        }
        else if(ammo == 0 && GetComponent<EnemyHandler>()) {
            StartCoroutine(Reload());
        }
    }

    public IEnumerator WaitForShoot() {
        yield return new WaitForSeconds(0.5f);
    }

    #endregion

    #endregion

    #region Reload

    [Header("Ammunition")]

    [SerializeField] private float MAX_AMMO;
    private bool _isReloading = false;

    [HideInInspector] public float ammo;

    public IEnumerator Reload() {
        if (_isReloading == false) {
            _isReloading = true;
            yield return new WaitForSeconds(1.5f);
            ammo = MAX_AMMO;
            _isReloading = false;
            if(GetComponent<InputHandler>()) {
                HUDManager.Instance.UpdateAmmo(ammo, MAX_AMMO);
            }
        }
    }

    #endregion

}
