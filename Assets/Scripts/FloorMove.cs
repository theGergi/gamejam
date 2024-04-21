using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public float speed = -0.5f;

    [SerializeField] private Rigidbody2D rb;

    private float count = 1;

    // Update is called once per frame
    void Update()
    {

        if (Time.unscaledTime / 10 > count)
        {
            count += 1;
            /*speed = (-1f)*(count + 1f);*/
            Time.timeScale = Mathf.Min(2f, Mathf.Max(1f, Time.unscaledTime/20)); 
        }

        rb.velocity = new Vector2(0, speed);

        if (transform.position.y < -8)
            Destroy(gameObject);
    }
}
