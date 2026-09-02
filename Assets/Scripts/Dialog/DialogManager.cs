using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    public UnityEvent OnDialogEnd { get; } = new();

    [SerializeField] private DialogWindowManager _dialogWindowManager;
    [SerializeField] private DialogConfig _dialogConfig;
    [SerializeField] private DialogManagerConfig _dialogManagerConfig;

    private List<CompanionSerializable> _currentCompanions;
    private DialogConfig _currentDialog;
    private int _currentIndex;

    private Camera _mainCamera;

    void OnEnable()
    {
        InputSystemManager.OnTouch.AddListener(ShowDialog);
    }

    void OnDisable()
    {
        InputSystemManager.OnTouch.RemoveListener(ShowDialog);
    }

    void Start()
    {
        Instance = this;
        _mainCamera = Camera.main;
    }

    public void StartDialog(DialogConfig dialog, List<CompanionSerializable> companions)
    {
        _currentIndex = 0;
        _currentDialog = dialog;
        _dialogWindowManager.ShowWindow();
        _currentCompanions = companions;

        ShowDialog();
    }

    private void ShowDialog()
    {
        if (_currentDialog == null || _currentIndex >= _currentDialog.dialog.Count)
        {
            _dialogWindowManager.HideWindow();
            OnDialogEnd.Invoke();
            return;
        }

        LogSerializable log = _currentDialog.dialog[_currentIndex];

        Vector2 cloudWindowPosition;

        Transform personTransform = GetTransform(log.person);
        if (personTransform != null)
        {
            Vector2 screenPersonPosition = _mainCamera.WorldToScreenPoint(personTransform.position);
            cloudWindowPosition = screenPersonPosition + _dialogManagerConfig.cloudWindowOffsset;
        }
        else
        {
            cloudWindowPosition = _dialogManagerConfig.cloudWindowOffsset;
        }

        _dialogWindowManager.SetLog(log, cloudWindowPosition);
        _currentIndex++;
    }

    private Transform GetTransform(PersonConfig person) => _currentCompanions?.FirstOrDefault(x => x.person == person)?.companion;
}
