using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HouseScript : MonoBehaviour
{
    public bool CanLoose;
    bool firstLoose = true;
    public GameObject GoOnButton;
    GameObject Manager;
    public GameObject EndInterface;
    void Start()
    {
        Manager = GameObject.Find("Manager");
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (CanLoose)
        {
            if (other.tag == "Trump")
                if (firstLoose & GameObject.Find("Day progress and weather").GetComponent<GameAddScript>().VideoIsLoaded())
                {
                    firstLoose = false;
                    // Первый вызов
                    GameObject.Find("Day progress and weather").GetComponent<MainMusicScript>().GameOver();
                    TrumpStop(other.gameObject);
                    GoOnButton.SetActive(true);

                }
                else
                {
                    GameObject.Find("Day progress and weather").GetComponent<MainMusicScript>().GameOver();
                    TrumpStop(other.gameObject);
                    EndInterface.SetActive(true);
                }
        }
    }

    public void EvadedAdd()
    {
        EndInterface.SetActive(true);
    }
    void TrumpStop(GameObject trmp)
    {
        trmp.GetComponent<BoxCollider2D>().enabled = false;
        trmp.transform.SetParent(null);
        Time.timeScale = 1;
        Manager.GetComponent<GlobalTrumpVelocityZeroScript>().StopHammerTime(GameEnding: true, TrumpLooser: trmp);
    }

    public void TrumpGoOn()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Find("Day progress and weather").GetComponent<MainMusicScript>().SecondChance();
        StartCoroutine(Disappeare());
        Manager.GetComponent<TapScript>().ImmortalMode = false;
        Manager.GetComponent<TapScript>().DeathMode = false;

    }

    public IEnumerator Disappeare(bool ShowTime = false)            // For thx 4 ur sup button. Это некрасиво и вообще ужасно, в след раз будет адекватный класс для интерфейса =(
    {
        GameObject button = GameObject.Find("ThxForAddGroup");
        
        if (!ShowTime)
            button.transform.GetChild(1).GetComponent<Button>().interactable = false;
        else
            button.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        float startSize = button.transform.localScale.x;
        float curSize = startSize;
        float step = 6 / (0.2f * 50);
        if (!ShowTime)
        {
            while (curSize > 0.1f)
            {
                curSize -= step;
                button.transform.localScale = new Vector3(curSize, curSize, curSize);
                yield return new WaitForFixedUpdate();
            }
            Destroy(button);
        }
        else
            while (curSize < 6)
            {
                curSize += step;
                button.transform.localScale = new Vector3(curSize, curSize, curSize);
                yield return new WaitForFixedUpdate();
            }
    }
}
