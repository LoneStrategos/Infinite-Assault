using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        position.x -= moveSpeed * Time.fixedDeltaTime;

        if (position.x < -2)
        {
            Destroy(gameObject);
        }

        transform.position = position;
    }
}
