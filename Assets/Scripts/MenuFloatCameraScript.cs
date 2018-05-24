using UnityEngine;
using System.Collections;

public class MenuFloatCameraScript : MonoBehaviour {


    float t,y, startYPos = 0;
    bool OneCorotine = true;
    public float TargetFlowTime;
    bool delayBool;
    public float delayTime;
	// Use this for initialization
	void Start () {
        if (Time.time > 15)          // Если это переход из главной сцены - камера не спускается 
        {
            transform.position = new Vector3(0, 0, -10);
            GameObject.Find("Manager").GetComponent<MenuScript>().EnableUI();
            GameObject.Find("Manager").GetComponent<MenuScript>().firstJump = false;
            GetComponent<MenuFloatCameraScript>().enabled = false;
        }
        startYPos = transform.position.y;
        TargetFlowTime = 1 / (50 * TargetFlowTime);
        Invoke("DelayMethod", delayTime);
	
	}


    void DelayMethod() { delayBool = true; }

	void FixedUpdate () {
        if (delayBool)
        {
            if (t < 1)
            {
                y = Mathf.Lerp(transform.position.y, 0, t);

                transform.position = new Vector3(0, y, -10);
                t += TargetFlowTime;




                if (t >= 0.11 & OneCorotine)
                {
                    OneCorotine = false;
                    GameObject.Find("Manager").GetComponent<MenuScript>().EnableUI();
                }



            }
            else
            {
                GetComponent<MenuFloatCameraScript>().enabled = false;
            }
        }
    }
}
