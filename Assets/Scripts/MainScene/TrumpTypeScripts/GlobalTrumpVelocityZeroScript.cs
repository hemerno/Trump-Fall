using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTrumpVelocityZeroScript : MonoBehaviour
{

    public GameObject TrumpStorage;
    TapScript TapManager;
    public GameObject PepeCircle;

    bool CoroutineComplete = false;
    int requiredTrumpCount;


    void Start()
    {
        TapManager = GetComponent<TapScript>();
    }

    /*  void Update()
      {
          if (Input.GetKeyDown(KeyCode.Space))
          {
              StopHammerTime();
          }
      }*/


    public void StopHammerTime(float HammerY = 0, bool HolyType = false, GameObject HolyTrigger = null, bool IceKingType = false, bool GameEnding = false, GameObject TrumpLooser = null)            // Остановка скорости для всех
    {
        CancelInvoke();     // На всякий случай для избежаия багов
        TapManager.ImmortalMode = true;     // Для отсановки всех новых трампов
        if (GameEnding)
            TapManager.DeathMode = true;





        foreach (Transform child in TrumpStorage.transform) // Для остановки уже существующих
        {
            child.gameObject.GetComponent<CandidateScript>().MyGlobalStopReact(HammerY, HolyType, IceKingType);
        }
        if (!TapManager.DeathMode)
            Invoke("LetsaGo", 0.8f);            // какая то ошибка

        if (HolyType)
        {
            StartCoroutine(PepeStyle(HolyTrigger));
        }


    }

    void LetsaGo()                      // Возобновление
    {
        if (!TapManager.DeathMode)
            TapManager.ImmortalMode = false;

        foreach (Transform child in TrumpStorage.transform)
        {
            child.GetComponent<CandidateScript>().MyGlobalStopReact();
        }

    }

    public IEnumerator PepeStyle(GameObject FatherGO)
    {
        GameObject localPepeCircle;
        GameObject[] Result = GameObject.FindGameObjectsWithTag("Trump");  //Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(5.5f, 9f), 0);
        foreach (GameObject col in Result)
        {
            if (col.tag == "Trump" & FatherGO.transform.position != col.transform.position & !col.GetComponent<CandidateScript>().alreadyDead)
            {
                localPepeCircle = Instantiate(PepeCircle, new Vector3(FatherGO.transform.position.x, FatherGO.transform.position.y, -0.5f), Quaternion.identity);
                localPepeCircle.GetComponent<PepeCircleScript>().Target = new Vector3(col.transform.position.x, col.transform.position.y, -0.5f);
                yield return new WaitForFixedUpdate();
            }
        }
    }
    /*  IEnumerator DieOrder(float delay, GameObject looser)
      {
          requiredTrumpCount = TrumpStorage.transform.childCount;
          while (requiredTrumpCount > 1) // Для остановки уже существующих
          {
              requiredTrumpCount -= 1;
              TrumpStorage.transform.GetChild(0).gameObject.GetComponent<CandidateScript>().MyGlobalStopReact(deathStage: true);
              TrumpStorage.transform.GetChild(0).transform.SetParent(null);

              yield return new WaitForSeconds(delay);
          }

          looser.GetComponent<CandidateScript>().MyGlobalStopReact(deathStage: true);
          CoroutineComplete = true;
      }*/
}
