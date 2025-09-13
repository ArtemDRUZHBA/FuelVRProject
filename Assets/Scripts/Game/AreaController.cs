using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class AreaController : MonoBehaviour
{
    [SerializeField] private TeleportationArea mainArea, pumpArea;

    public void MainToPumpArea()
    {
        mainArea.enabled = true;
        pumpArea.enabled = false;
    }
    public void PumpToMainArea()
    {
        pumpArea.enabled = true;
        mainArea.enabled = false;
    }
}
