using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameInterfaceScript : InterfaceFatherScript {

    public GameObject finalScoreText, newHSText;
    public GameObject finalScore;
    public GameObject tapScore;
    public GameObject topResult;
    GameObject Manager;
    int TapCount;
    string TopTxt;
    void Start() {
        Manager = GameObject.Find("Manager");
        TapCount = Manager.GetComponent<TapScript>().TapCount;
        
        if (TapCount > PlayerPrefs.GetInt("HighScore"))
        {
            newHSText.SetActive(true);
            PlayerPrefs.SetInt("HighScore",TapCount);
            PlayerPrefs.Save();
            AndroidLeaderBoard.PushScoreToTheBoard(TapCount);
        }
        else
            finalScoreText.SetActive(true);




        GameObject.Find("PauseButton").GetComponent<PauseButton>().BreakAndHide();
        StartCoroutine(MoveInterface(true, gameObject));
        finalScore.GetComponent<Text>().text = TapCount.ToString();
        tapScore.GetComponent<Text>().text = Manager.GetComponent<TapScript>().TapsTotal.ToString() + "\nTAPS";
        Invoke("ButtonsLerpTrigger", 1f);
        
            topResult.GetComponent<Text>().text = Manager.GetComponent<TapScript>().TrumpsKilled.ToString() + " TRUMPS\n";
        
        switch( Random.Range(1, 6))
        {
            case 1:
                {
                    topResult.GetComponent<Text>().text += "DESTROYED";     
                    break;
                }
            case 2:
                {
                    topResult.GetComponent<Text>().text += "RUINED";      
                    break;
                }
            case 3:
                {
                    topResult.GetComponent<Text>().text += "ANNIHILATED";     
                    break;
                }
            case 4:
                {
                    topResult.GetComponent<Text>().text += "OWNED";      
                    break;
                }
            case 5:
                {
                    topResult.GetComponent<Text>().text += "REKTED";
                    break;
                }
        }
        

        
    }


    void ButtonsLerpTrigger() {

        StartCoroutine(MoveInterface(true, transform.GetChild(0).GetChild(0).gameObject, transform.GetChild(0).GetChild(1).gameObject, transform.GetChild(0).GetChild(2).gameObject));
    }

    public void Restart()
    {

        GameObject.Find("Day progress and weather").GetComponent<GameAddScript>().CheckForAdd(SceneManager.GetActiveScene().name);
        GameObject.Find("Day progress and weather").GetComponent<MainMusicScript>().SecondChance();

    }
    public void SecondStepRestart()
    {
        StopAllCoroutines();
        StartCoroutine(MoveInterface(false, gameObject));
        Invoke("OneMoreTry", 1f);
    }

    public void GoToMenu()
    {
        StopAllCoroutines();
        StartCoroutine(MoveInterface(false, gameObject));
        GameObject.Find("Day progress and weather").GetComponent<MainMusicScript>().SecondChance();
        Invoke("ToTheMenuPls", 1f);
    }

    public void ShowHSBoard()
    {
        GameObject.Find("LeaderBoardSetup").GetComponent<AndroidLeaderBoard>().ShowLeaderBoard();
    }

    void OneMoreTry()
    {
        SceneManager.LoadScene(2);
    }

    void ToTheMenuPls()
    {
        SceneManager.LoadScene(1);
    }
}
