using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [Header("VARIABLES")]
    public float rayDist = 10f;
    public float rayRadius = 3f;

    private bool grindTried = false;

    [Header("REFERENCES")]
    public Transform hold;
    private Animator anim;
    public TMP_Text eDisplay;

    private InputAction interactAction;

    private Interactable currentSelection;
    private MortarPestle currentMP;

    public AudioSource pickupAudio;
    public float upPitch, downPitch;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        interactAction.performed += TryGrind;
        interactAction.canceled += TryPickUp;
    }

    void OnDisable()
    {
        interactAction.performed -= TryGrind;
        interactAction.canceled -= TryPickUp;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); ;

        RaycastHit[] hits = Physics.SphereCastAll(ray, rayRadius, rayDist);
        bool interactableInHits = false;
        foreach (RaycastHit hit in hits)
        {
            Interactable hitInteractable = hit.collider.gameObject.GetComponent<Interactable>();
            if (hitInteractable != null)
            {
                if(currentSelection != null)
                {
                    if(currentSelection != hitInteractable)
                    {
                        currentSelection.HideNameDisplay();
                    }
                }
                currentSelection = hitInteractable;
                hitInteractable.ShowNameDisplay();
                interactableInHits = true;
                break;
            }
        }

        if (!interactableInHits && currentSelection != null) 
        { 
            currentSelection.HideNameDisplay();
            currentSelection = null;
        }

        anim.SetBool("holdingItem", hold.childCount > 0);

        UpdateEDisplay();
    }

    private void UpdateEDisplay()
    {
        string textDisplay = "";

        // if player holding something
        if (hold.childCount > 0)
        {
            textDisplay = "<color=blue>Press E</color> to drop";

            // if player close to a drop point
            Interactable dropPoint = FindNearbyDropPoint();
            if (dropPoint != null)
            {
                if(dropPoint as MortarPestle)
                {
                    textDisplay += " in <color=blue>Mortar and Pestle</color>";
                }
                else if(dropPoint as Customer)
                {
                    textDisplay = "<color=blue>Press E</color> to give to <color=blue>Customer</color>";
                }
                else if(dropPoint as Trashcan)
                {
                    textDisplay += " in <color=blue>Trashcan</color>";
                }
                else
                {
                    Debug.Log("Unknown drop point");
                }
            }
        }
        else // if player not holding anything
        {
            if (currentSelection != null)
            {
                if (currentSelection as WorldPickup)
                {
                    textDisplay = "<color=blue>Press E</color> to pick up";
                }
                else if(currentSelection as MortarPestle)
                {
                    textDisplay = "<color=blue>Hold E</color> to mix";
                    MortarPestle mp = currentSelection as MortarPestle;
                    foreach (Transform objPos in mp.objectPlacement)
                    {
                        if (objPos.childCount > 0)
                        {
                            textDisplay += "\n<color=blue>Press E</color> to pick herb from <color=blue>Mortar and Pestle</color>";
                            break;
                        }
                    }

                    if(mp.salvePlacement.childCount > 0)
                    {
                        textDisplay += "\n<color=blue>Press E</color> to pick salve from <color=blue>Mortar and Pestle</color>";
                    }
                }
                else if(currentSelection is RecipeBook)
                {
                    textDisplay = "<color=blue>Press E</color> to read <color=blue>Recipe Book</color>";
                }
            }
        }

        eDisplay.text = textDisplay;
    }

    private void TryGrind(InputAction.CallbackContext context)
    {
        Interactable nearbyDrop = FindNearbyDropPoint();
        if (nearbyDrop == null) { return; }

        MortarPestle mp = nearbyDrop as MortarPestle;
        if (mp != null && currentMP == null)
        {
            currentMP = mp;
        }
        else if (mp == null && currentMP != null)
        {
            currentMP.StopGrind();
            currentMP = null;
        }

        if (!grindTried)
        {
            if (currentMP != null)
            {
                currentMP.StartGrind();
            }
        }

        grindTried = true;
    }

    private void TryPickUp(InputAction.CallbackContext context)
    {
        grindTried = false;
        // if grinding, stop grinding and end
        if(currentMP != null)
        {
            currentMP.StopGrind();
            currentMP = null;
            return;
        }

        // otherwise pick up or drop object
        if (hold.childCount <= 0) { PickUp(); }
        else { Drop(); }
    }

    private void PickUp()
    {
        if(currentSelection != null)
        {
            pickupAudio.pitch = upPitch;
            pickupAudio.Play();

            currentSelection.Interact(hold);
        }
    }

    private void Drop()
    {
        Transform currentPickup = hold.GetChild(0);

        Interactable dropPoint = FindNearbyDropPoint();
        if (dropPoint != null)
        {
            dropPoint.Interact(hold);
            return;
        }

        // otherwise, drop the object
        pickupAudio.pitch = downPitch;
        pickupAudio.Play();

        currentPickup.GetComponent<Rigidbody>().isKinematic = false;
        currentPickup.parent = null;
    }

    private Interactable FindNearbyDropPoint()
    {
        Ray ray = new Ray(transform.position, transform.forward); ;

        RaycastHit[] hits = Physics.SphereCastAll(ray, rayRadius, rayDist);
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.gameObject.GetComponent<MortarPestle>() != null)
            {
                return hit.collider.gameObject.GetComponent<MortarPestle>();
            }

            if (hit.collider.gameObject.GetComponent<Customer>() != null)
            {
                return hit.collider.gameObject.GetComponent<Customer>();
            }

            if (hit.collider.gameObject.GetComponent<Trashcan>() != null)
            {
                return hit.collider.gameObject.GetComponent<Trashcan>();
            }
        }

        return null;
    }
}
