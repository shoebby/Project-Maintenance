using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfClockScript : MonoBehaviour
{
    private float elapsedTime = 0f;

    public RectTransform handAnchor;

    private float handStartAngle = 45f;
    private float angleMax = 360f;

    public float anglePerSecond;
    public float angleCounter;
    public float totalMinutes;

    private void Start()
    {
        angleCounter = 0f;
        anglePerSecond = angleMax / (totalMinutes * 60);
        handAnchor.rotation = handAnchor.rotation * Quaternion.AngleAxis(handStartAngle, Vector3.back);
    }

    void Update()
    {
        if (angleCounter >= angleMax)
        {
            gameObject.SetActive(false);
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1f)
        {
            elapsedTime %= 1f;
            ProgressHand(anglePerSecond, Vector3.back, 0);
        }
    }

    public void ProgressHand(float angle, Vector3 axis, float inTime)
    {
        float rotationSpeed = angle / inTime;

        Quaternion startRotation = handAnchor.rotation;

        float deltaAngle = 0;

        // rotate until reaching angle
        while (deltaAngle < angle)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            handAnchor.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);
        }
        
        angleCounter += anglePerSecond;
    }

    public void ResetClock()
    {
        handAnchor.transform.Rotate(0f, 0f, angleCounter);
        angleCounter = 0f;
    }
}