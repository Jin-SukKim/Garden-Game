using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineGunController : MonoBehaviour {
    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    private float lastShot = 0;

    public Transform firePoint;

    //The bar that displays remaining mana
    public Image manabar;

    //The maximum amount of mana the player can have
    public float maxMana = 10;

    //The player's current mana
    public float mana;

    // Start is called before the first frame update
    void Start() {
        if(manabar)
        {
            mana = maxMana;
        }
    }

    // Update is called once per frame
    void Update() {
        if (isFiring) {
            if (Time.time > timeBetweenShots + lastShot) {
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                lastShot = Time.time;

                if(manabar)
                {
                    mana--;
                    manabar.fillAmount = mana/maxMana;
                }    
            }
        }
    }
}
