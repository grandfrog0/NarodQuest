using TMPro;
using UnityEngine;

public class DialogWindowManager : MonoBehaviour
{
    [SerializeField] RectTransform window;
    [SerializeField] TextMeshProUGUI person;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] DialogManagerConfig config;

    public void ShowWindow(Vector2 position)
    {
        window.gameObject.SetActive(true);
        window.anchoredPosition = position;
    }
    public void HideWindow()
    {
        window.gameObject.SetActive(false);
    }


    public void SetLog(LogSerializable log)
    {
        person.text = config.GetPersonName(log.person);
        message.text = log.message;
    }
}
