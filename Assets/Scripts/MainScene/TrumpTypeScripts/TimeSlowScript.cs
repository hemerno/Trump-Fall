using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class TimeSlowScript : MonoBehaviour {

    public float TimeScale;
    float SlowTimer;
    bool slowBool = false;
    float currentTimer;
    public AudioMixer Mixer;
    

	// Use this for initialization
	void Start () {
        
        SlowTimer = 3.2f * TimeScale;  //3 секунды реального времени
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        currentTimer -= Time.fixedDeltaTime;
        if (currentTimer <= 0)
        {
            Time.timeScale = 1F;
            Mixer.SetFloat("ExposePitch", 1);
            slowBool = false;
        }
	}


    public void Slowing()
    {
        if (!slowBool)
        {
            slowBool = true;
            currentTimer = SlowTimer;
            Time.timeScale = TimeScale;
            Mixer.SetFloat("ExposePitch", TimeScale);
        }
        else
            currentTimer = SlowTimer;
    }
}
