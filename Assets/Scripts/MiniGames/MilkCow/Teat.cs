using UnityEngine;
using UnityEngine.EventSystems;

public class Teat : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] int index;
    [SerializeField] UdderManager udder;
    [SerializeField] MilkCowConfig config;
    [SerializeField] ParticleSystem milkParticles;

    float _time;

    void Update()
    {
        if (InputSystemManager.PositionDelta.y == 0)
            milkParticles.Stop();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _time += Time.deltaTime;
        if (eventData.delta.y >= 0)
            milkParticles.Stop();
        else if (eventData.delta.y < -0.5f)
            milkParticles.Play();

        Debug.Log(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_time >= config.teatMinTimeMilking)
            udder.TeatMilked(index);

        milkParticles.Stop();
        _time = 0f;
    }
}
