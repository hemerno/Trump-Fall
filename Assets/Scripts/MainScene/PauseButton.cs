using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseButton : InterfaceFatherScript {
    public AudioMixer Mixer;
    bool timePause = true;
    bool CanPause = true;
    TapScript TapScr;
    float lastTimeScale;
    // bool GentelmenWaiting = false; // 3х секундное ожидание до снятие с пазуы
    bool commandProcessing = false; // Обработка команды в данный момент
    bool canUnpause = false;    // Можно ли снять с паузы - корутин дает добро после ожидания

    Text UnpausingText;

    float LastBackButtonTime;  // Для детекта даблтапа
    float TimeInPause = 0;
    Coroutine mover;
    Coroutine SettingsStop;
    public bool settingsLayer = false;

    public GameObject SettingsButton;



    void Start()
    {
        TapScr = GameObject.Find("Manager").GetComponent<TapScript>();
        UnpausingText = GameObject.Find("UnpauseText").GetComponent<Text>();
        mover = StartCoroutine(MoveInterface(true, gameObject, GameObject.Find("Text (3)")));
    }


    public void Pause(bool AddEvent = false)
    {

        if (CanPause)
        {
            if (timePause)
            {
                lastTimeScale = Time.timeScale;
                Time.timeScale = 0;
                Mixer.SetFloat("MusicPitchExpose", 0);
                timePause = !timePause;
                TapScr.IsPaused = true;
                if (!AddEvent)
                    UnpausingText.text = "GAME PAUSED";
                else
                    UnpausingText.text = "";
                StartCoroutine(Time_PauseTime(0.1f));
                SettingsStop = StartCoroutine(SettingsMoveMent(true)); // Выдвижение кнопки настройки
            }
            else
            {
                if (canUnpause)
                {
                    Mixer.SetFloat("MusicPitchExpose", 1);
                    Time.timeScale = lastTimeScale;
                    timePause = !timePause;
                    TapScr.IsPaused = false;
                    canUnpause = false;
                }
                else
                    if (!commandProcessing)
                {
                    StopCoroutine(SettingsStop);
                    StartCoroutine(SettingsMoveMent(false));
                    StartCoroutine(AwakeAfterPause(0.2f));
                }
            }

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))                                   // Действия при клавише назад
        {
            if (!settingsLayer)
            {
                if (TimeInPause - LastBackButtonTime <= 0.5f & Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    Mixer.SetFloat("MusicPitchExpose", 1);
                    SceneManager.LoadScene("MenuScene");
                }
                else
                {
                    LastBackButtonTime = TimeInPause;
                    GetComponent<PauseButton>().Pause();
                }
            }
            else
                SettingsButton.GetComponent<SettingsButtonMainScene>().Click();
        }
    
    }

    IEnumerator Time_PauseTime(float delay)
    {
        while (TapScr.IsPaused)
        {
            TimeInPause += delay;
            yield return new WaitForSecondsRealtime(delay);
        }
        TimeInPause = 0;
    }

    IEnumerator SettingsMoveMent(bool showUp = true)
    {
        Vector3 localVEctor3;
        float timer = 0;

        while (timer < 1)
        {
            if (showUp)
                localVEctor3 = Vector3.Lerp(SettingsButton.GetComponent<RectTransform>().anchoredPosition, SettingsButton.GetComponent<InterfaceFatherScript>().ShowPosition, timer);
            else
                localVEctor3 = Vector3.Lerp(SettingsButton.GetComponent<RectTransform>().anchoredPosition, SettingsButton.GetComponent<InterfaceFatherScript>().HidePosition, timer);
            SettingsButton.GetComponent<RectTransform>().anchoredPosition = localVEctor3;

            timer += Time.fixedDeltaTime * 0.7f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
            
        
    }

    IEnumerator AwakeAfterPause(float delay)
    {
        
        commandProcessing = true;
        float s = 3;
        
        UnpausingText.text = ((int)s).ToString();
        while (s>0.2f)
        {
            s -= delay;
            UnpausingText.text =   (Mathf.FloorToInt(s+1)).ToString();
            
            yield return new WaitForSecondsRealtime(delay);
        }
        UnpausingText.text = "";
        canUnpause = true;
        commandProcessing = false;
        Pause();
        
    }

    public void BreakAndHide()
    {
        CanPause = false;
        if (mover != null)
        StopCoroutine(mover);
       //    StopAllCoroutines();
        Time.timeScale = 1;
        StartCoroutine(MoveInterface(false, gameObject, GameObject.Find("Text (3)")));
        gameObject.GetComponent<Button>().interactable = false;
    }

}
