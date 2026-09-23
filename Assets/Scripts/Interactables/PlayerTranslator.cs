using UnityEngine;

public class PlayerTranslator : MonoBehaviour
{
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
            player.position = _other.TargetPosition;
            Camera.main.transform.position = Camera.main.GetComponent<CameraController>().TargetPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, TargetPosition);
    }
}
