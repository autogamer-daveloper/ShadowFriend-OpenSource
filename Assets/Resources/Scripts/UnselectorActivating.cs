using UnityEngine;
using UnityEngine.UI;

public class UnselectorActivating : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private GameObject ButtonUnselector;

    private Button Unselector;

    private void OnEnable()
    {
        ButtonUnselector.SetActive(true);

        Unselector = ButtonUnselector.GetComponent<Button>();
        Unselector.onClick.AddListener(Disable);
    }

    private void Disable()
    {
        ButtonUnselector.SetActive(false);
    }
}
