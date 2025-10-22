using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [Header("VARIABLES")]
    public float rayDist = 10f;
    public float rayRadius = 3f;

    [Header("REFERENCES")]
    public Transform hold;

    private InputAction interactAction;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            if(hold.childCount <= 0) { TryPickUp(); }
            else { TryDrop(); }
        }
    }

    private void TryPickUp()
    {
        Ray ray = new Ray(transform.position, transform.forward); ;

        RaycastHit[] hits = Physics.SphereCastAll(ray, rayRadius, rayDist);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact(hold);
                break;
            }
        }
    }

    private void TryDrop()
    {
        Transform currentPickup = hold.GetChild(0);
        currentPickup.GetComponent<Rigidbody>().isKinematic = false;
        currentPickup.parent = null;
    }
}
