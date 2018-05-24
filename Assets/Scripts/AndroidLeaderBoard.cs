using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AndroidLeaderBoard : MonoBehaviour
{



    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
    ILeaderboard lb, localLb;







    void Awake()            // 
    {
        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        LogIn();

    }



    public void ShowLeaderBoard()
    {
        if (!Social.localUser.authenticated)
            LogIn();
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_defenders_of_the_white_house);
    }

    public void LogIn()
    {
        print("Logging");
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                print("Login complete");
                CheckForHighscore();
            }
            else
            {
                print("Login failed");
                if (SceneManager.GetActiveScene().name == "LoadScene")
                {
                    GameObject.Find("Curtain").GetComponent<PreLoadScript>().LoadComplete();
                }
            }
        });
    }



    // Update is called once per frame
    public static void PushScoreToTheBoard(int newScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_defenders_of_the_white_house, (bool success) =>
        {
            // handle success or failure
        });
    }

    void CheckForHighscore()
    {
        lb = PlayGamesPlatform.Instance.CreateLeaderboard();
        lb.id = GPGSIds.leaderboard_defenders_of_the_white_house;
        lb.LoadScores(ok =>
        {
            if (ok)
            {
                Debug.Log(lb.localUserScore.formattedValue);
                if (PlayerPrefs.GetInt("HighScore") > int.Parse(lb.localUserScore.formattedValue))
                    PushScoreToTheBoard(PlayerPrefs.GetInt("HighScore"));
                else
                    PlayerPrefs.SetInt("HighScore", int.Parse(lb.localUserScore.formattedValue));
            }
            else
            {
                Debug.Log("Error retrieving leaderboardi");
            }
        });

        if (SceneManager.GetActiveScene().name == "LoadScene")
        {
            GameObject.Find("Curtain").GetComponent<PreLoadScript>().LoadComplete();
        }
    }


   
    /* internal void LoadUsersAndDisplay(ILeaderboard lb)
     {
         // get the user ids
         List<string> userIds = new List<string>();

         foreach (IScore score in lb.scores)
         {
             lb.userIds.Add(score.userID);
         }
         // load the profiles and display (or in this case, log)
         Social.LoadUsers(userIds.ToArray(), (users) =>
         {
             string status = "Leaderboard loading: " + lb.title + " count = " +
                 lb.scores.Length;
             foreach (IScore score in lb.scores)
             {
                 IUserProfile user = FindUser(users, score.userID);
                 status += "\n" + score.formattedValue + " by " +
                     (string)(
                         (user != null) ? user.userName : "**unk_" + score.userID + "**");
             }
             Debug.log(status);
         });
     }*/

}
