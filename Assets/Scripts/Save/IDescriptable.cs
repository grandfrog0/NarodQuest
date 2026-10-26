using System.Collections.Generic;
using UnityEngine;

public interface IDescriptable
{
    int Id { get; }
    List<string> GetTags();
    void ApplyTags(List<string> tags);
}
