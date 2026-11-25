using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class QuestLauncher
{
    private HashSet<IQuest> _quests;
    private IQuest.Factory _factory;

    public QuestLauncher(IQuest.Factory factory)
    {
        _factory = factory;
        _quests = new HashSet<IQuest>();
    }

    public void TryLaunch(QuestInfo info)
    {
        IQuest quest = _quests.FirstOrDefault(x => x?.Info == info);

        if (quest == null)
        {
            quest = _factory.Create(info);
            quest.OnFinished += OnQuestFinished;
            _quests.Add(quest);
        }

        quest.TryStart();

        Debug.Log(_quests.Count);
    }

    private void OnQuestFinished(object sender, System.EventArgs e)
    {
        if (sender is IQuest quest)
        {
            _quests.Remove(quest);
            Debug.Log($"Quest {quest} finished.");
        }
    }
}

/*
 * ----- ВНЕШНИЙ КОД -----
 * 1. Из внешнего кода вызывается TryStart(QuestInfo info) --- Например, при спавне (вызывается для QuestLauncher)
 * 
 * ----- ОБРАБОТКА КВЕСТОВ -----
 *  Есть ли экземпляр исполняемого квеста для этого QuestInfo? --- Сверяет QuestLauncher
 *  Нет:
 * 2. Создается экземпляр исполняемого Quest --- Создает QuestLauncher
 * 
 * ----- ЛОГИКА КВЕСТА -----
 * 3. Проверка условий: если все условия исполняются, то приступаем к шагу квеста 1. --- Quest
 * 4. Ожидаем завершение квеста --- Quest
 * 
 *  Есть ли еще шаги? --- Quest
 *  Да:
 * 5. Запускаем следующий, возврат к пункту 4 --- Quest
 * 
 * 6. Вызываем обработчики завершения квеста --- Quest
 * 7. Завершаем квест --- Quest
 *
 */