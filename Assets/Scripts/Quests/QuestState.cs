using UnityEngine;

public enum QuestState
{
    None,
    /// НавеститьБабку
    VisitTheGranny,
    // ПолучитьСписокИнгридиентов
    GetIngredientsList,
    // ПрийтиКДоярке
    ComeToTheMilkmaid,
    // ВзятьКосу,
    TakeTheScythe,
    // ПокоситьТравуДоярке,
    CutTheGrass,
    // ВзятьВедро,
    BringTheBucket,
    // ПодоитьКорову,
    MilkTheCow,
    // ОтнестиВедро,
    CarryTheBucket,
    // ЗабратьБутылкуМолока,
    TakeTheBottleOfMilk,
    // ПрийтиНаПоле,
    ComeToTheField,
    // СходитьККрестьянину,
    GoToTheFarmer,
    // ВзятьКарту,
    TakeTheMap,
    // ДойдиДоЛоговаРазбойников,
    ReachTheLair,
    // ПопытатьсяОбманутьРазбойников,
    TryToTrickTheRobbers,
    // УкрастьЗолото,
    StealTheGold,
    // ОбменятьЗолотоНаКостюм,
    ExchangeTheGold,
    // ПройтиКТравеМураве,
    PassThroughTheGrass,
    // ВернутьсяВИзбу,
    ReturnToTheHut,
    // ПопробоватьСваритьКашу,
    TryToCookThePorridge,
    // ПойтиКДровосеку,
    GoToTheWoodcutter,
    // ЗабратьТопор,
    TakeTheAxe,
    // ДобавитьТопорВКашу,
    AddTheAxeToThePorridge,
    // Финал
    End
}
