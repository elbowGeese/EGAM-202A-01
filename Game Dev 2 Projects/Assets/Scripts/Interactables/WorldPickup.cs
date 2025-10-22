using UnityEngine;

public class WorldPickup : MonoBehaviour, Interactable
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}
