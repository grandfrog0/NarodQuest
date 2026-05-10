using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogWindowManager dialogWindowManager;
    [SerializeField] DialogConfig dialogConfig;
    [SerializeField] DialogManagerConfig dialogManagerConfig;

    Dictionary<Person, Transform> currentCompanions;
    DialogConfig _currentDialog;
    int _currentIndex;

    Camera _mainCamera;

    void OnEnable()
    {
        InputSystemManager.OnTouch.AddListener(ShowDialog);
    }

    void OnDisable()
    {
        InputSystemManager.OnTouch.AddListener(ShowDialog);

    }

    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void StartDialog(DialogConfig dialog, Dictionary<Person, Transform> companions)
    {
        _currentDialog = dialog;
        dialogWindowManager.ShowWindow();
        currentCompanions = companions;

        ShowDialog();
    }

    void ShowDialog()
    {
        if (_currentDialog == null || _currentDialog.dialog.Count <= _currentIndex)
        {
            dialogWindowManager.HideWindow();
            return;
        }

        LogSerializable log = _currentDialog.dialog[_currentIndex];

        Vector2 screenPersonPosition = _mainCamera.WorldToScreenPoint(currentCompanions[log.person].position);
        Vector2 cloudWindowPosition = screenPersonPosition + dialogManagerConfig.cloudWindowOffsset;

        dialogWindowManager.SetLog(log, cloudWindowPosition);
        _currentIndex++;
    }
}
