using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTranslator : MonoBehaviour
{
    public UnityEvent OnTransitionComplete;

    [SerializeField] private PlayerTranslator _other;
    [SerializeField] private Vector3 _offset;
    public Vector3 TargetPosition => transform.position + _offset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            Teleport(collision.transform);
        }
    }

    public void Teleport(Transform player)
    {
        TransitionManager.Transition(0.5f, CompleteTeleport);

        void CompleteTeleport()
        {
            OnTransitionComplete.Invoke();

            Vector3 playerPosition = player.position;
            player.position = _other.TargetPosition;
            CinemachineCameraController.Instance.Move(_other.TargetPosition - playerPosition);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, TargetPosition);
    }
}
