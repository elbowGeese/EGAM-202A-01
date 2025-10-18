using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    public Image[] lifeImage = new Image[3];

    public void SetDisplay(int numLives)
    {
        // turn all off
        foreach (Image image in lifeImage)
        {
            if (image.enabled) { image.enabled = false; }
        }

        // turn some on
        for(int i = 0; i < numLives; i++)
        {
            if(i >= lifeImage.Length) { break; }

            lifeImage[i].enabled = true;
        }
    }
}
