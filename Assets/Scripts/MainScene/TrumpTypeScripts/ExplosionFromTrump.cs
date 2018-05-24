using UnityEngine;
using System.Collections;

public class ExplosionFromTrump : MonoBehaviour {

    bool scaleLocal = true;
    float t = 0;
    float buffer;
    Animator anim;
    int LoopTimes;
    public int LoopCondition;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        //anim.Play("StartMove");
    }

    // Update is called once per frame
   /* void FixedUpdate()
    {

        if (scaleLocal)
        {
            buffer =  Mathf.Lerp(0.1f, 0.5f, t);
            transform.localScale = new Vector3(buffer, buffer, buffer);
            t += 0.1F;
            if (t > 1f)
            {
                scaleLocal = false;
                anim.Play("ExploseAnimation");
                Destroy(this.gameObject, 0.5f);
            }
        }

    }*/


   void RedEvent()
    {
        anim.Play("ExploseAnimation");
        GetComponent<CircleCollider2D>().radius = 2;
    }

    void AnimEvent()
    {
        /*   LoopTimes += 1;
           if (LoopCondition <=LoopTimes)
           {
               anim.Play("ExpoEnd");
           }*/
        anim.Play("ExpoEnd");
    }

    void Destr()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trump")
        {
            other.transform.GetComponent<CandidateScript>().PrepareToDie();
        }
    }
}
