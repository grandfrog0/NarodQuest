using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "QuestInstaller", menuName = "SO/Quests/Quest Installer")]
public class QuestInstaller : ScriptableObjectInstaller
{
    [SerializeField] private QuestDatabase _questDatabase;

    public override void InstallBindings()
    {
        Container.Bind<QuestContainer>().AsSingle();
        Container.Bind<QuestLauncher>().AsSingle();
        Container.Bind<QuestStateHandler>().AsSingle();

        Container.BindFactory<QuestInfo, IQuest, IQuest.Factory>().To<Quest>();

        Container.BindInstance(_questDatabase);
    }
}
