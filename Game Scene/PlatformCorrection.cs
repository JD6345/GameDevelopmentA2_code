using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCorrection : MonoBehaviour
{
    //this script is entirely based around the possibility of the screen size being smaller than the defined value in GeneratePlatforms.cs

    float platformWidth;
    public float minWidthPercent;
    public float maxWidthPercent;
    Vector3 vectorScaler = new Vector3(0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        platformWidth = Random.Range(minWidthPercent, maxWidthPercent);
        vectorScaler.x = platformWidth;
        transform.localScale = vectorScaler;
    }

    // Update is called once per frame
    void Update()
    {
        //see CharacterScript "wrapping player from one side of the screen to the other" for details
        Vector3 platformPos = Camera.main.WorldToViewportPoint(transform.position);
        if ( ( platformPos.x + platformWidth ) <= 0)
        {
            transform.Translate(Vector3.right); //subtly shift platform to the right to ensure it is visible
        }
        else if ( ( platformPos.x - platformWidth ) >= 1)
        {
            transform.Translate(Vector3.left); //subtly shift platform to the left to ensure it is visible
        }
    }
}
