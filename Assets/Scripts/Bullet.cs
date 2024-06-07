﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > 6.0f)
        {
            Destroy(gameObject);
        }
    }
}