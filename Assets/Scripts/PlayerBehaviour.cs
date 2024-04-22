using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float EPSILON = 0.0005f;
    private float floorSpeed;

    private float horizontal;
    private float speed;
    private float jumpingPower;
    private bool isFacingLeft = true;

    private float deviationMargin = 3;
    private float randomDeviation = 0f;
    private float timeOfDeviation = -1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        speed = rb.gravityScale * 2f;
        jumpingPower = rb.gravityScale * 4f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (rb.velocity.y <= floorSpeed + EPSILON && rb.velocity.y >= floorSpeed - EPSILON && IsGrounded())
        {
            randomDeviation = Random.Range(-deviationMargin, deviationMargin);
            rb.velocity = new Vector2(rb.velocity.x + randomDeviation, jumpingPower);
            timeOfDeviation = Time.time;
        }

        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        floorSpeed = collision.transform.GetComponent<FloorMove>().speed;
    }

    private void FixedUpdate()
    {
        if (timeOfDeviation < Time.time - 0.5f)
        {
            randomDeviation = 0;
        }
        rb.velocity = new Vector2(horizontal * speed + randomDeviation, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingLeft && horizontal > 0f) || (!isFacingLeft && horizontal < 0f))
        {
            isFacingLeft = !isFacingLeft;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
