using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "QuestInstaller", menuName = "SO/Quests/Quest Installer")]
public class QuestInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<QuestLauncher>().AsSingle();
        Container.BindFactory<QuestInfo, IQuest, IQuest.Factory>().To<Quest>();
    }
}
