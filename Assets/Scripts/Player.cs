using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Gun[] guns;

    Vector2 startPos;
    public float moveSpeed = 5;

    int hits = 3;
    bool invincible = false;
    float invincibleTimer = 0;
    float invincibleDuration = 2;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool shoot;
    private float shootTimer = 0;
    public float fireRate = 10f;

    SpriteRenderer spriteRenderer;
    Score score;
    Level level;
    GameObject shield;
    int powerUpGuns = 0;
    
    private Animator anim;

    private void Awake()
    {
        startPos = transform.position;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();



        score = FindObjectOfType<Score>();
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
        guns = transform.GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.powerUpGunRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        shoot = Input.GetKey(KeyCode.Space);

        shootTimer += Time.deltaTime;

        if (moveRight)
        {
            anim.SetBool("Jett_Anima_schnell", true);
        }

        else
        {
            anim.SetBool("Jett_Anima_schnell", false);
        }

        if (moveLeft)
        {
            anim.SetBool("Jett_Anima_langsam", true);
        }

        else
        {
            anim.SetBool("Jett_Anima_langsam", false);
        }

        if (invincible)
        {
            if (invincibleTimer >= invincibleDuration)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }

        if (shoot && shootTimer >= (1/fireRate))
        {
            shootTimer = 0;
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        position += move;

        if (position.x <= 1.5f)
        {
            position.x = 1.5f;
        }

        if (position.x >= 16f)
        {
            position.x = 16f;
        }

        if (position.y <= 1f)
        {
            position.y = 1f;
        }

        if (position.y >= 9f)
        {
            position.y = 9f;
        }

        transform.position = position;
    }

    void AddGuns()
    {
        powerUpGuns++;
        foreach (Gun gun in guns)
        {
            if (gun.powerUpGunRequirement <= powerUpGuns)
            {
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }

    void ResetPlayer()
    {
        transform.position = startPos;
        DeactivateShield();
        powerUpGuns = -1;
        AddGuns();
        hits = 3;
    }

    void Hit(GameObject gameObjectHit)
    {
        if (HasShield())
        {
            DeactivateShield();
        }
        else
        {
            if (!invincible)
            {
                hits--;
                if (hits == 0)
                {
                    ResetPlayer();
                }
                else
                {
                    invincible = true;
                }
                Destroy(gameObjectHit);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Hit(bullet.gameObject);
            }
        }

        Destructible destructible = collision.GetComponent<Destructible>();
        if (destructible != null)
        {
            Hit(destructible.gameObject);
        }

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
            }
            if (powerUp.addGuns)
            {
                AddGuns();
            }
            score.AddScore(powerUp.pointValue);
            Destroy(powerUp.gameObject);
        }
    }

}
