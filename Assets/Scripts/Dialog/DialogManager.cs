using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] UnityEvent _onDialogStart;
    [SerializeField] UnityEvent _onDialogEnd;
    public UnityEvent OnDialogStart => _onDialogStart;
    public UnityEvent OnDialogEnd => _onDialogEnd;

    [SerializeField] private DialogWindowManager _dialogWindowManager;
    [SerializeField] private DialogConfig _dialogConfig;
    [SerializeField] private DialogManagerConfig _dialogManagerConfig;
    [SerializeField] private float _skipDialogCooldown;

    [SerializeField] private List<CompanionSerializable> _baseCompanions;

    private List<CompanionSerializable> _currentCompanions;
    private DialogConfig _currentDialog;
    private int _currentIndex;

    private float _dialogShowTime;

    private Camera _mainCamera;

    void OnEnable()
    {
        InputSystemManager.OnTouch.AddListener(TryShowDialog);
    }

    void OnDisable()
    {
        InputSystemManager.OnTouch.RemoveListener(TryShowDialog);
    }

    void Start()
    {
        Instance = this;
        _mainCamera = Camera.main;
    }

    public void StartDialog(DialogConfig dialog, List<CompanionSerializable> companions = null)
    {
        _currentIndex = 0;
        _currentDialog = dialog;
        _dialogWindowManager.ShowWindow();
        _currentCompanions = companions ?? _baseCompanions;

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

        Debug.Log(_currentDialog);
        OnDialogStart.Invoke();

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

    private void TryShowDialog()
    {
        if (Time.time >= _dialogShowTime + _skipDialogCooldown)
        {
            ShowDialog();
            _dialogShowTime = Time.time;
        }
    }

    private Transform GetTransform(PersonConfig person) => _currentCompanions?.FirstOrDefault(x => x.person == person)?.companion;
}
