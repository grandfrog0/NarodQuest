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

            Debug.Log($"Успешно сохранены данные в {FilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка сохранения данных: " + ex);
        }

        void SavePlayer()
        {
            ;

            string json = JsonUtility.ToJson(obj);
            File.WriteAllText(FilePath, json);
        }
        void SaveEnvironment()
        {

        }
        void SaveQuests()
        {

        }
    }

    public void Load()
    {
        try
        {
            string json = File.ReadAllText(FilePath);
            object obj = JsonUtility.FromJson<object>(json);

            // TODO

            Debug.Log($"Успешно загружено сохранение ({json})");
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка чтения данных: " + ex);
        }
    }

    public void Clear()
    {
        try
        {
            File.WriteAllText(FilePath, "");

            Debug.Log($"Успешно очищено сохранение в {FilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка сохранения данных: " + ex);
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