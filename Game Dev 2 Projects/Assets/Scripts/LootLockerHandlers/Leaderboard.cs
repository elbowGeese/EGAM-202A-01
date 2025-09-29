using UnityEngine;
using LootLocker.Requests;
using System.Collections;

public class Leaderboard : MonoBehaviour
{
    private string leaderboardKey = "globalhighscore";

    public LeaderboardDisplay leaderboardDisplay;

    public IEnumerator SubmitScoreRoutine(string playerName, int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerName, scoreToUpload, leaderboardKey, playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score.");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;

        if (leaderboardDisplay) { leaderboardDisplay.ClearDisplay(); }

        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, 0, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    // get item
                    string playerName = "";
                    string playerScore = "";

                    playerName += members[i].rank + ". ";
                    if (members[i].metadata != "")
                    {
                        playerName += members[i].metadata;
                    }
                    else
                    {
                        playerName += "anon";
                    }

                    playerScore += members[i].score;

                    // set display item
                    if (leaderboardDisplay)
                    {
                        leaderboardDisplay.AddDisplayItem(playerName, playerScore);
                    }
                }

                if (leaderboardDisplay)
                {
                    leaderboardDisplay.PopulateDisplay();
                }

                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
