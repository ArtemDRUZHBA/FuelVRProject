using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private string fuelSocketTag;
    [SerializeField] private string fuelTankTag;
    private bool inHand;

    public void InHand()
    {
        inHand = true;
    }

    public void Drop()
    {
        inHand = false;
    }

    public void HoverEntered()
    {
        Debug.Log("Hover Entered");
    }

    public void HoverExited()
    {
        Debug.Log("HoverExited");
    }
    public void SelectEntered()
    {
        Debug.Log("SelectEntered");
    }
    public void SelectExited()
    {
        Debug.Log("SelectExited");
    }
    public void FocusEntered()
    {
        Debug.Log("FocusEntered");
    }
    public void FocusExited()
    {
        Debug.Log("FocusExited");
    }
    public void Activated()
    {
        Debug.Log("Activated");
    }
    public void Deactivated()
    {
        Debug.Log("Deactivated");
    }
}
