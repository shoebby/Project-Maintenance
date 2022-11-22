using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class DoorScript : MonoBehaviour
{
    private float openAngle;
    private Usable usableScript;
    public bool isLocked;

    private void Start()
    {
        openAngle = 90f;
        usableScript = GetComponent<Usable>();
        if (isLocked)
        {
            usableScript.overrideUseMessage = "Locked";
        }
    }

    public void ToggleDoor()
    {
        if (isLocked)
        {
            return;
        }

        transform.rotation = transform.rotation * Quaternion.AngleAxis(openAngle, Vector3.forward);
        openAngle *= -1f;

        if (openAngle < 0)
        {
            usableScript.overrideUseMessage = "(E) Close";
        } else if (openAngle > 0)
        {
            usableScript.overrideUseMessage = "(E) Open";
        }
        usableScript.enabled = false;
        usableScript.enabled = true;
    }
}
