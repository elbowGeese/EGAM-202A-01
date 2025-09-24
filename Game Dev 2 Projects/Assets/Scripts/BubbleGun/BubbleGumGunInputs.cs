using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleGumGunInputs : MonoBehaviour
{
    private InputAction fireAction;

    private bool firing;
    public bool Firing {  get { return firing; } }

    private bool fired = false;
    public bool Fired { get { return fired; } }

    void Start()
    {
        fireAction = InputSystem.actions.FindAction("Fire");
    }

    void Update()
    {
        firing = fireAction.IsInProgress();
        fired = fireAction.WasReleasedThisFrame();
    }
}
