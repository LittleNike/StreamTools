using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreatorTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _platformText;
    public Button Delete;
    public Toggle Toggle;

    public void SetValues(string name, string platform)
    {
        _nameText.text = name;
        _platformText.text = platform;
    }
}
