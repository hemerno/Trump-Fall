using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{
    public bool firstJump = true;
    public AudioMixer Mixer;
    bool oneWayJump = false;        // Для вызова метода один раз
    bool enableUIclick = true;

    public bool SettingsLayer = false;

    public GameObject SettingsObj, LeaderboardObj, GameNameObj;

    AsyncOperation ao;

    void Start()
    {
        if (PlayerPrefs.HasKey("SFX"))
            if (PlayerPrefs.GetInt("SFX") == 0)
                Mixer.SetFloat("SFXVolumeExpose", -80);
            else
                Mixer.SetFloat("SFXVolumeExpose", 0);
        else
            Mixer.SetFloat("SFXVolumeExpose", 0);


        if (PlayerPrefs.HasKey("SFX"))
            if (PlayerPrefs.GetInt("Music") == 0)
                Mixer.SetFloat("MusicVolumeExpose", -80);
            else
                Mixer.SetFloat("MusicVolumeExpose", 0);
        else
            Mixer.SetFloat("MusicVolumeExpose", 0);

        //ao = SceneManager.LoadSceneAsync("MainScene");
        //ao.allowSceneActivation = false;  
    }


    public void CheckBeforeNextScene()
    {
        //ao.allowSceneActivation = true;
        if (!oneWayJump & !SettingsLayer)
            if ((Time.timeSinceLevelLoad > 3.6f & firstJump) || Time.timeSinceLevelLoad > 2f & !firstJump)         // Время через которое все анимации пройдут и можно уйти в игру
            {
                GameObject.Find("Day progress and weather").GetComponent<GameAddScript>().CheckForAdd(SceneManager.GetActiveScene().name);
            }
    }

    public void JumpIntoGame()
    {

        oneWayJump = true;
        EnableUI();
        Invoke("LoadNextScene", 1.2f);
            
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void EnableUI()
    {
        StopAllCoroutines();
        GameObject.Find("HighscoreGroup").GetComponent<MainHSScript>().GoBack();
        StartCoroutine(MoveInterface(enableUIclick, SettingsObj, LeaderboardObj, GameNameObj));
        
        if (enableUIclick)
        Invoke( "MovementEnd", 1.5f);
        else
            Invoke("MovementEnd", 0.5f);
        enableUIclick = !enableUIclick;
    }


    void LoadNextScene()
    {
        SceneManager.LoadScene("MainScene");
    }


    void MovementEnd()
    {
        GameNameObj.transform.GetChild(0).gameObject.SetActive(!enableUIclick);           // Активация текста
    }


    public  IEnumerator MoveInterface(bool MovementType, params GameObject[] Buttons)
    {
        float testT = Time.time;
        Vector3 localVEctor3;
        float timer = 0;

        foreach (GameObject element in Buttons)
            if (element.GetComponent<Button>() != null)
                element.GetComponent<Button>().interactable = MovementType;     // Кнопка выключается при сворачивании интерфейса

        while (timer < 1)
        {
            foreach (GameObject element in Buttons)
            {
                if (MovementType)
                    localVEctor3 = Vector3.Lerp(element.GetComponent<RectTransform>().anchoredPosition, element.GetComponent<InterfaceFatherScript>().ShowPosition, timer);
                else
                    localVEctor3 = Vector3.Lerp(element.GetComponent<RectTransform>().anchoredPosition, element.GetComponent<InterfaceFatherScript>().HidePosition, timer);

                element.GetComponent<RectTransform>().anchoredPosition = localVEctor3;
            }
            timer += Time.fixedDeltaTime * 0.1f;
            yield return new WaitForFixedUpdate();
        }
    }
}