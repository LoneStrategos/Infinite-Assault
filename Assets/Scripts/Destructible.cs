using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int hitPoints = 1;
    public int pointValue = 100;
    public int powerUpDropRate = 25;
    public GameObject explosion;
    public GameObject[] powerUpPrefabs;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 20f)
        {
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }
        }

        if (transform.position.x < 17f && !canBeDestroyed)
        {
            canBeDestroyed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                
                if (bullet.isEnergyBall)
                {
                    score.AddScore(pointValue);
                    DestroyEnemy();
                }
                else
                {
                    hitPoints--;
                    Destroy(bullet.gameObject);

                    if (hitPoints <= 0)
                    {
                        score.AddScore(pointValue);
                        DestroyEnemy();
                        Destroy(bullet.gameObject);

                        int dropPowerUp = Random.Range(0, 100);

                        if (dropPowerUp < powerUpDropRate)
                        {
                            int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
                            GameObject powerUpToSpawn = powerUpPrefabs[randomPowerUp];
                            Instantiate(powerUpToSpawn, transform.position, Quaternion.identity);
                        }
                    }
                }               
            }
        }
    }

    void DestroyEnemy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
