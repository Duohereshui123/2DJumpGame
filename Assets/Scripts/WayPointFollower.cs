﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make platform can move on the path with way points 
public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    private int currentWaypointIndex = 0;

    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position,transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
