using UnityEngine;
using UnityEngine.UI;

public class Zavod : MonoBehaviour
{
    [Header ("Settings - Standart")]
    [Range (15, 60)]
    [SerializeField] private int oneItemPerTime = 15;
    [SerializeField] private int ItemsInZavod;
    [SerializeField] private bool StartFreeItem = false;
    [SerializeField] private ItemBuilder whatMaterial;
    [SerializeField] private GameObject Selected;
    [SerializeField] private GameObject GoingTo;

    [Space (5)]
    [Header ("Settings - UI")]
    [SerializeField] private Button _Button;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject Portrait;
    [SerializeField] private Text[] CountInZavod;

    [HideInInspector] public PlayerMovement TempPlayer;

    private void Start()
    {
        _Button.onClick.AddListener(ClickOnTheButton);
        _Button.onClick.AddListener(UnselectThis);
        ItemsInZavod = PlayerPrefs.GetInt(whatMaterial.PlayerPrefsKeyZavod, 0);
        ShowTexts();
        if(StartFreeItem)
        {
            AddItemToZavod();
        }

        Invoke(nameof(AddTimedItemToZavod), oneItemPerTime);
    }

#region Zavod-chane

    private void AddTimedItemToZavod()
    {
        AddItemToZavod();
        Invoke(nameof(AddTimedItemToZavod), oneItemPerTime);
    }

    private void AddItemToZavod()
    {
        ItemsInZavod++;
        SaveItems();
    }

    private void SaveItems()
    {
        PlayerPrefs.SetInt(whatMaterial.PlayerPrefsKeyZavod, ItemsInZavod);
        ShowTexts();
    }

#endregion

#region Elite-UI

    private void ShowTexts()
    {
        foreach(Text count in CountInZavod)
        {
            count.text = ItemsInZavod.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GoingToZavod(false);
            SelectThis();
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.CompareTag("Player"))
    //     {
    //         HideButtons();
    //     }
    // }

    public void GoingToZavod(bool Something)
    {
        GoingTo.SetActive(Something);
    }

    public void SelectThis()
    {
        ShowButtons();
        Selected.SetActive(true);
    }

    public void UnselectThis()
    {
        HideButtons();
        Selected.SetActive(false);
        GoingToZavod(false);
    }

    private void ShowButtons()
    {
        Panel.SetActive(true);
        Portrait.SetActive(true);
        ShowTexts();
    }

    private void HideButtons()
    {
        Panel.SetActive(false);
        Portrait.SetActive(false);
    }

    public void ClickOnTheButton()
    {
        int temp = PlayerPrefs.GetInt(whatMaterial.PlayerPrefsKey, 0);
        temp = temp + ItemsInZavod;
        PlayerPrefs.SetInt(whatMaterial.PlayerPrefsKey, temp);

        ItemsInZavod = 0;
        PlayerPrefs.SetInt(whatMaterial.PlayerPrefsKeyZavod, 0);
        ShowTexts();

        HideButtons();
    }

#endregion
}
