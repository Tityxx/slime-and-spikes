using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private float yPos = -3.5f;

    public Vector3 GetSidePosition(Vector3 playerPos, Vector3 innerSide, bool left)
    {
        Vector3 pos = left ? leftPoint.position : rightPoint.position;
        pos += playerPos - innerSide;
        pos.y = yPos;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 pos = Vector3.zero;
        pos.y = yPos;
        Gizmos.DrawSphere(pos, 0.2f);
    }
}