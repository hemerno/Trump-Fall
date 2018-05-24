using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrumpOPool : MonoBehaviour {
    ArrayList FreeTrumpArray = new ArrayList();
    ArrayList EngagedTrumpArray = new ArrayList();

    ArrayList FreeIceArray = new ArrayList();
    ArrayList EngagedIceArray = new ArrayList();

    int PresidentId = 0;
    int IceId = 0;

    GameObject TrumpStorage;
    GameObject IceStorage;

    public GameObject PrefTrump;
    GameObject localTrump;

    public GameObject PrefIce;
    GameObject localIce;

    TapScript TpScrpt;


    void Start()
    {
        TrumpStorage = GameObject.Find("Trumps");
        IceStorage = GameObject.Find("Ices");
        TpScrpt = GameObject.Find("Manager").GetComponent<TapScript>();
    }

   

    public void TrumpPlease(Vector2 TrumpPosition)
    {
        if (!TpScrpt.ImmortalMode)
        {

            if (FreeTrumpArray.Count == 0)
            {
                CreateNewTrump(TrumpPosition);
                return;
            }
            else
            {
                localTrump = (GameObject)FreeTrumpArray[0];
                FreeTrumpArray.RemoveAt(0);
                EngagedTrumpArray.Add(localTrump);
                localTrump.transform.position = TrumpPosition;
                localTrump.SetActive(true);
                localTrump.GetComponent<CandidateScript>().Initialization();
            }
        }
    }

    void CreateNewTrump(Vector2 TrumpPosition)
    {
        localTrump = Instantiate(PrefTrump, TrumpPosition, Quaternion.identity);
        localTrump.name = "Trump" + (PresidentId+1).ToString();
        localTrump.transform.SetParent(TrumpStorage.transform);
        PresidentId += 1;
        EngagedTrumpArray.Add(localTrump);
    }

    public void TrumpGoToThePool(GameObject TiredTrump)
    {
        TiredTrump.SetActive(false);
        if (EngagedTrumpArray.Count == 0)
        {
            Destroy(TiredTrump);
            print("MYSTERY ERROR!!!!!!");
        }
        else
        {
            EngagedTrumpArray.RemoveAt(0);
            FreeTrumpArray.Add(TiredTrump);
        }
    }

    public GameObject IcePlease(GameObject IceTarget)
    {
        if (FreeIceArray.Count == 0)
        {
            return CreateNewIce(IceTarget);
        }
        else
        {
            localIce = (GameObject)FreeIceArray[0];
            FreeIceArray.RemoveAt(0);
            EngagedIceArray.Add(localIce);
            //localIce.transform.position = IceTarget.transform.position + Vector3.back;
            localIce.transform.position = IceTarget.transform.position;
            localIce.SetActive(true);
            localIce.GetComponent<IceDestroyScript>().Target = IceTarget;
            localIce.GetComponent<IceDestroyScript>().Initialization();
            
            return localIce;

        }
    }

    GameObject CreateNewIce (GameObject IceTarget)
    {
        localIce = Instantiate(PrefIce, IceTarget.transform.position, IceTarget.transform.rotation);     // Смещения из за кривого льда -__-
        localIce.transform.SetParent(IceStorage.transform);
        localIce.GetComponent<IceDestroyScript>().Target = IceTarget;
        EngagedIceArray.Add(localIce);
        localIce.name = "Ice" + (IceId + 1).ToString();
        IceId += 1;
        return localIce;
    }

    public void IceGoToThePool(GameObject BrokenIce)
    {
        BrokenIce.SetActive(false);
        
        EngagedIceArray.RemoveAt(0);
        FreeIceArray.Add(BrokenIce);
    }

   
}
