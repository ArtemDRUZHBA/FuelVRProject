using UnityEngine;
using UnityEngine.UI;

public class SettingButtonsEffect : MonoBehaviour, IButtonEffect
{
    [SerializeField] private Color activeColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private float activeScale = 0.9f;

    public void ApplyEffect(Button button, bool isActive)
    {
        button.transform.localScale = isActive ? Vector3.one * activeScale : Vector3.one;
        button.image.color = isActive ? activeColor : defaultColor;
    }
}
