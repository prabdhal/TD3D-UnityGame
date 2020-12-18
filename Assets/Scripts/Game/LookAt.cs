using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class LookAt : MonoBehaviour
    {
        Camera cam;
        Transform obj;

        private void Start()
        {
            cam = Camera.main;
            obj = GetComponent<Transform>();
        }
        private void Update()
        {
            obj.LookAt(cam.transform);
        }
    }
}
