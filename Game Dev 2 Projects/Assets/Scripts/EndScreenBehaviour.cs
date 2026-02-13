using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenBehaviour : MonoBehaviour
{
    public GameObject screen;
    public TMP_Text title, finalTime, levelText;
    public Button nextLevelButton;

    public Image[] stars;
    public float threeStarTime, twoStarTime, oneStarTime;
    public Color earnedStar;
    private MainTimer timer;

    public float showDelay = 0.5f;

    public AudioSource openSFX, countSFX, winSFX, loseSFX;

    void Start()
    {
        timer = FindAnyObjectByType<MainTimer>();

        nextLevelButton.transform.parent.gameObject.SetActive(false);
        levelText.text = "Level " + GetComponent<SceneChanges>().GetCurrentSceneIndex();
        //levelText.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        finalTime.gameObject.SetActive(false);
        screen.SetActive(false);
    }

    public int GetNumOfStarsEarned()
    {
        float timePassed = timer.timePassed;

        int starsEarned = 0;
        if (timePassed <= oneStarTime)
        {
            starsEarned++;
            if(timePassed <= twoStarTime)
            {
                starsEarned++;
                if(timePassed <= threeStarTime)
                {
                    starsEarned++;
                }
            }
        }

        return starsEarned;
    }

    public IEnumerator ShowEndScreen(bool won)
    {
        openSFX.Play();
        int thisLevelIndex = GetComponent<SceneChanges>().GetCurrentSceneIndex() - 1;

        screen.SetActive(true);

        yield return new WaitForSeconds(showDelay);

        // show + count stars
        int numCoinFlips = FindAnyObjectByType<CoinBehaviour>().numFlips;
        if (won)
        {
            int starsEarned = GetNumOfStarsEarned();
            if (starsEarned >= 1)
            {
                countSFX.Play();
                stars[0].color = earnedStar;
                yield return new WaitForSeconds(showDelay);
                if (starsEarned >= 2)
                {
                    countSFX.pitch += 0.1f;
                    countSFX.Play();
                    stars[1].color = earnedStar;
                    yield return new WaitForSeconds(showDelay);
                    if (starsEarned >= 3)
                    {
                        countSFX.pitch += 0.1f;
                        countSFX.Play();
                        stars[2].color = earnedStar;
                    }
                }
            }
            // update this level data
            if (starsEarned > LevelData.levels[thisLevelIndex].stars)
            {
                LevelData.levels[thisLevelIndex].stars = starsEarned;
            }
        }

        yield return new WaitForSeconds(showDelay);

        // show text
        title.gameObject.SetActive(true);
        finalTime.gameObject.SetActive(true);

        if (won) 
        { 
            winSFX.Play();
            title.text = "You won!"; 
            // unlock next level
            if(thisLevelIndex + 1 < LevelData.levels.Length)
            {
                LevelData.levels[thisLevelIndex + 1].unlocked = true;
            }
        }
        else 
        { 
            loseSFX.Play();
            title.text = "You lost..."; 
        }

        // update timer text
        float timePassed = timer.timePassed;
        int minutes = (int)timePassed / 60;
        int seconds = (int)timePassed % 60;
        finalTime.text = "Final Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        nextLevelButton.transform.parent.gameObject.SetActive(true);
        // if won, unlock next level
        // if next level not unlocked, lock next level button
        if(thisLevelIndex + 1 < LevelData.levels.Length)
        {
            if (!LevelData.levels[thisLevelIndex + 1].unlocked)
            {
                nextLevelButton.interactable = false;
            }
        }
        else
        {
            if (!won)
            {
                nextLevelButton.interactable = false;
            }
        }
        
        LevelData.SaveLevelData();
    }
}
