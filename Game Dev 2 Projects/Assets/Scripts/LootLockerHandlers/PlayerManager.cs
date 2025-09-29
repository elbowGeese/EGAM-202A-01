using UnityEngine;
using LootLocker.Requests;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public Leaderboard leaderboard;

    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in.");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());

                if (leaderboard.leaderboardDisplay)
                {
                    leaderboard.leaderboardDisplay.DisplayStatus("");
                }

                done = true;
            }
            else
            {
                Debug.Log("Could not start session.");

                if (leaderboard.leaderboardDisplay)
                {
                    leaderboard.leaderboardDisplay.DisplayStatus("Connection failed\nCurrently offline");
                }

                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
