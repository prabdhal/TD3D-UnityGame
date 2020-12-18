using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Arrows : MonoBehaviour
    {
        public static Arrows instance;
        public static Transform[] arrows;
        public List<WaypointArrow> arrowScript = new List<WaypointArrow>();

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of Arrows found in scene!");
                return;
            }

            instance = this;

            arrows = new Transform[transform.childCount];
            
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i] = transform.GetChild(i);

                WaypointArrow a = arrows[i].GetComponent<WaypointArrow>();
                arrowScript.Add(a);
            }
        }
    }
}