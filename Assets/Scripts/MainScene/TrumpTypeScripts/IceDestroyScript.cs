using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDestroyScript : MonoBehaviour
{

    TrumpOPool Pool;
    public GameObject Target;
    Rigidbody2D Rb2d;
    Animator IceAnim;
    public Sprite Type1, Type2;
    int MycurrentType;

    bool ReverciveError = false;

    void Start()
    {
        Pool = GameObject.Find("Manager").GetComponent<TrumpOPool>();
        Rb2d = GetComponent<Rigidbody2D>();
        IceAnim = GetComponent<Animator>();
        Initialization();
    }

    void MainACtivity()
    {
        //Rb2d.velocity = Vector2.zero;
        Pool.IceGoToThePool(gameObject);
    }

    public void Initialization()
    {
        if (Random.Range(0, 2) == 1)
        {
            GetComponent<SpriteRenderer>().sprite = Type1;
            MycurrentType = 1;
            IceAnim.Play("IceIdle");
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Type2;
            MycurrentType = 2;
            IceAnim.Play("IceIdle2");
        }
        if (ReverciveError)
        {
            ReverciveError = false;
            ReverciveMethod();
        }
    }


    public void ReverciveMethod()
    {
        if (IceAnim == null)                // Если лед новый и вызван синим, то этот метод запустится быстрее Start и выдаст ошибку
        {
            ReverciveError = true;
        }
        else
        {
            if (MycurrentType == 1)
                IceAnim.Play("IceSplitReverse");
            else
                IceAnim.Play("IceSplitReverse2");
        }
    }

    public void CrashTheIce()
    {
        if (MycurrentType == 1)
            IceAnim.Play("IceSplit");
        else
            IceAnim.Play("IceSplit2");
    }

    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, new Vector3( Target.transform.position.x + 0.07f, Target.transform.position.y + 0.02f, -1f), 10); //Vector2.MoveTowards(transform.position, Target.transform.position, 20); +new Vector3(0.07f, 0.02f, -1f)
        if (Target != null)
        {
            transform.position = Target.transform.position + Vector3.back;
            transform.rotation = Target.transform.rotation;
        }
    }
}
