using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class WaypointArrow : MonoBehaviour
    {
        private Transform target;
        private int waypointIndex = 0;

        public Transform startingPoint;
        public float speed;
        public float rotSpeed;
        public float accuracy = 2;
        public Vector3 offset = new Vector3(0, 1, 0);

        public int route;
        private Transform[] waypoints;

        [HideInInspector]
        public bool stop;

        void Start()
        {
            gameObject.SetActive(true);
            startingPoint.position = transform.position;

            if (route.Equals(0))
            {
                waypoints = Waypoints.points;
            }
            else if (route.Equals(1))
            {
                waypoints = Waypoints.points01;
            }
            else if (route.Equals(2))
            {
                waypoints = Waypoints.points02;
            }
            else if (route.Equals(10))
            {
                waypoints = Waypoints.points03;
            }
            else if (route.Equals(11))
            {
                waypoints = Waypoints.points04;
            }
            target = waypoints[0];
        }

        void Update()
        {
            if (stop)
            {
                gameObject.SetActive(false);
                return;
            }

            if (Vector3.Distance(target.position, transform.position) < accuracy)
            {
                GetNextWaypoint();
            }

            Vector3 dir = (target.position + offset) - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        private void GetNextWaypoint()
        {
            if (waypointIndex >= waypoints.Length - 1)
            {
                EndPath();
                return;
            }

            waypointIndex++;
            target = waypoints[waypointIndex];
        }

        private void EndPath()
        {
            transform.position = startingPoint.position;
            waypointIndex = -1;
            GetNextWaypoint();
        }
    }
}