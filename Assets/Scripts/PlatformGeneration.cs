using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    public GameObject platform;
    //spawntime of platform
    private float spawnTime=1.5f;
    //yUp max y on y axe for random spawn, yDown for min on y axe (maybe 3, 0 for test).
    private float yMin=6, yMax=7;
    private float xPivot = -6, xDiff = 8;

    private float tempxPivot;

    void Start()
    {

        Vector3 pos = new Vector3(xPivot, 1f, 0f);
        Instantiate(platform, pos, transform.rotation);

        xPivot = Random.Range(xPivot, xPivot + antiSign(xPivot) * xDiff);

        pos = new Vector3(xPivot, 4f, 0f);
        Instantiate(platform, pos, transform.rotation);

        xPivot = Random.Range(xPivot, xPivot + antiSign(xPivot) * xDiff);

        /*pos = new Vector3(xPivot, 4f, 0f);
        Instantiate(platform, pos, transform.rotation);*/

        xPivot = Random.Range(xPivot, xPivot + antiSign(xPivot) * xDiff);

        InvokeRepeating("platformSpawn", 0, spawnTime);
    }

    float antiSign(float x)
    {
        if (x < 0)
            return 1f;
        return -1f;
    }

    //funktion for spawn
    void platformSpawn()
    {
        //random point on y axe for spawn
        float y = Random.Range(yMin, yMax);

        //make vector3 for spawn position (i set z to zero)
        Vector3 pos = new Vector3(xPivot, y, 0);

        xPivot = Random.Range(xPivot, xPivot + antiSign(xPivot) * xDiff);

        //set platform in the world (transform.rotation as from main gameObject ("platformController")
        Instantiate(platform, pos, transform.rotation);
    }

}
