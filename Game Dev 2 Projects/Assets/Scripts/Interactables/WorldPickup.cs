using UnityEngine;

public class WorldPickup : MonoBehaviour, Interactable
{
    private Rigidbody rb;
    public NameDisplay nameDisplay;
    private ObjectType objectType;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectType = GetComponent<ObjectType>();

        string displayText = objectType.objName.ToString();
        if(objectType.objType == ObjectType.Type.SALVE) { displayText += " " + objectType.objType.ToString(); }
        nameDisplay.SetText(displayText);
        nameDisplay.HideDisplay();
    }

    public void Interact(Transform hold)
    {
        Debug.Log("Hit!");
        // check if hold is empty
        if(hold.childCount >= 1) { return; }
        // if it is not, then do nothing
        // if it is, move this item to the hold
        rb.isKinematic = true;
        transform.parent = hold;
        transform.localPosition = Vector3.zero;
    }

    public void ShowNameDisplay()
    {
        nameDisplay.ShowDisplay();
    }

    public void HideNameDisplay()
    {
        nameDisplay.HideDisplay();
    }
}
