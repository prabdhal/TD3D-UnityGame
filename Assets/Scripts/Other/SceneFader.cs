using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image image;

    public AnimationCurve curve;

    [HideInInspector]
    public bool doneFading;

    private void Start()
    {
        if (image == null)
            image = GetComponentInChildren<Image>();
        
        Time.timeScale = 1;
        doneFading = false;
        StartCoroutine(FadeIn());

    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        doneFading = true;
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        //Done fading
        SceneManager.LoadScene(scene);
    }
}
