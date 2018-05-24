using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScript : InterfaceFatherScript {


    public GameObject endGroup;
    bool clicked = false;
    public float ShowingTime;
    public GameObject RedTimerGO;

	// Use this for initialization
	void Start () {
        ShowingTime *= 50;
        StartCoroutine(RedTimer());
        StartCoroutine(InterfaceFatherScript.MoveInterface(true, gameObject));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!clicked)
        {
            ShowingTime -= Time.fixedDeltaTime;
            if (ShowingTime <= 0)
            {
                GameOverTime();
            }
        }
		
	}

    public void GameOverTime()
    {
        StopAllCoroutines();
        StartCoroutine(InterfaceFatherScript.MoveInterface(false, gameObject));
        endGroup.SetActive(true);
        Destroy(gameObject, 2);
    }

    public void Click()
    {
        StopAllCoroutines();
        GameObject.Find("Manager").GetComponent<TapScript>().AddRunning = true;
        clicked = true;
        //Show add video
        GameObject.Find("Day progress and weather").GetComponent<GameAddScript>().ShowRewVideo();
        //Destroy(gameObject);
        StartCoroutine(Disappeare());
    }

    IEnumerator RedTimer()
    {
        RectTransform RctTrasform = RedTimerGO.GetComponent<RectTransform>();
        float startWidth = RctTrasform.rect.width;
        float height = RctTrasform.rect.height;
        float step = 320 / ShowingTime;
        while (startWidth > 0)
        {
            startWidth -= step;
            if (startWidth < 0)
                startWidth = 0;
            RctTrasform.sizeDelta = new Vector2(startWidth, height);
            yield return new WaitForFixedUpdate();
        }
        GameOverTime();
    }

    IEnumerator Disappeare()
    {
        float startSize = gameObject.transform.localScale.x;
        float curSize = startSize;
        float step = curSize / (0.2f * 50);
        while (curSize > 0.1f)
        {
            curSize -= step;
            gameObject.transform.localScale = new Vector3(curSize, curSize, curSize);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
