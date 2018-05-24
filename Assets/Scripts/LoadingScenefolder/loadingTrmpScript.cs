using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingTrmpScript : MonoBehaviour {

    public Sprite[] TrmpSprites;
    public bool MovementType;
    Vector2 startPosition;
    // Use this for initialization
    void Start() {
        if (MovementType)
            GetComponent<Rigidbody2D>().velocity = new Vector2(1,1)*3;
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1)*3;

        ReSprite();
         startPosition = transform.position;
        

        }
	

    void FixedUpdate()
    {
        if (MovementType)
        {


            if (transform.position.x > 8)
            {
                transform.position = new Vector2(transform.position.x - 20, transform.position.y - 20);
                ReSprite();
            }
        }
        else
        {
            if (transform.position.x < -8)
            {
                transform.position = new Vector2(transform.position.x + 20, transform.position.y - 20);
                ReSprite();
            }
        }
    }
    
    void ReSprite()
    {
        foreach (Transform Child in transform)
        {
            Child.GetComponent<SpriteRenderer>().sprite = TrmpSprites[Random.Range(0, TrmpSprites.Length - 1)];
        }
    }
}
