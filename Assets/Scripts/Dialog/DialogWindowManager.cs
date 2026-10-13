using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogWindowManager : MonoBehaviour
{
    [SerializeField] RectTransform cloudWindow;
    [SerializeField] GameObject bottomPanel;
    [SerializeField] TextMeshProUGUI person;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] DialogManagerConfig config;

    public void ShowWindow()
    {
        bottomPanel.gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        bottomPanel.gameObject.SetActive(false);
    }

    public void SetLog(LogSerializable log, Vector2 position)
    {
        person.text = log.person.Name;
        message.text = log.message;
        cloudWindow.anchoredPosition = position;
    }
}

/*
 * ПП СИННАБОНЫ РЕЦЕПТ ОНЛАЙН БЕСПЛАТНО
 * 
 * Булочки
 * 1. 220г творога, 2 яйца, 150г муки, разрыхлитель, сахарозаменитель, соль, ванилин
 * 2. Намешиваем творожное тесто, делаем тонкий квадратный пласт на столе, обсыпаем корицей, натертым яблоком, кукурузным крахмалом
 * 3. Сворачиваем в рулет и нарезаем на 8 кусков (булочек)
 * 4. Слегка прижимаем и ставим в духовку на полчаса при температуре 180 градусов
 * 
 * Крем
 * 1. Маленькая упаковка сливочного творожного сыра, 2 столовые ложки греческого йогурта, далее по вкусу добавляем пп сироп/сгущенку/сахзамы
 * 2. Горячие булочки обливаем кремом
 * 3. ВКУСНО. ЕДИМ.
 */