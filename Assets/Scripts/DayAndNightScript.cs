using System.Collections;
using UnityEngine;
using Light2D;

public class DayAndNightScript : MonoBehaviour
{

    public Gradient test;
    public static bool unique = true;


    public GameObject CurrentCam;
    public GameObject SceneLight;
    public GameObject FlashLight1, FlashLight2;

    public float DayLenght;
    float CurrentTime;
    float DayProgress;

    bool incrRain = false;
    int incRainValue;
    float Timer = 1;
    bool RainingNow = false;

    ParticleSystem PartSys;
    bool NeedToChangeWheather;

    bool LastChekDayProgress;



    void Awake()
    {
        if (unique)
        {
            unique = false;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);

        DayLenght *= 60;
    }


    void Start()
    {
        PartSys = GetComponent<ParticleSystem>();
        CurrentTime = Random.Range(0, DayLenght + 1);


        if (((CurrentTime + Time.fixedDeltaTime) % DayLenght) / DayLenght >= 0.15f & ((CurrentTime + Time.fixedDeltaTime) % DayLenght) / DayLenght <= 0.7f)
        {
            LastChekDayProgress = true;
        }

        RainDropper();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Time.timeSinceLevelLoad <= 0.2f)
        {
            CurrentCam = GameObject.Find("Main Camera");
            SceneLight = GameObject.Find("GlobalLight");
        }

        CurrentTime += Time.fixedDeltaTime;
        CurrentTime %= DayLenght;       // Получение текущей фазы дня
        DayProgress = CurrentTime / DayLenght;      // Коэффицент 

        CurrentCam.GetComponent<Camera>().backgroundColor = test.Evaluate(DayProgress);
        if (DayProgress <= 0.5f)
        {

            //CurrentCam.GetComponent<Camera>().backgroundColor = Color.Lerp(dayColor, nightColor, DayProgress * 2);            // 20 global 120 flashlight
            //  SceneLight.GetComponent<Light>().intensity = Mathf.Lerp(0, 1.35f, (DayProgress) * 2);
            SceneLight.GetComponent<LightSprite>().Color.a = Mathf.Lerp(0.35f, 0.5f, (DayProgress) * 2);
        }
        else
        {
            //CurrentCam.GetComponent<Camera>().backgroundColor = Color.Lerp(nightColor, dayColor, (DayProgress-0.5f) * 2 );
            // SceneLight.GetComponent<Light>().intensity = Mathf.Lerp(1.35f, 0, (DayProgress - 0.5f) * 2);
            SceneLight.GetComponent<LightSprite>().Color.a = Mathf.Lerp(0.5f, 0.35f, (DayProgress - 0.5f) * 2);
        }


        if (DayProgress >= 0.15f & DayProgress <= 0.7f)                  // Stars creating
        {
            if (LastChekDayProgress)        // Для проверки ОДИН РАЗ 
            {
                FlashLight1.GetComponent<FlashLightScript>().Rebind(false);
                FlashLight2.GetComponent<FlashLightScript>().Rebind(false);
                var emisson = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
                emisson.rateOverTime = 0;
                LastChekDayProgress = false;
            }
        }
        else
        {
            if (!LastChekDayProgress)
            {
                FlashLight1.GetComponent<FlashLightScript>().Rebind(true);
                FlashLight2.GetComponent<FlashLightScript>().Rebind(true);
                var emisson = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
                emisson.rateOverTime = 5;
                LastChekDayProgress = true;
            }
        }


        Timer -= Time.fixedDeltaTime;
        if (Timer <= 0)
            if (incrRain) 
            {
                if (incRainValue < 200)
                {
                    incRainValue += 5;
                    SetEmissionRate(incRainValue);
                }
                else
                    incrRain = false;          
                Timer = 1;
            }
            else
            {
                if (incRainValue > 0)
                {
                    incRainValue -= 11;
                    SetEmissionRate(incRainValue);
                }
                else
                    PartSys.Stop();
                Timer = 1;
            }


    }

    void SetEmissionRate(float emissionRate)             // Текущая версия юнити (5.5.0f3) не дает напрямую обращаться к emission rate в 
    {                                                                                   // связи с тем, что код партиклов написан на плюсах и обертнут в местный интерфейс -___-

        if (Time.time > 5)
        {
            var emission = PartSys.emission;

            emission.rateOverTime = emissionRate;


        }

    }

    void RainDropper()
    {

        int rainLenght = 1;
        if (RainingNow)
        {
            RainingNow = false;
            incrRain = false;
            SetEmissionRate(0);
            //transform.GetChild(1).gameObject.SetActive(false);
            
            Invoke("RainDropper", 60);

        }
        else
            if (Random.Range(1, 101) <= 10)     // Rain starting
        {
            rainLenght = Random.Range(30, 121);

            PartSys.Play();
            RainingNow = true;
            incrRain = true;
            Invoke("RainDropper", rainLenght);

        }
        else
            Invoke("RainDropper", 60);
    }
}
