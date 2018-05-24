using UnityEngine;
using System.Collections;

public class FlyingDollarsScript : MonoBehaviour {

    float Timer = 1;
    float alpha = 1;
    SpriteRenderer SpRend;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        SpRend = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 5);
        Invoke("StopGettingUp", 1);
	
	}
	

    void FixedUpdate()
    {
        Timer -= Time.fixedDeltaTime;
        if (Timer<=0)
        {
            alpha -= 0.01f;
            SpRend.color = new Color(255, 255, 255, alpha);
        }
    }

    void StopGettingUp()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
