using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private GameObject _selectionOutline;
    [SerializeField] private LayerMask _selectMask;
    private InteractableObject _target;

    private void Start()
    {
        InputSystemManager.OnTouch += OnScreenTouched;
    }

    private void OnScreenTouched(Vector2 screenPos)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screenPos), Vector3.zero, 1, _selectMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == _target.gameObject)
            {
                _target.Interact();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_target == null || collision.gameObject != _target.gameObject)
        {
            if (collision.CompareTag("Interactable"))
            {
                if (!HasTarget || Vector3.Distance(transform.position, _target.transform.position) > Vector3.Distance(transform.position, collision.transform.position))
                {
                    _target = collision.GetComponent<InteractableObject>();

                    _selectionOutline.SetActive(true);
                    _selectionOutline.transform.position = _target.transform.position;
                }
            }
        }
    }

    private bool HasTarget => _target != null && _target.IsActive;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_target != null && collision.gameObject == _target.gameObject)
        {
            _target = null;
            _selectionOutline.SetActive(false);
        }
    }
}
