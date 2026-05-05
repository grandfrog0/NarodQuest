using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "SO/Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;
}
