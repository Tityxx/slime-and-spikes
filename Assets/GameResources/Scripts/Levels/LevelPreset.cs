using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreset : MonoBehaviour
{
    public Vector3 TopPosition => topPoint.position;
    public Vector3 DownPosition => downPoint.position;

    [SerializeField]
    private Transform topPoint;
    [SerializeField]
    private Transform downPoint;

    public Vector3 GetOffset(bool top)
    {
        if (top) return TopPosition - transform.position;
        else return transform.position - DownPosition;
    }
}