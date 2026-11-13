using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FormattedText : MonoBehaviour
{
    [SerializeField] private string _format = "{0}";
    private TMP_Text Text
    {
        get => _text ?? (_text = GetComponent<TMP_Text>());
    }
    private TMP_Text _text;

    public void Clear()
    {
        Text.text = "";
    }
    public void Clear(string text)
    {
        Text.text = text;
    }
    public void SetValue(int value)
    {
        Text.text = string.Format(_format, value);
    }
    public void SetValue(float value)
    {
        Text.text = string.Format(_format, value);
    }
    public void SetValue(object value)
    {
        Text.text = string.Format(_format, value);
    }
    public void SetValue<T>(T value)
    {
        Text.text = string.Format(_format, value);
    }

    public void SetValues(params object[] values)
    {
        Text.text = string.Format(_format, values);
    }
}
