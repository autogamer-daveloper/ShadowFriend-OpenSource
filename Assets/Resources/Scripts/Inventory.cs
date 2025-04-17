using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header ("Settings - Scriptable objects")]
    [SerializeField] private ItemBuilder[] item;

    [Space (5)]
    [Header ("Settings - UI")]
    [SerializeField] private Text[] Count;

    /*
     ВАЖНОЕ ПРИМЕЧАНИЕ, длинна Count и item должно быть одинаковым!
    */

    private void OnEnable()
    {
        DrawInventory();
    }

    private void DrawInventory()
    {
        for(int i = 0; i < item.Length; i++)
        {
            int x = PlayerPrefs.GetInt(item[i].PlayerPrefsKey, 0);
            Count[i].text = x.ToString();
        }
    }
}
