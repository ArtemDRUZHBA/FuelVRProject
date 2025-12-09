using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingButtonManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private SettingButtonsEffect buttonEffect;
    private int activePanelIndex = 0;

    void Start()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => SwitchPanels(index));
        }

        SwitchPanels(0);
    }


    public void SwitchPanels(int index)
    {
        activePanelIndex = index;

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[index].SetActive(true);

        foreach (Button btn in buttons)
        {
            buttonEffect.ApplyEffect(btn, false);
        }
        buttonEffect.ApplyEffect(buttons[index], true);
    }
}
