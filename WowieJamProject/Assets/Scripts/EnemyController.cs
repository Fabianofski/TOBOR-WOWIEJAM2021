using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    public class WaypointSystem
    {
        public Transform waypoint;
        public float waitTime;
    }

    [SerializeField] WaypointSystem[] waypoints;
    int currentWaypoint;
    [SerializeField] float speed;
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

        Vector3 target = waypoints[currentWaypoint].waypoint.position;
        if (!MoveX)
            target.x = transform.position.x;
        if(!MoveY)
            target.y = transform.position.y;

        if (transform.position == target)
        {
            Moving = false;
            Invoke("NextWaypoint", waypoints[currentWaypoint].waitTime);
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
            Gizmos.DrawSphere(waypoints[i].waypoint.position, 0.1f);
            if(i < waypoints.Length - 1)
                Gizmos.DrawLine(waypoints[i].waypoint.position, waypoints[i + 1].waypoint.position);
        }
    }

}
