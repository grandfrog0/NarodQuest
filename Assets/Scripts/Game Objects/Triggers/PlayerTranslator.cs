using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerTranslator : MonoBehaviour
{
    public UnityEvent OnTransitionComplete;

    [SerializeField] private PlayerTranslator _other;
    [SerializeField] private Vector3 _offset;
    public PlayerTranslator Other => _other;
    public Vector3 TargetPosition => transform.position + _offset;
    public Location Location { get; private set; }

    private TransitionManager _transitionManager;
    private PlayerLocationHandler _playerLocationHandler;

    public void Initialize(Location location)
    {
        Location = location;
    }

    [Inject]
    private void Construct(TransitionManager transitionManager, PlayerLocationHandler playerLocationHandler)
    {
        _transitionManager = transitionManager;
        _playerLocationHandler = playerLocationHandler;
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
            _playerLocationHandler.Current = _other.Location;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, TargetPosition);
    }
}
