using TMPro;
using UnityEngine;

public class FuelTankUi : MonoBehaviour
{
    [SerializeField] private GameObject texts;
    [SerializeField] private TextMeshProUGUI maxFuelInTankText;
    [SerializeField] private TextMeshProUGUI fuelInTankText;

    private void Awake()
    {
        texts.SetActive(false);
    }

    public void ActiveTrue()
    {
        texts.SetActive(true);
    }
    public void ActiveFalse()
    {
        texts.SetActive(false);
    }

    public void UpdateUi(float maxFuelInTank, float fuelInTank)
    {
        maxFuelInTankText.text = (int)maxFuelInTank + "";
        fuelInTankText.text = (int)fuelInTank + "";
    }
}
