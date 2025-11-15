using UnityEngine;
using TMPro;

public class DMGPopup : MonoBehaviour
{
    public TMP_Text dmgText;

    public void SetDMGText(string dmg)
    {
        dmgText.text = dmg;
    }
}
