using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class DragonScene : MonoBehaviour
    {
        Animator anim;

        public GameObject flamethrower;
        float cooldown = 10f;

        private void Start()
        {
            anim = GetComponent<Animator>();
            flamethrower.SetActive(false);
        }

        public void Update()
        {

            if (cooldown <= 0)
            {
                anim.SetTrigger(StringData.attack);
                cooldown = 10f;
            }
            else { cooldown -= Time.deltaTime; }
        }

        public void FlamethrowerOn()
        {
            flamethrower.SetActive(true);
        }

        public void FlamethrowerOff()
        {
            flamethrower.SetActive(false);
        }
    }
}
