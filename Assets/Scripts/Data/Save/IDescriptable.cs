using System.Collections.Generic;
using UnityEngine;

public interface IDescriptable
{
    int Id => ((MonoBehaviour)this).GetId();
    List<string> GetTags();
    void ApplyTags(List<string> tags);
}
