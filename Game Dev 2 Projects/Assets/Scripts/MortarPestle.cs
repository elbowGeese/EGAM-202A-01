using UnityEngine;
using UnityEngine.UI;

public class MortarPestle : MonoBehaviour, Interactable
{
    [Header("VARIABLES")]
    public GameObject[] salvePrefabs;

    public float mixingTime = 2f;
    private float mixingTimer;

    private bool mixing = false;

    [Header("REFERENCES")]
    public Transform[] objectPlacement = new Transform[3];
    private Animator anim;

    public Transform salvePlacement;

    public AudioSource grindingAudio, placeItemAudio, doneMixingAudio;
    public AudioClip placedClip, missClip;

    public NameDisplay nameDisplay;
    public Slider mixProgressSlider;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0f;

        nameDisplay.SetText("Mortar & Pestle");
        HideNameDisplay();

        grindingAudio.Play();
        grindingAudio.Pause();

        mixingTimer = 0f;
        mixProgressSlider.value = 0f;
    }

    void Update()
    {
        if (mixing)
        {
            mixingTimer += Time.deltaTime;
            mixProgressSlider.value = mixingTimer / mixingTime;

            if (mixingTimer >= mixingTime)
            {
                TryMakeSalve();

                mixingTimer = 0f;
                mixProgressSlider.value = mixingTimer / mixingTime;
                mixing = false;
            }
        }
    }

    public void Interact(Transform hold)
    {
        if(hold.childCount == 0)
        {
            // try picking
            Transform pickup = GetPickup();

            if (pickup != null)
            {
                pickup.parent = hold;
                pickup.localPosition = Vector3.zero;
                pickup.localScale = Vector3.one;
            }
        }
        else
        {
            // if there is a salve in the mortar, don't allow dropping
            if(salvePlacement.childCount != 0)
            {
                placeItemAudio.clip = missClip;
                placeItemAudio.Play();
                Debug.Log("Mortar has a mixed salve, take it out before putting more herbs in!");
                return;
            }

            // try dropping object in
            int openPlace = OpenPlacement();
            if (openPlace != -1)
            {
                Transform objToPlace = hold.GetChild(0);
                if (objToPlace.GetComponent<ObjectType>() != null)
                {
                    if(objToPlace.GetComponent<ObjectType>().objType == ObjectType.Type.HERB)
                    {
                        placeItemAudio.clip = placedClip;
                        placeItemAudio.Play();
                        objToPlace.parent = objectPlacement[openPlace];
                        objToPlace.localPosition = Vector3.zero;
                    }
                    else
                    {
                        placeItemAudio.clip = missClip;
                        placeItemAudio.Play();
                        Debug.Log("Cannot place this item in the mortar.");
                    }
                }
                else
                {
                    placeItemAudio.clip = missClip;
                    placeItemAudio.Play();
                    Debug.Log("Cannot place this item in the mortar.");
                }
            }
            else
            {
                placeItemAudio.clip = missClip;
                placeItemAudio.Play();
                Debug.Log("Mortar is full!");
            }
        }
    }

    public void ShowNameDisplay()
    {
        nameDisplay.ShowDisplay();
    }

    public void HideNameDisplay()
    {
        nameDisplay.HideDisplay();
    }

    private int OpenPlacement()
    {
        for(int i = 0; i < objectPlacement.Length; i++)
        {
            if (objectPlacement[i].childCount == 0)
            {
                return i;
            }
        }

        return -1;
    }

    public void StartGrind()
    {
        anim.speed = 1f;
        if(CheckForHerbs() > 0)
        {
            mixing = true;
        }

        grindingAudio.UnPause();
    }

    public void StopGrind()
    {
        anim.speed = 0f;
        mixing = false;

        grindingAudio.Pause();
    }

    private Transform GetPickup()
    {
        // check if there is an herb to pick up
        foreach (Transform op in objectPlacement)
        {
            if(op.childCount != 0)
            {
                if(CheckForHerbs() == 1)
                {
                    mixingTimer = 0f;
                    mixProgressSlider.value = mixingTimer / mixingTime;
                }

                return op.GetChild(0);
            }
        }

        // check if there is a salve to pick up
        if(salvePlacement.childCount != 0)
        {
            return salvePlacement.GetChild(0);
        }

        // mortar is empty
        return null;
    }

    private void TryMakeSalve()
    {
        // check if theres any herbs to mix
        int numHerbs = CheckForHerbs();

        if (numHerbs != 0)
        {
            // make salve based on herbs in mortar
            Transform salve = Instantiate(CalculateSalveMade(), salvePlacement).transform;
            salve.localPosition = Vector3.zero;

            doneMixingAudio.Play();

            // destroy each herb
            foreach (Transform op in objectPlacement)
            {
                if (op.childCount != 0)
                {
                    Destroy(op.GetChild(0).gameObject);
                }
            }
        }
    }

    private int CheckForHerbs()
    {
        int numHerbs = 0;

        foreach (Transform op in objectPlacement)
        {
            if (op.childCount != 0) { numHerbs++; }
        }

        return numHerbs;
    }

    private GameObject CalculateSalveMade()
    {
        int[] herbCount = CountHerbs();

        // if 2 or more chamomile, return cough salve
        if (herbCount[1] >= 2)
        {
            return salvePrefabs[2];
        }

        // if 1 basil and 2 dandelion, return burn salve
        if (herbCount[0] == 1 && herbCount[2] == 2)
        {
            return salvePrefabs[1];
        }

        // if 1 of basil, chamomile and dandelion, return itch salve
        if (herbCount[0] == 1 && herbCount[1] == 1 && herbCount[2] == 1)
        {
            return salvePrefabs[3];
        }

        // if 2 basil, 1 chamomile, return hangover salve
        if(herbCount[0] == 2 && herbCount[1] == 1)
        {
            return salvePrefabs[4];
        }

        // if 1 basil, 1 chamomile, return headache salve
        if (herbCount[0] == 1 && herbCount[1] == 1)
        {
            return salvePrefabs[5];
        }

        // if 1 basil, 1 dandelion, 1 mint, return scrape salve
        if (herbCount[0] == 1 && herbCount[2] == 1 && herbCount[3] == 1)
        {
            return salvePrefabs[6];
        }

        // if 1 mint, 1 dandelion, 1 chamomile, return poison salve
        if (herbCount[3] == 1 && herbCount[2] == 1 && herbCount[1] == 1)
        {
            return salvePrefabs[8];
        }

        // if =>1 mint, =>1 chamomile, return acne salve
        if(herbCount[3] >= 1 && herbCount[1] >= 1)
        {
            return salvePrefabs[7];
        }

        // if =>2 mint, return cramp salve
        if (herbCount[3] >= 2)
        {
            return salvePrefabs[9];
        }

        // default return mystery salve
        return salvePrefabs[0];
    }

    // BASIL = 0, CHAMOMILE = 1, DANDELION = 2, MINT = 3
    private int[] CountHerbs()
    {
        int[] herbCount = new int[4];

        foreach (Transform op in objectPlacement)
        {
            if (op.childCount > 0)
            {
                ObjectType.Name herb = op.GetChild(0).gameObject.GetComponent<ObjectType>().objName;

                switch (herb)
                {
                    case ObjectType.Name.BASIL:
                        herbCount[0]++;
                        break;
                    case ObjectType.Name.CHAMOMILE:
                        herbCount[1]++;
                        break;
                    case ObjectType.Name.DANDELION:
                        herbCount[2]++;
                        break;
                    case ObjectType.Name.MINT:
                        herbCount[3]++;
                        break;
                    default:
                        Debug.Log("Unknown herb in mix");
                        break;
                }
            }
        }

        return herbCount;
    }
}
