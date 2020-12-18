using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarProgress : MonoBehaviour
{
    public Image star01;
    public Image star02;
    public Image star03;

    public void Init(int index)
    {
        if (index == 0)
        {
            star01.fillAmount = 0;
            star02.fillAmount = 0;
            star03.fillAmount = 0;
            return;
        }

        star01.fillAmount = 1;
        if (index <= 1)
            return;

        star02.fillAmount = 1;
        if (index <= 2)
            return;

        star03.fillAmount = 1;
    }
}
