using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlayScript : MonoBehaviour
{
    public string[] PhrasesArray;
    RectTransform RctTrsf;
    Text Txt;
    float z;
    float t = 0;
    bool increase = true;
    bool alphaIncrease = true;
    float alpha = 0;
    float r, g, b;
    // Use this for initialization
    void Start()
    {     // 3

        RctTrsf = GetComponent<RectTransform>();
        Txt = GetComponent<Text>();
        r = Txt.color.r;
        g = Txt.color.g;
        b = Txt.color.b;
        Txt.color = new Color(r, g, b, alpha);
        Txt.text = PhrasesArray[Random.Range(0, PhrasesArray.Length)];


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        z = Mathf.Lerp(-5, 5, t);
        RctTrsf.rotation = Quaternion.Euler(0, 0, z);               // Вращение на 5 градусов

        if (increase)
        {
            t += Time.fixedDeltaTime * 0.4f;
            if (t >= 1)
                increase = false;
        }
        else
        {
            t -= Time.fixedDeltaTime * 0.4f;
            if (t <= 0)
            {
                increase = true;
            }


        }


        if (alphaIncrease)
        {
            alpha += Time.fixedDeltaTime * 1.4f;
            if (alpha >= 1)
            {
                alphaIncrease = false;
            }
        }
        else
        {
            alpha -= Time.fixedDeltaTime*1.4f;
            if (alpha <= 0)
                alphaIncrease = true;
        }

        Txt.color = new Color(r, g, b, alpha);
    }


}
