using UnityEngine;

public class CustomerPhrasesLoader : MonoBehaviour
{
    private string[] phrases;

    void Start()
    {
        // ��������� ��������� ���� customer_phrases.txt �� ����� Resources
        TextAsset textAsset = Resources.Load<TextAsset>("customer_phrases"); // ��� ����� ��� ����������
        if (textAsset != null)
        {
            // ��������� �� ������ �����, ������ ������ ������
            phrases = textAsset.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
    }

    // ����� ��� ��������� ��������� �����
    public string GetRandomPhrase()
    {
        if (phrases == null || phrases.Length == 0)
            return "��� ����������� ����!";

        int index = Random.Range(0, phrases.Length);
        return phrases[index];
    }
}
