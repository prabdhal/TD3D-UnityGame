using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class CameraController : MonoBehaviour
    {
        public Transform[] camAngles;

        private KeyCode[] clockwiseMovement = new KeyCode[2];
        private KeyCode[] counterClockwiseMovement = new KeyCode[2];

        int index;
        bool canRotate;


        public void Start()
        {
            //gameManager = GameManager.instance;
            //lastFrameTime = Time.realtimeSinceStartup;
            index = 0;
            canRotate = true;
            transform.position = camAngles[0].position;
            transform.rotation = camAngles[0].rotation;
            clockwiseMovement[0] = KeyCode.RightArrow;
            clockwiseMovement[1] = KeyCode.D;
            counterClockwiseMovement[0] = KeyCode.LeftArrow;
            counterClockwiseMovement[1] = KeyCode.A;
        }

        public void LateUpdate()
        {
            if (Input.GetKey(clockwiseMovement[0]) && canRotate
                || Input.GetKey(clockwiseMovement[1]) && canRotate)
            {
                RightCamViewButton();

                canRotate = false;
            }
            else if (Input.GetKey(counterClockwiseMovement[0]) && canRotate 
                || Input.GetKey(counterClockwiseMovement[1]) && canRotate)
            {
                LeftCamViewButton();

                canRotate = false;
            }
            else if (Input.GetKeyUp(clockwiseMovement[0]) || Input.GetKeyUp(counterClockwiseMovement[0]) 
                || Input.GetKeyUp(clockwiseMovement[1]) || Input.GetKeyUp(counterClockwiseMovement[1]))
            {
                canRotate = true;
            }
        }

        public void RightCamViewButton()
        {
            index++;

            if (index > camAngles.Length - 1)
                index = 0;

            transform.position = camAngles[index].position;
            transform.rotation = camAngles[index].rotation;
        }

        public void LeftCamViewButton()
        {
            index--;

            if (index < 0)
                index = camAngles.Length - 1;

            transform.position = camAngles[index].position;
            transform.rotation = camAngles[index].rotation;
        }
    }
}


//GameManager gameManager;
//public LayerMask ground;
//public LayerMask wall;

//public float speed = 5;
//public float wallDistance = 10;

//Vector3 moveDir;
//Vector3 curVel;
//float lastFrameTime;

//if (gameManager.onTwoDMode) return;

//float myDeltaTime = Time.realtimeSinceStartup - lastFrameTime;
//lastFrameTime = Time.realtimeSinceStartup;

//float x = Input.GetAxisRaw(StringData.horizontal);
//float z = Input.GetAxisRaw(StringData.vertical);

//Vector3 origin = transform.position;
//RaycastHit hit;

//// camera movement direction
//moveDir = new Vector3(x, 0, z);
//moveDir = moveDir.normalized;

//if (Physics.Raycast(origin, moveDir * .1f, out hit, wall))
//{
//    if (hit.collider.tag.Equals(StringData.wallTag) && hit.distance <= wallDistance)
//        return;
//}

//// allow camera movement
//transform.Translate(moveDir * speed * myDeltaTime);