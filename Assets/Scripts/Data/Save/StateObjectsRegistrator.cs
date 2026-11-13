using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateObjectsRegistrator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stateObjects;

    public List<IDescriptable> Descriptables
    {
        get
        {
            if (_descriptables == null)
            {
                _descriptables = _stateObjects.Select(x => x.GetComponent<IDescriptable>()).Where(x => x != null).ToList();
            }
            return _descriptables;
        }
    }
    private List<IDescriptable> _descriptables;

    public List<InteractiveDto> ToDtos()
    {
        List<InteractiveDto> result = new();

        for (int i = 0; i < _stateObjects.Count; i++)
        {
            result.Add(new InteractiveDto { Id = i, Tags = Descriptables[i].GetTags() });
        }

        return result;
    }
    public void FromDtos(List<InteractiveDto> dtos)
    {
        for (int i = 0; i < _stateObjects.Count; i++)
        {
            Descriptables[i].ApplyTags(dtos.First(x => x.Id == i).Tags);
        }
    }

    private void OnValidate()
    {
        if (_stateObjects.Count == 0)
        {
            return;
        }

        if (_stateObjects[^1].GetComponent<IDescriptable>() == null)
        {
            Debug.LogError("Объект должен содержать компонент, реализующий интерфейс IDescriptable.");
        }
    }
}
