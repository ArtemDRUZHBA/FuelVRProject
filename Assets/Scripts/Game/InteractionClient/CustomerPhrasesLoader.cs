using UnityEngine;

public class CustomerPhrasesLoader : MonoBehaviour
{
    private string[] phrases;

    void Start()
    {
        // Загружаем текстовый файл customer_phrases.txt из папки Resources
        TextAsset textAsset = Resources.Load<TextAsset>("customer_phrases"); // имя файла без расширения
        if (textAsset != null)
        {
            // Разбиваем на массив строк, удаляя пустые строки
            phrases = textAsset.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
    }

    // Метод для получения случайной фразы
    public string GetRandomPhrase()
    {
        if (phrases == null || phrases.Length == 0)
            return "Нет загруженных фраз!";

        int index = Random.Range(0, phrases.Length);
        return phrases[index];
    }
}
