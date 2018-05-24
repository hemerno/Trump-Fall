using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

    TrumpOPool TrumpsPool;

    void Start()
    {
        TrumpsPool = GameObject.Find("Manager").GetComponent<TrumpOPool>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trump")
            other.gameObject.GetComponent<CandidateScript>().GoToThePoolAnimEvent();
    }
}
