using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Extinguish : MonoBehaviour
{
    [SerializeField] private string[] keywords = { "потуши", "туши", "убери", "уберай" };
    private GuidedProcedure _speak;

    private void Start()
    {
        _speak = GetComponent<GuidedProcedure>();
    }
    async void Update()
    {
        string result = await _speak.SpeakAsync("");

        CheckString(result);
    }

    private void CheckString(string text)
    {
        if (keywords.Any(word => text.ToLower().Contains(word)))
        {
            //меняем анимацию без цикла и заверешение игры
        }

    }
}
