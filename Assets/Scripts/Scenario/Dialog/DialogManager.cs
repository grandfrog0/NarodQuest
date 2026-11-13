using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DialogManager : MonoBehaviour
{
    [SerializeField] UnityEvent _onDialogStart;
    [SerializeField] UnityEvent _onDialogEnd;
    public UnityEvent OnDialogStart => _onDialogStart;
    public UnityEvent OnDialogEnd => _onDialogEnd;

    private InputSystemManager _inputSystemManager;
    private DialogWindowManager _dialogWindowManager;
    [SerializeField] private DialogConfig _dialogConfig;
    [SerializeField] private DialogManagerConfig _dialogManagerConfig;
    [SerializeField] private float _skipDialogCooldown;

    [SerializeField] private List<CompanionSerializable> _baseCompanions;

    private List<CompanionSerializable> _currentCompanions;
    private DialogConfig _currentDialog;
    private int _currentIndex;

    private float _dialogShowTime;

    private Camera _mainCamera;

    [Inject]
    private void Construct(InputSystemManager inputSystemManager, DialogWindowManager dialogWindowManager)
    {
        _inputSystemManager = inputSystemManager;
        _dialogWindowManager = dialogWindowManager;
    }

    void OnEnable()
    {
        _inputSystemManager.OnTouch.AddListener(TryShowDialog);
    }

    private void OnDisable()
    {
        _inputSystemManager.OnTouch.RemoveListener(TryShowDialog);
    }

    private void Start()
    {
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
        if (_dialogWindowManager.IsTyping)
        {
            _dialogWindowManager.FinishTyping();
            return;
        }

        if (_currentDialog == null || _currentIndex >= _currentDialog.dialog.Count)
        {
            _dialogWindowManager.HideWindow();
            OnDialogEnd.Invoke();
            return;
        }

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
