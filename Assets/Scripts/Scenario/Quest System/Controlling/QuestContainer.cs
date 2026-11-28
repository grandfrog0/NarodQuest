using System.Collections.Generic;
using System.Linq;

public class QuestContainer
{
    public IEnumerable<IQuest> Quests => _quests;
    private HashSet<IQuest> _quests;
    private IQuest.Factory _factory;

    public QuestContainer(IQuest.Factory factory)
    {
        _quests = new();
        _factory = factory;
    }

    public IQuest Get(QuestInfo info)
    {
        EnsureCreated(info);
        IQuest quest = _quests.FirstOrDefault(x => x.Info == info);

        return quest;
    }

    private void EnsureCreated(QuestInfo info)
    {
        if (!_quests.Any(x => x.Info == info))
        {
            IQuest quest = _factory.Create(info);
            _quests.Add(quest);
        }
    }
}