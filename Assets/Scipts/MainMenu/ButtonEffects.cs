using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // ���������� ��� ������� ����
using DG.Tweening;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // ��������� ����������
{
    public Button button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.transform.DOScale(1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.DOScale(1f, 0.2f);
    }

}
