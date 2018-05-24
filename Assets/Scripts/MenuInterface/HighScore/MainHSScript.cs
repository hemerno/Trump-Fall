using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHSScript : MonoBehaviour {

    public GameObject UpLines;
    public GameObject DownLines;
    public GameObject DotasGroup;
    public GameObject TextGroup;
    bool Intro = false;          // Используем интро для воспроизведения всей анимации в обратном порядке


    void Start()
    {
        TextGroup.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("HighScore").ToString();
    }

	void MyStart () {

        StartCoroutine( InterfaceFatherScript.MoveInterface(Intro, UpLines.transform.GetChild(0).gameObject, UpLines.transform.GetChild(1).gameObject));
        if (Intro)
        Invoke("DownLinesInv", 0.6f);
	}
	
	// Update is called once per frame
	void DownLinesInv () {
        StartCoroutine(InterfaceFatherScript.MoveInterface(Intro, DownLines.transform.GetChild(0).gameObject, DownLines.transform.GetChild(1).gameObject));
        if (Intro)
            Invoke("DotsStage1", 0.4f);
        else
            MyStart();
    }

    void DotsStage1()
    {
        StartCoroutine(InterfaceFatherScript.MoveInterface(Intro, DotasGroup.transform.GetChild(2).gameObject));
        if (Intro)
        Invoke("DotsStage2", 0.2f);
        else
            Invoke("DownLinesInv", 0.1f);
    }

    void DotsStage2()
    {
        StartCoroutine(InterfaceFatherScript.MoveInterface(Intro, DotasGroup.transform.GetChild(1).gameObject, DotasGroup.transform.GetChild(3).gameObject));
        if (Intro)
            Invoke("DotsStage3", 0.2f);
        else
            Invoke("DotsStage1", 0.1f);
    }

    void DotsStage3()
    {
        StartCoroutine(InterfaceFatherScript.MoveInterface(Intro, DotasGroup.transform.GetChild(0).gameObject, DotasGroup.transform.GetChild(4).gameObject));
        if (Intro)
            Invoke("TextFadeStage", 0.6f);
        else
            Invoke("DotsStage2", 0.1f);

    }

    void TextFadeStage()
    {
        StartCoroutine(FadeIn(Intro, TextGroup.transform.GetChild(0).gameObject, TextGroup.transform.GetChild(1).gameObject));
        if (!Intro)
        {
            Invoke("DotsStage3", 0.1f);
        }
    }

    public void GoBack()
    {
        StopAllCoroutines();
        Intro = !Intro;
        if (Intro)
            MyStart();
        else
            TextFadeStage();
        
    }

    IEnumerator FadeIn(bool Cond, params GameObject[] Obj)
    {
        float t;
        if (Cond)
            t = 0;
        else
            t = 1;
        Color buffcolor;
        if (Cond)
            while(t <= 1)
            {
                foreach (GameObject element in Obj)
                {
                    buffcolor = element.GetComponent<Text>().color;
                    element.GetComponent<Text>().color = new Color(buffcolor.r, buffcolor.g, buffcolor.b, t);
                }
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        else
            while (t >= -0.2)
            {
                foreach (GameObject element in Obj)
                {
                    buffcolor = element.GetComponent<Text>().color;
                    element.GetComponent<Text>().color = new Color(buffcolor.r, buffcolor.g, buffcolor.b, t);
                }
                t -=2* Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }


    }
}
