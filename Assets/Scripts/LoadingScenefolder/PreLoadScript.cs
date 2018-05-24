using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadScript : MonoBehaviour {
    bool loadCompleteBool = false;
    bool nextSceneBool = false;
    float t = 0;
    Vector3[] startVectors = new Vector3[3];
    Vector3[] endVectors = new Vector3[3];
    int i = 0;
    bool oneLoadBool;
	// Use this for initialization
	void Start () {
        foreach(Transform child in transform)
        {
            startVectors[i] = child.position;
            i += 1;
        }
        i = 0;
        endVectors[0] = new Vector3(0f, 11.5f, -9);
        endVectors[1] = new Vector3(-0.45f, 7.5f, -9);
        endVectors[2] = new Vector3(0f, 13.5f, -9);
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	public void LoadComplete() {
        loadCompleteBool = true;
	}

    void FixedUpdate()
    {
        if (loadCompleteBool)
            if (Time.time > 3)
            {
                foreach (Transform child in transform)
                {
                    child.transform.position = Vector3.Lerp(startVectors[i], endVectors[i], t);
                    i += 1;
                }
                i = 0;
                t += Time.fixedDeltaTime*0.5f ;
                if (t > 1f)
                    if (!oneLoadBool)
                    {
                        oneLoadBool = true;
                        t = 0;
                        nextSceneBool = true;
                        loadCompleteBool = false;
                        
                        SceneManager.LoadScene("MenuScene");
                        GameObject.Find("Day progress and weather").GetComponent<MainMusicScript>().LetsRock();
                    }
                    

            }

        if (nextSceneBool)
        {
            foreach (Transform child in transform)
            {
                child.transform.position = Vector3.Lerp( endVectors[i], startVectors[i], t);
                i += 1;
            }
            i = 0;
            t += Time.fixedDeltaTime * 0.5f;
            if (t > 1)
                Destroy(gameObject);
        }
    }
}
