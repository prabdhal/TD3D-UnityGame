using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class StarPopAnim : MonoBehaviour
    {
        Animator anim;
        Image star;
        
        void Start()
        {
            if (anim == null)
                anim = GetComponent<Animator>();
            if (star == null)
                star = GetComponent<Image>();
        }
        
        void Update()
        {
            anim.SetFloat(StringData.fillAmount, star.fillAmount);
        }
    }
}
