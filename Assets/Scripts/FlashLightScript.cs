using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightScript : MonoBehaviour {
    public float t;
    public float increment = 0.001f;
    bool rebindMethod = false;
    bool LightOn = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-25,25,t));
        if (t < 0 || t>1)
            increment *=-1;
        t += increment;

        if (rebindMethod)
        {
            if (LightOn)  // Включение фонаря
            {
                if (GetComponent<Light2D.LightSprite>().Color.a < 0.25f)
                    GetComponent<Light2D.LightSprite>().Color.a += 0.02f;
                else
                    rebindMethod = false;
            }
            else     // Выключение
            {
                if (GetComponent<Light2D.LightSprite>().Color.a > 0)
                    GetComponent<Light2D.LightSprite>().Color.a -= 0.02f;
                else
                    rebindMethod = false;
            }
        }

	}

    public void Rebind(bool LocalLightOn)
    {
        LightOn = LocalLightOn;
        rebindMethod = true;
    }
}
