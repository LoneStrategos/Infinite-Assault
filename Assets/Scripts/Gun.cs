using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    Vector2 direction;
    public bool isActive = false;
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0f;
    float shootTimer = 0f;
    float delayTimer = 0f;
    
    public AudioClip PlayerGun;
    public AudioSource GunShooting;

    public int powerUpGunRequirement = 0;
    public GameObject muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        GunShooting = GetComponent<AudioSource>();
        if (GunShooting == null)
        {
            
        }
        else
        {
            GunShooting.clip = PlayerGun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                shootTimer += Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        GunShooting.PlayOneShot(PlayerGun);

        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }

}
