using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageWave : MonoBehaviour
{
    public GameObject apple;
    public float speed;

    public void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) - 5.5f;
        apple.transform.position = new Vector3(apple.transform.position.x, y, 0);
    }
}
