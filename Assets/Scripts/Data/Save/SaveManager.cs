using System;
using System.IO;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    private string FolderPath => Application.persistentDataPath;
    private string PlayerPath => Path.Combine(FolderPath, "player.json");
    private string EnvironmentPath => Path.Combine(FolderPath, "environment.json");
    private string QuestsPath => Path.Combine(FolderPath, "quests.json");

    private StateSaver _stateSaver;

    [Inject]
    private void Construct(StateSaver stateSaver)
    {
        _stateSaver = stateSaver;
    }

    public void Save()
    {
        try
        {
            PlayerSave playerSave = _stateSaver.CreatePlayerSave();
            string playerSaveJson = JsonUtility.ToJson(playerSave, true);

            EnvironmentDto environmentSave = _stateSaver.CreateEnvironmentSave();
            string environmentSaveJson = JsonUtility.ToJson(environmentSave, true);

            QuestSave questSave = _stateSaver.CreateQuestSave();
            string questSaveJson = JsonUtility.ToJson(questSave, true);

            File.WriteAllText(PlayerPath, playerSaveJson);
            File.WriteAllText(EnvironmentPath, environmentSaveJson);
            File.WriteAllText(QuestsPath, questSaveJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка сохранения данных: {ex}");
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(PlayerPath) || !File.Exists(EnvironmentPath) || !File.Exists(QuestsPath))
            {
                Debug.LogWarning("Файлы сохранения не были найдены.");
                return;
            }

            string playerSaveJson = File.ReadAllText(PlayerPath);
            PlayerSave playerSave = JsonUtility.FromJson<PlayerSave>(playerSaveJson);

            string environmentSaveJson = File.ReadAllText(EnvironmentPath);
            EnvironmentDto environmentSave = JsonUtility.FromJson<EnvironmentDto>(environmentSaveJson);

            string questSaveJson = File.ReadAllText(QuestsPath);
            QuestSave questSave = JsonUtility.FromJson<QuestSave>(questSaveJson);

            _stateSaver.ApplyPlayerSave(playerSave);
            _stateSaver.ApplyEnvironmentSave(environmentSave);
            _stateSaver.ApplyQuestSave(questSave);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка загрузки данных: {ex}");
        }
    }

    public void Clear()
    {
        try
        {
            File.Delete(PlayerPath);
            File.Delete(EnvironmentPath);
            File.Delete(QuestsPath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка удаления данных: " + ex);
        }
    }
}

/*
 * ПП СОЧНИКИ
 * 
 * Тесто:
 * Творог - 150г
 * Яйцо - 1
 * Сметана - 1 столовая ложка
 * Разрыхлитель - 1 чайная ложка
 * Мука - 100-120г
 * Яичный желток - 1 (для смазывания)
 * сахарозаменитель - по вкусу
 * Соль - по вкусу
 * В холодильник на 15 минут
 * 
 * Начинка:
 * Творог - 120г
 * Сметана - 1 ст ложка
 * СахЗам - по вкусу
 * Яичный белок - 1
 * 
 * В духовку (180гр) на 25 минут
 * */