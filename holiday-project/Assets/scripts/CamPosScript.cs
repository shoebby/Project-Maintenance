using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosScript : MonoBehaviour
{
    public Transform camPos;
    private static PlayerScript player;
    private static MainCameraScript mainCam;

    public bool lerpTo = false;
    public float setLerpSpeed = 0.02f;

    public bool follow = false;
    public float followMaxDist = 3f;
    public float followMinDist = 3f;

    private float clampedXpos;
    private float clampedYpos;
    private float clampedZpos;

    public enum clampSetting {noClamp, X, Y, Z, XY, XZ, YZ }
    public clampSetting clamp;

    private void Start()
    {
        mainCam = Camera.main.transform.GetComponent<MainCameraScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerScript>();

        clampedXpos = Mathf.Clamp(camPos.position.x, camPos.position.x, camPos.position.x);
        clampedYpos = Mathf.Clamp(camPos.position.y, camPos.position.y, camPos.position.y);
        clampedZpos = Mathf.Clamp(camPos.position.z, camPos.position.z, camPos.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (lerpTo)
            {
                mainCam.lerp = true;
                mainCam.lerpSpeed = setLerpSpeed;
            }
            else
                mainCam.lerp = false;

            if (follow)
            {
                Debug.DrawLine(player.transform.position, camPos.position, Color.blue);
                var distance = Vector3.Distance(player.transform.position, camPos.position);

                if (distance >= followMaxDist)
                    camPos.position = (camPos.position - player.transform.position).normalized * followMaxDist + player.transform.position;
                if (distance <= followMinDist)
                    camPos.position = (camPos.position - player.transform.position).normalized * followMaxDist + player.transform.position;

                switch (clamp)
                {
                    case clampSetting.X:
                        camPos.position = new Vector3(clampedXpos, camPos.position.y, camPos.position.z);
                        break;
                    case clampSetting.Y:
                        camPos.position = new Vector3(camPos.position.x, clampedYpos, camPos.position.z);
                        break;
                    case clampSetting.Z:
                        camPos.position = new Vector3(camPos.position.x, camPos.position.y, clampedZpos);
                        break;
                    case clampSetting.XY:
                        camPos.position = new Vector3(clampedXpos, clampedYpos, camPos.position.z);
                        break;
                    case clampSetting.XZ:
                        camPos.position = new Vector3(clampedXpos, camPos.position.y, clampedZpos);
                        break;
                    case clampSetting.YZ:
                        camPos.position = new Vector3(camPos.position.x, clampedYpos, clampedZpos);
                        break;
                    case clampSetting.noClamp:
                        break;
                }
            }

            mainCam.targetPos = camPos.position;
        }
    }
}
