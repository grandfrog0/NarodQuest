using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRegistrator : MonoBehaviour
{
    [Header("Interactables")]
    [SerializeField] private List<DroppedItem> _droppedItems;
    [SerializeField] private List<TakeableObject> _takeables;
    [SerializeField] private List<NPC> _npc;
    [SerializeField] private List<InteractableObject> _otherInteractables;

    [Header("Other")]
    [SerializeField] private List<Minigame> _minigames;
    [SerializeField] private List<GameObject> _misc;

    private List<GameObject> Registrated
    {
        get
        {
            if (_registrated == null)
            {
                _registrated = new List<GameObject>();
                _registrated.AddRange(_droppedItems.Select(x => x.gameObject));
                _registrated.AddRange(_takeables.Select(x => x.gameObject));
                _registrated.AddRange(_npc.Select(x => x.gameObject));
                _registrated.AddRange(_otherInteractables.Select(x => x.gameObject));
                _registrated.AddRange(_minigames.Select(x => x.gameObject));
                _registrated.AddRange(_misc);
            }

            return _registrated;
        }
    }
    private List<GameObject> _registrated;

    private void Awake()
    {
        ObjectRegistrationExtension.Construct(this);
    }

    public IEnumerable<GameObject> Enumerate()
    {
        return Registrated.AsEnumerable();
    }

    public int GetId(GameObject gameObject)
    {
        return Registrated.IndexOf(gameObject);
    }
    public GameObject Get(int id)
    {
        if (id >= 0 && id < Registrated.Count)
        {
            return Registrated[id];
        }

        return null;
    }
}
