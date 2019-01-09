using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public IEnumerator FadeInOut(float t1, float t2, GameObject i, float delay)
    {
        StartCoroutine(FadeToFullAlpha(t1, i));
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeToZeroAlpha(t2, i));
    }

    public IEnumerator FadeToFullAlpha(float t, GameObject i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeToZeroAlpha(float t, GameObject i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
