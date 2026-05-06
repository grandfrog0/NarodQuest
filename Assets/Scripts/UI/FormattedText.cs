using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FormattedText : MonoBehaviour
{
    [SerializeField] private string _format = "{0}";
    private TMP_Text _text;

    public void SetValue(object value)
    {
        _text.text = string.Format(_format, value);
    }
    public void SetValue<T>(T value)
    {
        _text.text = string.Format(_format, value);
    }

    public void SetValues(params object[] values)
    {
        _text.text = string.Format(_format, values);
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
}
