using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private ConfirmButtonController _confirmCont;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        SendToConfirmController();
    }

    public void SendToConfirmController()
    {
        int sliderValue = (int)_slider.value;
        _confirmCont.SetFuelCount(sliderValue);
        _countText.text = sliderValue + "";
    }
}
