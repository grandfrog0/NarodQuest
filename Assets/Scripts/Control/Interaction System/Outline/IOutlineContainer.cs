using UnityEngine;
using UnityEngine.UI;

public interface IOutlineContainer
{
    Transform transform { get; }
    OutlineConfig Outline { get; }
}