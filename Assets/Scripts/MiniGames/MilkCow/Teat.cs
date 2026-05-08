using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Teat : MonoBehaviour, /*IDragHandler,*/ IEndDragHandler
{
    [SerializeField] int index;
    [SerializeField] UdderManager udder;
    [SerializeField] MilkCowConfig config;
    [SerializeField] ParticleSystem milkParticles;

    Image _teatImage;
    float _time;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        _teatImage = GetComponent<Image>();
    }

    void Update()
    {
        Debug.Log(udder.IsShowing || !IsPointerOverImage(InputSystemManager.CurrentTouchPosition));
        if (udder.IsShowing || !IsPointerOverImage(InputSystemManager.CurrentTouchPosition))
        {
            milkParticles.Stop();
            return;
        }

        _time += Time.deltaTime;
        if (InputSystemManager.PositionDelta.y >= 0)
            milkParticles.Stop();
        else if (InputSystemManager.PositionDelta.y < -0.5f)
            milkParticles.Play();
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    _time += Time.deltaTime;
    //    if (eventData.delta.y >= 0)
    //        milkParticles.Stop();
    //    else if (eventData.delta.y < -0.5f)
    //        milkParticles.Play();

    //    //Debug.Log(eventData.delta);
    //}

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_time >= config.teatMinTimeMilking)
            udder.TeatMilked(index);

        milkParticles.Stop();
        _time = 0f;
    }

    bool IsPointerOverImage(Vector2 screenPosition)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = screenPosition;

        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == _teatImage.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator SetTeatColor(Color targetColor)
    {
        float elapsed = 0f;
        float duration = config.showingTeatDuration / 2;
        Color startColor = _teatImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            _teatImage.color = Color.Lerp(startColor, targetColor, elapsed / duration);

            yield return null;
        }
    }

    public IEnumerator ShowTeat()
    {
        yield return SetTeatColor(config.selectColor);
        yield return SetTeatColor(config.baseColor);
    }
}
