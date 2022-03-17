using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlowDev.Pathfinding;
using System;

using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    Vector3[] path;
    int targetIndex;

    public Transform target;
    public float speed = 5;

    [SerializeField] bool debugPath;
    [SerializeField] InputAction requestPathHotkey;

    void RequestPath(InputAction.CallbackContext callback)
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void OnEnable()
    {
        requestPathHotkey.Enable();
        requestPathHotkey.performed += RequestPath;
    }

    private void OnDisable()
    {
        requestPathHotkey.Disable();
    }

    Coroutine FollowPathCoroutine;

    private void OnPathFound(Vector3[] newPath, bool pathSucessful)
    {
        if (pathSucessful)
        {
            path = newPath;
            targetIndex = 0;
            if (FollowPathCoroutine != null) {
                StopCoroutine(FollowPathCoroutine);
            }
            FollowPathCoroutine = StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length == 0) yield break;

        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null && debugPath)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
