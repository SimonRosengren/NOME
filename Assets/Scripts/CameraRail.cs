using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRail : MonoBehaviour
{
    Vector3[] nodes;
    int nodeCount;

	void Start ()
    {
        nodeCount = transform.childCount;
        nodes = new Vector3[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            nodes[i] = transform.GetChild(i).position;
        }
	}
	
	void Update ()
    {
        ///*ONLY FOR DEBUGGING*/
        //if (nodeCount > 1)
        //{
        //    for (int i = 0; i < nodeCount ; i++)
        //    {
        //        Debug.DrawLine(nodes[i], nodes[i + 1], Color.green);
        //    }
        //}
	}

    public Vector3 ProjectPositionOnRail(Vector3 player)
    {
        int closestNodeIndex = GetClosestNode(player);

        if (closestNodeIndex == 0)
        {
            return ProjectOnSegtment(nodes[0], nodes[1], player);
        }
        else if(closestNodeIndex == nodeCount - 1)
        {
            return ProjectOnSegtment(nodes[nodeCount - 1], nodes[nodeCount - 2], player);
        }
        else
        {
            Vector3 leftSeg = ProjectOnSegtment(nodes[closestNodeIndex - 1], nodes[closestNodeIndex], player);
            Vector3 rightSeg = ProjectOnSegtment(nodes[closestNodeIndex + 1], nodes[closestNodeIndex], player);

            Debug.DrawLine(player, leftSeg, Color.red);
            Debug.DrawLine(player, rightSeg, Color.yellow);

            if ((player - leftSeg).sqrMagnitude <= (player - rightSeg).sqrMagnitude)
            {
                return leftSeg;
            }
            else
                return rightSeg;
        }
    }

    private int GetClosestNode(Vector3 pos)
    {
        int closestNodeIndex = -1;
        float shortestNodeDistance = 0.0f;

        for (int i = 0; i < nodeCount; i++)
        {
            float squareDistance = (nodes[i] - pos).sqrMagnitude;

            if (shortestNodeDistance == 0.0f || squareDistance < shortestNodeDistance)
            {
                shortestNodeDistance = squareDistance;
                closestNodeIndex = i;
            }
        }
        return closestNodeIndex;
    }

    private Vector3 ProjectOnSegtment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(segDirection, v1ToPos);

        if (distanceFromV1 < 0.0f)
        {
            return v1;
        }

        else if (distanceFromV1*distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }

        else
        {
            Vector3 fromV1 = segDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }
}
