using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TapScript : MonoBehaviour
{
    TrumpOPool TrumpsPool;
    public bool ImmortalMode = false;
    public bool DeathMode = false;
    public bool AddRunning = false;
    public bool AddEvaded = false;
    public int TrumpsKilled;
    public int TapCount 
        {
            set
        {
            switch (value- _TapCount)
            {
                case 1: _TapCount += 1;
                    break;

                case 5: _TapCount += 5;
                    break;
            }
            
                

        }
            get { return _TapCount; }
        }
    private int _TapCount;
    private int StringTapCount;
    float TimeForString = 0.05f;
    float currentStringTimer;


    RaycastHit2D[] hits;
    public bool IsPaused = false;
    Text TextComp, txt1, txt2,txt3;

    int localTouchBlock;


    public int TapsTotal;
    


    void Start()
    {
       // TextComp = GameObject.Find("Text").GetComponent<Text>();
        txt1 = GameObject.Find("Text (1)").GetComponent<Text>();
        txt2 = GameObject.Find("Text (2)").GetComponent<Text>();
        txt3 = GameObject.Find("Text (3)").GetComponent<Text>();
        TrumpsPool = GetComponent<TrumpOPool>();
    }

    void Update()
    {
        if(true)        // !ImmortalMode
        { 
        if (Application.platform == RuntimePlatform.WindowsEditor & !IsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.30F, Vector2.zero);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.tag == "Trump")
                    {
                        hit.transform.GetComponent<CandidateScript>().PrepareToDie();
                       
                    }
                }
            }
        }
            if (Input.touchCount > 0 & !IsPaused)
            {

                if (Input.touchCount >= 2)
                    localTouchBlock = 2;
                else
                    localTouchBlock = 1;

                for (int i = 0; i < localTouchBlock; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        TapsTotal += 1;
                        hits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), 0.35f, Vector2.zero);         // было ровно 0.3f
                        foreach (RaycastHit2D hit in hits)
                        {

                            if (hit.transform.tag == "Trump")
                            {
                                hit.transform.GetComponent<CandidateScript>().PrepareToDie();
                            }
                        }
                    }
                }
            }
        }


       

    }

    void FixedUpdate()
    {
        currentStringTimer -= Time.fixedDeltaTime;
        if (StringTapCount < _TapCount & currentStringTimer <= 0)
        {
            StringTapCount += 1;
            currentStringTimer = TimeForString;
        }
        //TextComp.text = TapCount.ToString();// + " trumpets";
        txt1.text = StringTapCount.ToString();
        txt2.text = StringTapCount.ToString();
        txt3.text = StringTapCount.ToString();

        /*if ( IsPaused & Input.GetKeyDown(KeyCode.Escape))
        {
            _TapCount *= 10;
            SceneManager.LoadScene("MenuScene");
        }*/
        

        
    }

    void OnApplicationPause()
    {
        if (Time.timeScale > 0)
            if (DeathMode)
            {
                //GameObject.Find("PauseButton").GetComponent<PauseButton>().Pause(true);
            }
            else
                 //if (!DeathMode)
                GameObject.Find("PauseButton").GetComponent<PauseButton>().Pause();

    }

  /*  void OnApplicationFocus()
    {
        if (AddRunning)
        {
            if (AddEvaded)
            {
                Time.timeScale = 1;
                AddRunning = false;
            }
            else
            {
                AddRunning = false;
                GameObject.Find("PauseButton").GetComponent<PauseButton>().Pause();
            }
        }
    }*/


}
