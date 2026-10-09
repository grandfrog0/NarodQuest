using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSave
{
    // 2. Окружение (поднятые предметы, интерактив, нпс)

    public List<DroppedItem> RaisedObjects;

    // Интерактив
    public bool IsGateOpened;

    // Статусы NPC
    public List<NpcStatus> NpcStatuses;
}
