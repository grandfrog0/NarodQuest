using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerDistanceController : MonoBehaviour
{
    [SerializeField] private List<DistanceControlledObject> _controlledObjects;
    private Transform _player;

    [Inject]
    private void Construct(GameObject player)
    {
        _player = player.transform;
    }

    private void Start()
    {
        StartCoroutine(UpdateRoutine());
    }
    private IEnumerator UpdateRoutine()
    {
        WaitForSeconds period = new WaitForSeconds(1f);

        while (true)
        {
            foreach (DistanceControlledObject obj in _controlledObjects)
            {
                obj.Object.SetActive(Vector2.Distance(obj.Object.transform.position, _player.position) < obj.Distance);
            }

            yield return period;
        }
    }
}
