using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Waypoints : MonoBehaviour
    {
        public static Transform[] points;
        public static Transform[] points01;
        public static Transform[] points02;
        public static Transform[] points03;
        public static Transform[] points04;
        public Transform waypoints;
        public Transform waypoints01;
        public Transform waypoints02;
        public Transform waypoints03;
        public Transform waypoints04;

        void Awake()
        {
            points = new Transform[waypoints.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = waypoints.GetChild(i);
            }

            if (waypoints01 == null)
                return;
            points01 = new Transform[waypoints01.childCount];
            for (int i = 0; i < points01.Length; i++)
            {
                points01[i] = waypoints01.GetChild(i);
            }

            if (waypoints02 == null)
                return;
            points02 = new Transform[waypoints02.childCount];
            for (int i = 0; i < points02.Length; i++)
            {
                points02[i] = waypoints02.GetChild(i);
            }

            if (waypoints03 == null)
                return;
            points03 = new Transform[waypoints03.childCount];
            for (int i = 0; i < points03.Length; i++)
            {
                points03[i] = waypoints03.GetChild(i);
            }

            if (waypoints04 == null)
                return;
            points04 = new Transform[waypoints04.childCount];
            for (int i = 0; i < points04.Length; i++)
            {
                points04[i] = waypoints04.GetChild(i);
            }
        }
    }
}
