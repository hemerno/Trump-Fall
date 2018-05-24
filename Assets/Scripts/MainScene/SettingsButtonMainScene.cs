using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsButtonMainScene : InterfaceFatherScript {

	// Use this for initialization
	void Start () {
		
	}
    bool clickBool = true;
    public GameObject SettingsGO;
    public GameObject DarkBackground;
    public GameObject PauseGO;
    public AudioMixer Mixer;
    public void Click()
    {
        if (clickBool)
        {
            DarkBackground.SetActive(true);
            //DarkBackground.GetComponent<RawImage>().raycastTarget = true;
            StopAllCoroutines();
            SettingsGO.SetActive(true);
            clickBool = false;
            PauseGO.GetComponent<PauseButton>().settingsLayer = true;
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
            PauseGO.GetComponent<PauseButton>().settingsLayer = false;
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

        while (Mathf.Abs(EnumTargetX - EnumStartX) >= 0.1f)
        {
            EnumLerpResult = Mathf.Lerp(EnumStartX, EnumTargetX, EnumTime);
            SettingsGO.GetComponent<RectTransform>().localScale = new Vector3(EnumLerpResult, EnumLerpResult);
            DarkBackground.GetComponent<RawImage>().color = new Color(0, 0, 0, (EnumLerpResult * 0.25f));
            EnumTime += 10 * Time.fixedDeltaTime;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        if (!show)
        {
            SettingsGO.SetActive(false);
            DarkBackground.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        }
    }


}
