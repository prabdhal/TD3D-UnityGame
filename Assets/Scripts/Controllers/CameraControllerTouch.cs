using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class CameraControllerTouch : MonoBehaviour
    {
        Camera cam;

        public float panSpeed = 1f;
        public float zoomSpeed = 1f;
        [Header("Clamp X Pos")]
        public float minPosX = 230f;
        public float maxPosX = 275f;
        [Header("Clamp Y Pos")]
        public float minPosY = 5f;
        public float maxPosY = 30f;
        [Header("Clamp Z Pos")]
        public float minPosZ = 180f;
        public float maxPosZ = 215f;
        [Header("Clamp Zoom")]
        public float minZoom = 20f;
        public float maxZoom = 70f;

        public void Start()
        {
            if (cam == null)
                cam = Camera.main;
        }

        public void LateUpdate()
        {
            if (Input.touchCount.Equals(1) && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Translate(-touchDeltaPosition.x * panSpeed * Time.deltaTime, -touchDeltaPosition.y * panSpeed * Time.deltaTime, 0);

                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minPosX, maxPosX),
                    Mathf.Clamp(transform.position.y, minPosY, maxPosY),
                    Mathf.Clamp(transform.position.z, minPosZ, maxPosZ)); 
            }
            else if (Input.touchCount.Equals(2))
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                cam.fieldOfView += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;

                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
            }
            
        }
    }
}
