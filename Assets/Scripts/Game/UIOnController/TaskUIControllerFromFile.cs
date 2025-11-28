using UnityEngine;
using TMPro; // если используешь TextMeshPro
using System.IO;

public class TaskUIControllerFromFile : MonoBehaviour
{
    [Header("UI элемент для текста задания")]
    [SerializeField] private TextMeshProUGUI taskText;

    [Header("Файл с заданиями (Resources/tasks.txt)")]
    [SerializeField] private string tasksFileName = "Tasks";
    // без расширения, файл должен лежать в папке Resources

    private string[] tasks;
    private int currentTaskIndex = 0;

    private void Start()
    {
        LoadTasksFromFile();
        ShowCurrentTask();
    }

    private void LoadTasksFromFile()
    {
        TextAsset taskFile = Resources.Load<TextAsset>(tasksFileName);
        if (taskFile != null)
        {
            // Разбиваем по строкам
            tasks = taskFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            Debug.LogError("Файл с заданиями не найден в Resources: " + tasksFileName);
            tasks = new string[0];
        }
    }

    public void CompleteTask()
    {
        currentTaskIndex++;

        if (tasks != null && currentTaskIndex < tasks.Length)
        {
            ShowCurrentTask();
        }
        else
        {
            taskText.text = "Все задания выполнены!";
        }
    }

    private void ShowCurrentTask()
    {
        if (tasks != null && tasks.Length > 0 && currentTaskIndex < tasks.Length)
        {
            taskText.text = tasks[currentTaskIndex];
        }
    }
}
