using UnityEngine;
using TMPro;

public class AddCreatorPanel : MonoBehaviour
{
    public TMP_InputField _nameField;
    public TMP_InputField _platformField;

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
