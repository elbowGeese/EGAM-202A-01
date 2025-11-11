using UnityEngine;

public class Trashcan : MonoBehaviour, Interactable
{
    public NameDisplay nameDisplay;
    public AudioSource trashAudio;

    void Start()
    {
        nameDisplay.SetText("Trashcan");
        HideNameDisplay();
    }

    public void Interact(Transform hold)
    {
        if (hold.childCount > 0) 
        { 
            Discard(hold.GetChild(0).gameObject);
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

    private void Discard(GameObject trash)
    {
        Destroy(trash);
        trashAudio.Play();
        // play trash particles
    }
}
