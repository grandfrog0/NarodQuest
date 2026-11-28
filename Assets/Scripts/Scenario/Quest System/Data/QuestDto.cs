using System;

[Serializable]
public class QuestDto
{
    public string Id;
    public QuestState State;
    public int Step;

    public QuestDto(string id, QuestState state, int step)
    {
        Id = id;
        State = state;
        Step = step;
    }
}
