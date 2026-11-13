using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerTranslator : MonoBehaviour
{
    public UnityEvent OnTransitionComplete;

    [SerializeField] private PlayerTranslator _other;
    [SerializeField] private Vector3 _offset;
    public Vector3 TargetPosition => transform.position + _offset;

    private TransitionManager _transitionManager;
    private CinemachineCameraController _cameraController;

    [Inject]
    private void Construct(TransitionManager transitionManager, CinemachineCameraController cameraController)
    {
        _transitionManager = transitionManager;
        _cameraController = cameraController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            Teleport(collision.transform);
        }
    }

    public void Teleport(Transform player)
    {
        _transitionManager.Transition(0.5f, CompleteTeleport);

        void CompleteTeleport()
        {
            OnTransitionComplete.Invoke();

            Vector3 playerPosition = player.position;
            player.position = _other.TargetPosition;
            _cameraController.Move(_other.TargetPosition - playerPosition);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, TargetPosition);
    }
}
