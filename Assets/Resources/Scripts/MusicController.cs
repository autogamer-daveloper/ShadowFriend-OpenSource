using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float savedValue = PlayerPrefs.GetFloat("savedSoundValue", 1f);

        volumeSlider.value = savedValue;
        audioSource.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("savedSoundValue", value);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
