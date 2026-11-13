using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные окружения
/// </summary>
public class EnvironmentDto
{
    /// <summary>
    /// ID подобранных предметов
    /// </summary>
    public List<int> RaisedObjectsIds;

    /// <summary>
    /// Состояние интерактива (включая Npc)
    /// </summary>
    public List<InteractiveDto> InteractiveDatas;

    /// <summary>
    /// Данные объектов состояния
    /// </summary>
    public List<InteractiveDto> StateObjects;
}
