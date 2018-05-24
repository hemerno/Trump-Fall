using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverProgressScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	 public void SaveSoundSettings (string soundType, int state) {

        PlayerPrefs.SetInt(soundType, state);
		
	}
}
