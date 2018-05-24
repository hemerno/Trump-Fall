using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSliderScript : MonoBehaviour
{

                        // -27 start
    public GameObject polzunok;
    public string MyPrefKey;
    int localKeySavedState;

    // Use this for initialization              
    void Start()
    {
        if (PlayerPrefs.HasKey(MyPrefKey))
        {
            localKeySavedState = PlayerPrefs.GetInt(MyPrefKey);
            if (localKeySavedState == 1)
                polzunok.GetComponent<RectTransform>().localPosition = new Vector3(-27, 0, 0);
            else
                polzunok.GetComponent<RectTransform>().localPosition = new Vector3(27, 0, 0);
        }
        else
        {
            localKeySavedState = 1;
            PlayerPrefs.SetInt(MyPrefKey, localKeySavedState);
            polzunok.GetComponent<RectTransform>().localPosition = new Vector3(-27, 0, 0);
        }

        // if (check sound state)
        //    polzunok rabotai suka  

    }



    public void Click()
    {
        if (localKeySavedState == 1)
            localKeySavedState = 0;
        else
            localKeySavedState = 1;
        PlayerPrefs.SetInt(MyPrefKey, localKeySavedState);

            StopAllCoroutines();
            StartCoroutine(Mover(localKeySavedState));

        

    }


    IEnumerator Mover(int KeyState)
    {
        float EnumStartX = GetComponent<RectTransform>().localScale.x;
        float EnumTargetX;
        float EnumTime = 0;
        float EnumLerpResult;
        if (KeyState == 0)
            EnumTargetX = 27;
        else
            EnumTargetX = -27;

        while (Mathf.Abs(EnumTargetX - EnumStartX) >= 0.1f)
        {
            EnumLerpResult = Mathf.Lerp(EnumStartX, EnumTargetX, EnumTime);
            polzunok.GetComponent<RectTransform>().localPosition = new Vector3(EnumLerpResult, 0);
            EnumTime += 5 * Time.fixedDeltaTime;
            yield return new WaitForSecondsRealtime(0.02f);
        }


        print("Ienumerator Finished");
    }
}
