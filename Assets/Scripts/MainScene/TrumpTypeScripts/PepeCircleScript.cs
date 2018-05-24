using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepeCircleScript : MonoBehaviour {
    public Vector3 Target;
    bool OneWayBool = false;
    float t=0.02f / 0.3f , s = 0;
    // Use this for initialization

    // Update is called once per frame
    void FixedUpdate() {

        transform.position = Vector3.Lerp(transform.position, Target, s); 
        s += t;
        if (s >= 1 & !OneWayBool)
        {
            OneWayBool = !OneWayBool;
            GetComponent<Animator>().SetBool("CircleBool", true);
        }
	}


    void AnimEnd()
    {
        Destroy(gameObject);
    }
}
