using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

public class ButtonSoundScript : MonoBehaviour {

	public void Click()
    {
        GameObject.Find("Day progress and weather").GetComponents<AudioSource>()[1].Play();
    }
}
