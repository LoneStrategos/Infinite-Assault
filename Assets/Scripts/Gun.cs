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

    AudioClip[] PlayerGun;
    private AudioSource GunShooting;

    public void Awake()
    {
        GunShooting = GameObject.Find("GunShooting").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    //void PlayGunSound()
    //{
        //int shotSound = Random.Range(1, PlayerGun.Length);
        //GunShooting.clip = PlayerGun[shotSound];
        //GunShooting.PlayOneShot(GunShooting.clip);

        //PlayerGun[shotSound] = PlayerGun[0];
        //PlayerGun[0] = GunShooting.clip;
    //}

    public void Shoot()
    {
        //PlayGunSound();

        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
    }

}
