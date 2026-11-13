using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : InteractableObject, IDescriptable
{
    public event Action OnDialogComplete;

    [SerializeField] private List<CompanionSerializable> _companions;
    [SerializeField] private DialogConfig _dialog;

    public int Id => this.GetId();

    public override void Interact(InteractionData data)
    {
        DialogManager.Instance.StartDialog(_dialog, _companions);

        DialogManager.Instance.OnDialogEnd.AddListener(OnDialogEnd);

        void OnDialogEnd()
        {
            _dialog = _dialog?.NextDialog;
            IsActive = _dialog != null;

            DialogManager.Instance.OnDialogEnd.RemoveListener(OnDialogEnd);
            OnDialogComplete?.Invoke();
        }
    }

    public virtual List<string> GetTags()
    {
        if (_dialog == null)
        {
            return new List<string>();
        }

        return new List<string>()
        {
            _dialog.name
        };
    }

    public virtual void ApplyTags(List<string> tags)
    {
        if (tags.Count > 0)
        {
            while (_dialog != null && _dialog.name != tags[0])
            {
                _dialog = _dialog.NextDialog;
            }
        }

        IsActive = _dialog != null;
    }
}
