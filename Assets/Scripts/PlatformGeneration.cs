using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    public GameObject platform;
    //spawntime of platform
    private float spawnTime=1.75f;
    //yUp max y on y axe for random spawn, yDown for min on y axe (maybe 3, 0 for test).
    private float yMin=6, yMax=7;
    private float xPivot = -6, xDiff = 7;

    private float count = 1f;

    void Start()
    {

        Vector3 pos = new Vector3(xPivot, 1f, 0f);
        Instantiate(platform, pos, transform.rotation);

        UpdateXPivot();

        pos = new Vector3(xPivot, 4f, 0f);
        Instantiate(platform, pos, transform.rotation);

        UpdateXPivot();

        InvokeRepeating("platformSpawn", 0, spawnTime);
    }

    void Update()
    {
        
    }

    void UpdateXPivot()
    {
        xPivot = Random.Range(Mathf.Min(xPivot, xPivot + antiSign(xPivot) * xDiff), Mathf.Max(xPivot, xPivot + antiSign(xPivot) * xDiff));
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
        Vector3 pos = new Vector3(xPivot, yMin, 0);

        UpdateXPivot();

        //set platform in the world (transform.rotation as from main gameObject ("platformController")
        Instantiate(platform, pos, transform.rotation);
    }

}
