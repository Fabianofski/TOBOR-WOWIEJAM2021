using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Transform[] waypoints;
    int currentWaypoint;
    [SerializeField] float speed;
    [SerializeField] float CheckpointWaittime;
    bool Moving = true;
    [SerializeField] bool MoveX;
    [SerializeField] bool MoveY;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if(animator)
            animator.SetBool("Walking", Moving);

        if (!Moving) return;

        Vector3 target = waypoints[currentWaypoint].position;
        if (!MoveX)
            target.x = transform.position.x;
        if(!MoveY)
            target.y = transform.position.y;

        if (transform.position == target)
        {
            Moving = false;
            Invoke("NextWaypoint", CheckpointWaittime);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }

    private void NextWaypoint()
    {
        Moving = true;

        if (currentWaypoint < waypoints.Length - 1)
            currentWaypoint++;
        else
            currentWaypoint = 0;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoints[i].position, 0.1f);
            if(i < waypoints.Length - 1)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }

}
