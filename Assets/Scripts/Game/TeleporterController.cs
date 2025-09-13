using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] private TeleportationArea teleportationArea;

    private void Start()
    {
        teleportationArea = GetComponent<TeleportationArea>();
    }

    public void TeleportOff()
    {
        teleportationArea.enabled = false;
    }
    public void TeleportOn()
    {
        teleportationArea.enabled = true;
    }
}
