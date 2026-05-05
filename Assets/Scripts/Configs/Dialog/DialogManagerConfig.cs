using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogManagerConfig", menuName = "Scriptable Objects/DialogManagerConfig")]
public class DialogManagerConfig : ScriptableObject
{
    public List<PersonSerializable> personsSerializable;

    public string GetPersonName(Person person)
    {
        foreach (PersonSerializable personSerializable in personsSerializable)
        {
            if (personSerializable.person == person)
                return personSerializable.name;
        }

        throw new ArgumentException($"Отсутствует person {person}");
    }
}
