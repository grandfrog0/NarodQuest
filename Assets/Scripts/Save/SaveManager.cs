using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string FolderPath => Application.persistentDataPath;
    private string PlayerPath => Path.Combine(FolderPath, "player.json");
    private string EnvironmentPath => Path.Combine(FolderPath, "environment.json");
    private string QuestsPath => Path.Combine(FolderPath, "quests.json");

    [SerializeField] private StateSaver _stateSaver;

    public void Save()
    {
        try
        {
            SavePlayer();
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка сохранения данных игрока: " + ex);
        }
        try
        {
            SaveEnvironment();
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка сохранения данных: " + ex);
        }
        try
        {
            SaveQuests();
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка сохранения данных: " + ex);
        }

        void SavePlayer()
        {
            PlayerSave playerSave = _stateSaver.CreatePlayerSave();
            string json = JsonUtility.ToJson(playerSave);
            File.WriteAllText(PlayerPath, json);

            Debug.Log($"player save: {json}");
            Debug.Log($"inventory: {JsonUtility.ToJson(playerSave.Inventory)}");
        }
        void SaveEnvironment()
        {
            // TODO
        }
        void SaveQuests()
        {
            // TODO
        }
    }

    public void Load()
    {
        try
        {
            LoadPlayer();
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка загрузки данных игрока: " + ex);
        }

        void LoadPlayer()
        {
            string json = File.ReadAllText(PlayerPath);
            PlayerSave playerSave = JsonUtility.FromJson<PlayerSave>(json);
            _stateSaver.ApplyPlayerSave(playerSave);
        }
        void LoadEnvironment()
        {
            // TODO
        }
        void LoadQuests()
        {
            // TODO
        }
    }

    public void Clear()
    {
        throw new NotImplementedException();
        //try
        //{
        //    File.WriteAllText(FilePath, "");

        //    Debug.Log($"Успешно очищено сохранение в {FilePath}");
        //}
        //catch (Exception ex)
        //{
        //    Debug.LogError("Ошибка сохранения данных: " + ex);
        //}
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