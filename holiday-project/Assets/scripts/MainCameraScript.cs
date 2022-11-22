using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetPos;
    public float lerpSpeed;
    public bool lerp;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        lerp = false;
        lerpSpeed = 0.02f;
        targetPos = gameObject.transform.position;
    }

    void Update()
    {
        //set the position of the camera, either lerp or not depending on a bool
        if (lerp)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed);
        }
        else if (!lerp)
        {
            transform.position = targetPos;
        }

        //set the rotation of the camera after setting position to prevent flickering during a hard cut
        transform.LookAt(target.transform.position);
    }
}
