using TMPro;
using UnityEngine;

public class ComboPopup : MonoBehaviour
{
    private Animator anim;
    public TMP_Text comboText;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowCombo(string message, Color comboColor)
    {
        comboText.text = message;
        comboText.color = comboColor;

        anim.SetTrigger("show");
    }
}
