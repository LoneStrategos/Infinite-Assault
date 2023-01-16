using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSinWave : MonoBehaviour
{
    float sinCenterY;
    public float amplitude = 2;
    public float frequency = 0.5f;
    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        float sin = Mathf.Sin(position.x * frequency) * amplitude;
        if (inverted)
        {
            sin *= -1;
        }
        position.y = sinCenterY + sin;

        transform.position = position;
    }
}
