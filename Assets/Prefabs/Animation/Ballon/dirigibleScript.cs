using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirigibleScript : MonoBehaviour {


	public void ChangeDirection () {
        GetComponent<SpriteRenderer>().flipX = true;
        transform.GetChild(0).transform.localPosition = new Vector3(-0.5f, 0.2666666f, -0.1f);	
	}
	
	
}
