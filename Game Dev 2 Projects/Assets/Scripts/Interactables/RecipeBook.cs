using UnityEngine;

public class RecipeBook : MonoBehaviour, Interactable
{
    private bool isOpen = false;

    public NameDisplay nameDisplay;
    public Animator uiAnim;

    void Start()
    {
        nameDisplay.SetText("Recipe Book");
        HideNameDisplay();
    }

    public void Interact(Transform hold)
    {
        isOpen = !isOpen;

        uiAnim.SetBool("isOpen", isOpen);
        hold.parent.GetComponent<PlayerMovement>().isPaused = isOpen;
        // play sound
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
