using UnityEngine;

public class RecipeUnlockManager : MonoBehaviour
{
    public enum RecipePart
    {
        BASIL,
        CHAMOMILE,
        DANDELION,
        MINT,
        MYSTERY,
        BURN,
        COUGH,
        ITCH,
        HANGOVER,
        HEADACHE,
        SCRAPE,
        ACNE,
        POISON,
        CRAMP
    }
    private bool[] activeRecipeParts = new bool[14];

    public GameObject[] 
        basils, 
        chamomiles, 
        dandelions,
        mints,
        burn,
        cough,
        itch,
        hangover,
        headache,
        scrape,
        acne,
        poison,
        cramp;

    void Start()
    {
        HideAll();
    }

    void Update()
    {
        ObjectType[] allObjects = FindObjectsByType<ObjectType>(FindObjectsSortMode.None);
        foreach (ObjectType obj in allObjects)
        {
            if(activeRecipeParts[(int)obj.objName] == false)
            {
                // set active
                ShowObjInRecipeBook(obj.objName.ToString());
            }
        }
    }

    private void HideAll()
    {
        ShowArray(basils, false);
        ShowArray(chamomiles, false);
        ShowArray(dandelions, false);
        ShowArray(mints, false);

        ShowArray(burn, false);
        ShowArray(cough, false);
        ShowArray(itch, false);
        ShowArray(hangover, false);
        ShowArray(headache, false);
        ShowArray(scrape, false);
        ShowArray(acne, false);
        ShowArray(poison, false);
        ShowArray(cramp, false);
    }

    private void ShowArray(GameObject[] array, bool active)
    {
        foreach (GameObject herb in array)
        {
            herb.SetActive(active);
        }
    }

    private void ShowObjInRecipeBook(string objName)
    {
        int place = -1;
        GameObject[] array = new GameObject[0];

        switch (objName)
        {
            case "BASIL":
                array = basils;
                place = (int)RecipePart.BASIL;
                break;
            case "CHAMOMILE":
                array = chamomiles;
                place = (int)RecipePart.CHAMOMILE;
                break;
            case "DANDELION":
                array = dandelions;
                place = (int)RecipePart.DANDELION;
                break;
            case "MINT":
                array = mints;
                place = (int)RecipePart.MINT;
                break;
            case "BURN":
                array = burn;
                place = (int)RecipePart.BURN;
                break;
            case "COUGH":
                array = cough;
                place = (int)RecipePart.COUGH;
                break;
            case "ITCH":
                array = itch;
                place = (int)RecipePart.ITCH;
                break;
            case "HANGOVER":
                array = hangover;
                place = (int)RecipePart.HANGOVER;
                break;
            case "HEADACHE":
                array = headache;
                place = (int)RecipePart.HEADACHE;
                break;
            case "SCRAPE":
                array = scrape;
                place = (int)RecipePart.SCRAPE;
                break;
            case "ACNE":
                array = acne;
                place = (int)RecipePart.ACNE;
                break;
            case "POISON":
                array = poison;
                place = (int)RecipePart.POISON;
                break;
            case "CRAMP":
                array = cramp;
                place = (int)RecipePart.CRAMP;
                break;
        }

        if(place != -1)
        {
            activeRecipeParts[place] = true;
            ShowArray(array, true);
        }
    }
}
