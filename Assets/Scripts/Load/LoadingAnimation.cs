using UnityEngine;
using TMPro;
using DG.Tweening;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text tipText;

    string[] tips =
    {
        "Следите за ценой топлива — она меняется каждый день!",
        "Клиенты недовольны, если ждать слишком долго.",
        "Бонус! Иногда клиенты оставляют чаевые.",
        "Не забывайте пополнять склад продуктами.",
        "Новые технологии помогут улучшить бизнес!",
        "Проверьте камеру слежения — может быть вор?",
        "Иногда случаются неожиданные события ночью."
    };

    void Start()
    {
        ChangeTip();
    }

    void ChangeTip()
    {
        Sequence tipSequence = DOTween.Sequence();

        tipSequence.AppendCallback(() => tipText.text = "Совет: " + tips[Random.Range(0, tips.Length)])
                   .AppendInterval(3f) // Меняем совет каждые 3 секунды
                   .SetLoops(-1);
    }
}
