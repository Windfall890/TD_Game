using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 20f;

    private Transform target;
    private int WaypointIndex = 0;

    public void Start()
    {
        target = Waypoints.points[WaypointIndex];
    }

    public void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if( Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            if (WaypointIndex >= Waypoints.points.Length-1)
            {
                Destroy(gameObject);
                return;
            }
            WaypointIndex++;
            target = Waypoints.points[WaypointIndex];       
        }
    }
}
