using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsButton : InterfaceFatherScript
{

    bool clickBool = true;

    RectTransform RectTransformComponent;

    GameObject Manager;
    public GameObject SettingsGO;
    public GameObject DarkBackground;
    public AudioMixer Mixer;



    void Start()
    {

        Manager = GameObject.Find("Manager");


    }
    

    public void Click()
    {
        if (clickBool)
        {
            DarkBackground.SetActive(true);
            //DarkBackground.GetComponent<RawImage>().raycastTarget = true;
            StopAllCoroutines();
            SettingsGO.SetActive(true);
            clickBool = false;
            Manager.GetComponent<MenuScript>().SettingsLayer = true;
            StartCoroutine(ShowUpLayer(true));
        }
        else
        {
            if (PlayerPrefs.GetInt("SFX") == 0)
                Mixer.SetFloat("SFXVolumeExpose", -80);
            else
                Mixer.SetFloat("SFXVolumeExpose", 0);

            

            if (PlayerPrefs.GetInt("Music") == 0)
                Mixer.SetFloat("MusicVolumeExpose", -80);
            else
                Mixer.SetFloat("MusicVolumeExpose", 0);

            

            DarkBackground.SetActive(false);   
            StopAllCoroutines();
            clickBool = true;
            Manager.GetComponent<MenuScript>().SettingsLayer = false;
            StartCoroutine(ShowUpLayer(false));
        }


    }

    IEnumerator ShowUpLayer(bool show = true)
    {
        float EnumStartX = SettingsGO.GetComponent<RectTransform>().localScale.x;
        float EnumTargetX;
        float EnumTime = 0;
        float EnumLerpResult;
        if (show)
            EnumTargetX = 1;
        else
            EnumTargetX = 0;

        while (Mathf.Abs(EnumTargetX - EnumStartX )>= 0.1f)
        {
            EnumLerpResult = Mathf.Lerp(EnumStartX, EnumTargetX, EnumTime);
            SettingsGO.GetComponent<RectTransform>().localScale = new Vector3(EnumLerpResult, EnumLerpResult);
            DarkBackground.GetComponent<RawImage>().color = new Color(0, 0, 0, (EnumLerpResult * 0.25f) );
            EnumTime += 10 * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (!show)
        {
            SettingsGO.SetActive(false);
            DarkBackground.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        }
    }


}