using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInsideHutRegistrator : MonoBehaviour, IDescriptable
{
    [SerializeField] private UnityEvent _onPlayerEnter;
    [SerializeField] private UnityEvent _onPlayerExit;

    public bool IsPlayerInside { get; private set; }

    public void SetPlayerInside(bool value)
    {
        IsPlayerInside = value;

        if (value)
        {
            _onPlayerEnter.Invoke();
        }
        else
        {
            _onPlayerExit.Invoke();
        }
    }

    public void ApplyTags(List<string> tags)
    {
        SetPlayerInside(tags[0] == "1");
    }

    public List<string> GetTags()
    {
        return new List<string>
        {
            IsPlayerInside ? "1" : "0"
        };
    }
}
