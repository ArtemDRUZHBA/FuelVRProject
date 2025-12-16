using UnityEngine;

using System.Collections.Generic;

public class VictorinaScriptController : MonoBehaviour
{
    public List<GameObject> Panel = new();
    

    public GameObject PanelWin;
    public GameObject PanelLose;
    public GameObject PanelHise;
    public GameObject PanelNormal;
    public int i = 0;

    private void Awake()
    {

        PanelHise.SetActive(true);
        Panel[i].SetActive(false);
        PanelWin.SetActive(false);
        PanelLose.SetActive(false);
    }

    public void StartVictorine()
    {
        PanelNormal.SetActive(false);
        PanelHise.SetActive(false);
        Panel[i].SetActive(true);
    }



    public void Lose()
    {
        Panel[i].SetActive(false);
        PanelLose.SetActive(true);
    }

    public void Win()
    {
        Panel[i].SetActive(false);
        PanelWin.SetActive(true);
    }

    public void WinPanelManager()
    {
        i++;
        PanelWin.SetActive(false);
        Panel[i].SetActive(true);
        if(i>=9)
        {
            PanelHise.SetActive(false);
            Panel[i].SetActive(false);
            PanelWin.SetActive(false);
            PanelLose.SetActive(false);
            PanelNormal.SetActive(true);
        }
    }

    public void LosePanelManager()
    {
       
        PanelLose.SetActive(false);
        Panel[i].SetActive(true);
    }

}
