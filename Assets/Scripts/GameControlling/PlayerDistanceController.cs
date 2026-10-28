using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<DistanceControlledObject> _controlledObjects;

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
