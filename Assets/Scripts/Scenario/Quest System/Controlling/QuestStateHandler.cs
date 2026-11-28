using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestStateHandler
{
    private QuestContainer _questContainer;
    private QuestDatabase _questDatabase;

    [Inject]
    public QuestStateHandler(QuestContainer questContainer, QuestDatabase questDatabase)
    {
        _questContainer = questContainer;
        _questDatabase = questDatabase;
    }

    public void ApplySave(QuestSave questSave)
    {
        foreach (QuestDto questDto in questSave.Quests)
        {
            QuestInfo info = _questDatabase.GetById(questDto.Id);
            IQuest quest = _questContainer.Get(info);

            if (quest != null)
            {
                quest.Initialize(questDto.State, questDto.Step);
            }
        }
    }

    public QuestSave CreateSave()
    {
        QuestSave questSave = new QuestSave();

        foreach (IQuest quest in _questContainer.Quests)
        {
            questSave.Quests.Add(new QuestDto(quest.Info.Id, quest.State, quest.CurrentStep));
        }

        return questSave;
    }
}