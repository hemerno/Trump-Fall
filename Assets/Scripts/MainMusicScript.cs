using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicScript : MonoBehaviour {

    AudioSource MusicSlot;
    public AudioClip[] Loopys;
    public AudioClip LooseMusic;
    int loopysLength;
    bool clipSwitcherBool;      // First clip now question????

    // Use this for initialization
    void Start() {
        MusicSlot = GetComponent<AudioSource>();
        loopysLength = Loopys.Length;
        //MusicSlot.clip = Background_Clips[0];
        //print(Random.Range(0, Background_Clips.Length - 1));
    }

    public void LetsRock()
    {
        StartCoroutine(VolumeUp());
        StartCoroutine(RealTimeInvoke());
    }

   /* void ChangeLoop()
    {
        MusicSlot.clip = Loopys[Random.Range(0, loopysLength)];
        MusicSlot.Play();
        Invoke("ChangeLoop", 7.385f);
    }*/

    public void GameOver()
    {
        StopAllCoroutines();
        MusicSlot.Pause();
        MusicSlot.clip = LooseMusic;
        MusicSlot.Play();
    }

    public void SecondChance()
    {
        StartCoroutine(RealTimeInvoke(1.5f));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameOver();

        }
    }

    IEnumerator VolumeUp()
    {
        AudioSource ASIEnumComp = GetComponent<AudioSource>();
        while (ASIEnumComp.volume < 0.7f)
        {
            ASIEnumComp.volume += 0.01f;
            yield return new WaitForFixedUpdate();
        }

    }

    IEnumerator RealTimeInvoke(float delay = 0)
    {
        if (delay != 0)
            yield return new WaitForSecondsRealtime(delay);
        MusicSlot.clip = Loopys[Random.Range(0, loopysLength)];
        MusicSlot.Play();
        yield return new WaitForSecondsRealtime(7.385f);
        StartCoroutine(RealTimeInvoke());
    }

}
