using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1,0);
    public Vector2 velocity;
    public float speed = 2;
    public bool isEnemy = false;
    public bool isEnergyBall = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemy)
        {
            Destroy(gameObject, 3);
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        position += velocity * Time.fixedDeltaTime;

        transform.position = position;
    }

}
