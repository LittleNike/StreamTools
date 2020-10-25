using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System.Text;

public class UI : MonoBehaviour
{
    string path = Path.Combine(Application.streamingAssetsPath, "CreatorList.json");
    List<Creator> creators;

    [SerializeField] private GameObject _addPanel;
    [SerializeField] private GameObject _createCopyPastaPanel;
    private AddCreatorPanel _addCreatorPanel;
    [SerializeField] private GameObject _template;

    [SerializeField] private TMP_InputField _timeText;
    [SerializeField] private TMP_InputField _gameText;

    private void Start()
    {
        _addCreatorPanel = _addPanel.GetComponent<AddCreatorPanel>();
        creators = CreatorUtilities.ReadJson(path).ToList();
        _addPanel.SetActive(true);
        _addPanel.SetActive(false);
        _template.SetActive(false);
        _createCopyPastaPanel.SetActive(false);

        foreach (Creator c in creators)
            CreatePanelForCreator(c);
    }

    public void AddButtonClicked()
    {
        _addCreatorPanel._nameField.text = "";
        _addCreatorPanel._platformField.text = "";
        _addPanel.SetActive(true);
    }
    public void AddCreatorClicked()
    {
        Creator c = new Creator(_addCreatorPanel._nameField.text, _addCreatorPanel._platformField.text);
        creators.Add(c);
        CreatePanelForCreator(c, true);
        _addPanel.SetActive(false);
    }

    public void CreatePanelForCreator(Creator c, bool sort = false)
    {
        GameObject g = Instantiate(_template, _template.transform.parent);
        CreatorTemplate gs = g.GetComponent<CreatorTemplate>();
        gs.Delete.onClick.AddListener(delegate { DeleteCreator(g, c); });
        gs.SetValues(c.tName, c.tPlatform.ToString());
        c.tToggle = false;
        gs.Toggle.onValueChanged.AddListener(delegate { c.tToggle = gs.Toggle.isOn; });

        g.name = c.tName;
        if(sort)
            SortChildren(_template.transform.parent.gameObject);
        g.SetActive(true);
    }

    private void SortChildren(GameObject g)
    {
        List<GameObject> children = new List<GameObject>();
        for(int i = 0; i < g.transform.childCount; i++)
            children.Add(g.transform.GetChild(i).gameObject);
        children = children.OrderBy(o => o.name).ToList();
        foreach(GameObject child in children)
            child.transform.SetAsLastSibling();
    }

    public void DeleteCreator(GameObject g, Creator c)
    {
        Destroy(g);
        creators.Remove(c);
    }

    public void CreateCopyPastaClicked()
    {
        string game = _gameText.text;
        string time = _timeText.text;

        StringBuilder s = new StringBuilder();
        if (game.Length > 0)
            s.Append(game);
        if (time.Length > 0)
            s.Append($" at {time}");
        if(creators.Where(o => o.tToggle).Count() > 0)
            s.Append($" with {string.Join(" | ", creators.Where(o => o.tToggle).Select(o => o.ToString()))}");

        _createCopyPastaPanel.SetActive(true);
        _createCopyPastaPanel.GetComponent<Clipboard>().SetText(s.ToString());
    }

    private void OnDisable()
    {
        CreatorUtilities.WriteJson(path, creators.OrderBy(o => o.tName).ToArray());
    }
}
