using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogWindowManager : MonoBehaviour
{
    [SerializeField] RectTransform cloudWindow;
    [SerializeField] GameObject bottomPanel;
    [SerializeField] TextMeshProUGUI person;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] DialogManagerConfig config;

    public void ShowWindow()
    {
        bottomPanel.gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        bottomPanel.gameObject.SetActive(false);
    }

    public void SetLog(LogSerializable log, Vector2 position)
    {
        person.text = config.GetPersonName(log.person);
        message.text = log.message;
        cloudWindow.anchoredPosition = position;

    }
}
