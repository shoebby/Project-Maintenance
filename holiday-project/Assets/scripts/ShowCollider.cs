using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCollider : MonoBehaviour
{
    new public bool enabled = true;
    public BoxCollider boxVolume;
    public Vector3 colliderCenter = Vector3.zero;
    public Vector3 colliderSize = Vector3.one;
    public float rotation;

    public Color colliderColor;

    void OnDrawGizmos()
    {
        if (!enabled) return;

        colliderCenter = boxVolume.center;
        colliderSize = boxVolume.size;

        Gizmos.color = colliderColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(colliderCenter, colliderSize);
    }
}
