using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Объект, контролирующий поведение PlayerBringable
/// </summary>
[RequireComponent(typeof(PlayerBringable))]
public class TakeableObject : InteractableObject, IDescriptable
{
    private Collider2D _collider;

    public PlayerBringable Bringable => _bringable;

    public int Id => this.GetId();

    private PlayerBringable _bringable;

    public override void Interact(InteractionData data)
    {
        bool isTaken = BringableObjectController.Instance.SwitchBring(_bringable);
        _collider.isTrigger = isTaken;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _bringable = GetComponent<PlayerBringable>();
    }

    public List<string> GetTags()
    {
        if (BringableObjectController.Instance.Current == Bringable)
        {
            return new List<string>();
        }
        else
        {
            return new List<string>
            {
                transform.position.x.ToString(),
                transform.position.y.ToString()
            };
        }
    }

    public void ApplyTags(List<string> tags)
    {
        if (tags.Count >= 2)
        {
            transform.position = new Vector3(float.Parse(tags[0]), float.Parse(tags[1]));
        }
    }
}
