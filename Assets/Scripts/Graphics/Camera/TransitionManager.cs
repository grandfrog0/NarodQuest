using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Image _fade;
    private Coroutine _coroutine;

    public bool Transition(float seconds = 0.5f, Action onComplete = null)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(TransitionRoutine(seconds, onComplete));
            return true;
        }
        return false;
    }

    private IEnumerator TransitionRoutine(float seconds, Action onComplete)
    {
        _fade.gameObject.SetActive(true);
        _fade.color = Color.clear;

        for (float t = 0; t <= 1; t += Time.deltaTime * 2)
        {
            _fade.color = Color.Lerp(Color.clear, Color.black, t);
            yield return null;
        }

        _fade.color = Color.black;

        onComplete?.Invoke();

        yield return new WaitForSeconds(seconds);
        
        for (float t = 0; t <= 1; t += Time.deltaTime * 2)
        {
            _fade.color = Color.Lerp(Color.black, Color.clear, t);
            yield return null;
        }

        _fade.gameObject.SetActive(false);

        _coroutine = null;
    }
}
