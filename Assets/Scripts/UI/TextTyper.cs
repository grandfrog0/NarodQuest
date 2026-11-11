using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public class TextTyper
{
    [SerializeField] private TMP_Text _target;
    [SerializeField, Min(0.1f)] private float _speed = 1f;

    private Coroutine _routine;
    private string _currentText;

    public bool IsTyping { get; private set; }

    public void Start(string text)
    {
        if (_target == null)
        {
            Debug.LogError("Target text component cannot be null!");
            return;
        }

        _currentText = text;

        if (_target.gameObject.activeSelf && _target.gameObject.activeInHierarchy)
        {
            _routine = _target.StartCoroutine(TypingRoutine(text));
        }
    }

    public void Finish()
    {
        if (_target == null)
        {
            Debug.LogError("Target text component cannot be null!");
            return;
        }

        if (_routine != null)
        {
            _target.StopCoroutine(_routine);
            _routine = null;
        }

        _target.text = _currentText;

        IsTyping = false;
    }

    private IEnumerator TypingRoutine(string text)
    {
        WaitForSeconds period = new WaitForSeconds(1f / _speed);

        IsTyping = true;
        _target.text = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            _target.text += text[i];
            yield return period;
        }

        IsTyping = false;
    }
}
