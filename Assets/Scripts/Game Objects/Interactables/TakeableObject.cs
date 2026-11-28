using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

/// <summary>
/// Объект, контролирующий поведение PlayerBringable
/// </summary>
[RequireComponent(typeof(PlayerBringable))]
public class TakeableObject : InteractableObject, IDescriptable
{
    public PlayerBringable Bringable => _bringable;
    public override bool IsActive { get => _bringable.IsActive; set => _bringable.IsActive = value; }
    public int Id => this.GetId();

    private PlayerBringable _bringable;
    private BringableObjectController _bringableController;

    [Inject]
    private void Construct(BringableObjectController bringableController)
    {
        _bringableController = bringableController;
    }

    protected override void HandleInteraction(InteractionData data)
    {
        bool isBrought = _bringableController.SwitchBring(_bringable, data.Point);
        IsActive = !isBrought;
    }

    private void Awake()
    {
        _bringable = GetComponent<PlayerBringable>();
    }

    public List<string> GetTags()
    {
        if (_bringableController.Current == Bringable)
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
