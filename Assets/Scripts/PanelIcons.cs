using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelIcons : MonoBehaviour
{

    [SerializeField] private Sprite checkMark;
    [SerializeField] private Sprite questionMark;

    [Header("Icons")]
    [SerializeField] private Image bertrandIcon;
    [SerializeField] private Image humeIcon;
    [SerializeField] private Image immanuelIcon;
    [SerializeField] private Image lockeIcon;
    [SerializeField] private Image reneIcon;
    [SerializeField] private Image rousseauIcon;

    private Dictionary<string, Image> iconMap = new();
    private Dictionary<string, Image> keywordIconMap = new();

    // Start is called before the first frame update
    void Start()
    {
        iconMap.Add("Bertrand", bertrandIcon);
        iconMap.Add("Hume", humeIcon);
        iconMap.Add("Immanuel", immanuelIcon);
        iconMap.Add("Locke", lockeIcon);
        iconMap.Add("Rene", reneIcon);
        iconMap.Add("Rousseau", rousseauIcon);
    }

    public void AddNewKeywordIconMap(string keyword, Image iconImageComponent)
    {
        Debug.Log(keyword + " / " + iconImageComponent);
        keywordIconMap.Add(keyword, iconImageComponent);
    }

    public void RefreshKeywordStatusIcons(string NPCname)
    {
        Debug.Log(NPCname);
        
        foreach (string keyword in keywordIconMap.Keys)
        {
            keywordIconMap[keyword].sprite = GetIcon(NPCname, keyword);
        }
        
    }



    public void RefreshNPCStatusIcons(string keyword)
    {
        foreach (string name in iconMap.Keys)
        {
            iconMap[name].sprite = GetIcon(name, keyword);
        }
    }

    private Sprite GetIcon(string name, string keyword)
    {
        var entry = JournalManager.GetInstance().journalData.GetKeywordEntry(name, keyword);

        if (entry.Found)
        {
            return checkMark;
        }
        else if (entry.ConfusedResponseFound)
        {
            return questionMark;
        }
        else
        {
            return null;
        }
    }
}
