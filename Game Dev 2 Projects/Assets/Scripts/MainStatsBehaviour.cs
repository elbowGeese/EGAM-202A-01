using UnityEngine;
using UnityEngine.UI;

public class MainStatsBehaviour : MonoBehaviour
{
    public Image[] stars;
    public Color emptyColor;
    public EndScreenBehaviour endScreenBehaviour;

    void Update()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        int numStarsEarned = endScreenBehaviour.GetNumOfStarsEarned();
        for (int i = 0; i < stars.Length; i++)
        {
            if(i < numStarsEarned)
            {
                stars[i].color = Color.white;
            }
            else
            {
                stars[i].color = emptyColor;
            }
        }
    }
}
