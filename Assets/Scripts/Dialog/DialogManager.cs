using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogWindowManager dialogWindowManager;
    [SerializeField] DialogConfig dialogConfig;

    DialogConfig _currentDialog;
    int _currentIndex;

    void OnEnable()
    {
        InputSystemManager.OnTouch += ShowDialog;
    }

    void OnDisable()
    {
        InputSystemManager.OnTouch -= ShowDialog;

    }

    void Start()
    {
        StartDialog(dialogConfig, Vector2.zero);
    }

    public void StartDialog(DialogConfig dialog, Vector2 position)
    {
        _currentDialog = dialog;
        dialogWindowManager.ShowWindow(position);

        ShowDialog();
    }

    void ShowDialog()
    {
        if (_currentDialog.dialog.Count <= _currentIndex)
        {
            dialogWindowManager.HideWindow();
            return;
        }

        LogSerializable log = _currentDialog.dialog[_currentIndex];
        dialogWindowManager.SetLog(log);
        _currentIndex++;
    }
}
