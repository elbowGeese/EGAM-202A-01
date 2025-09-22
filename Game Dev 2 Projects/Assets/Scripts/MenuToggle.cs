using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public GameObject[] menus;
    
    void Start()
    {
        CloseAllMenus();
    }

    void CloseAllMenus()
    {
        foreach (GameObject menu in menus)
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
        }
    }

    public void ToggleMenu(int menuIndex)
    {
        bool opening = !menus[menuIndex].activeSelf;

        CloseAllMenus();

        if (opening)
        {
            menus[menuIndex].SetActive(true);
        }
    }
}
