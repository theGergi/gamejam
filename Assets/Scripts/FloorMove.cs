using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    private float speed = -0.5f;

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, speed);
    }
}
