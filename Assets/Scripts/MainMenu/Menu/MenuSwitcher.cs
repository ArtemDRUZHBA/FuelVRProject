using UnityEngine;

public class MenuSwitcher : MonoBehaviour, IMenuSwitcher
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject singlePlayerMenu;

    public void SwitchMenu(bool showSinglePlayer)
    {
        mainMenu.SetActive(!showSinglePlayer);
        singlePlayerMenu.SetActive(showSinglePlayer);
    }
}
