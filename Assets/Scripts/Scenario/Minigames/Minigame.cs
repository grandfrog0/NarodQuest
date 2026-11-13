using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour, IDescriptable
{
    public bool IsCompleted { get; set; }
    public int Id => this.GetId();

    public virtual List<string> GetTags()
    {
        return new List<string>
        {
            IsCompleted ? "1" : "0"
        };
    }

    public virtual void ApplyTags(List<string> tags)
    {
        IsCompleted = tags[0] == "1";
    }
}
