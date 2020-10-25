using UnityEngine;
using TMPro;

public class Clipboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetText(string s)
    {
        _text.text = s;
    }

    public void CopyToClipboard()
    {
        TextEditor te = new TextEditor();
        te.text = _text.text;
        te.SelectAll();
        te.Copy();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
