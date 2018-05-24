using UnityEngine;
using System.Collections;

public class HighscoreMenuScript : InterfaceFatherScript
{
    GameObject AndrewScr;
    void Start()
    {
        AndrewScr = GameObject.Find("LeaderBoardSetup");
    }

    public void Click()
    {
        AndrewScr.GetComponent<AndroidLeaderBoard>().ShowLeaderBoard();
    }

}