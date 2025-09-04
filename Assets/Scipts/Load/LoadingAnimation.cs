using UnityEngine;
using TMPro;
using DG.Tweening;

public class LoadingAnimation : MonoBehaviour
{
    public TMP_Text loadingText;
    public TMP_Text tipText;

    string[] tips =
    {
        "������� �� ����� ������� � ��� �������� ������ ����!",
        "������� ����������, ���� ����� ������� �����.",
        "�����! ������ ������� ��������� ������.",
        "�� ��������� ��������� ����� ����������.",
        "����� ���������� ������� �������� ������!",
        "��������� ������ �������� � ����� ���� ���?",
        "������ ��������� ����������� ������� �����."
    };

    void Start()
    {
        StartLoadingAnimation();
        ChangeTip();
    }

    void StartLoadingAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => loadingText.text = "��������.")
                .AppendInterval(0.5f)
                .AppendCallback(() => loadingText.text = "��������..")
                .AppendInterval(0.5f)
                .AppendCallback(() => loadingText.text = "��������...")
                .AppendInterval(0.5f)
                .SetLoops(-1);
    }

    void ChangeTip()
    {
        Sequence tipSequence = DOTween.Sequence();

        tipSequence.AppendCallback(() => tipText.text = "�����: " + tips[Random.Range(0, tips.Length)])
                   .AppendInterval(3f) // ������ ����� ������ 3 �������
                   .SetLoops(-1);
    }
}
