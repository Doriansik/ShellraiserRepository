using UnityEngine;
using UnityEngine.UI; // Potrzebne do pracy z UI

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // èrÛd≥o düwiÍku
    [SerializeField] private Slider volumeSlider; // Slider do zmiany g≥oúnoúci

    private void Awake()
    {
        if (audioSource != null)
        {
            audioSource.Play();

            // Wczytaj zapisany poziom g≥oúnoúci lub ustaw domyúlny (0.5f)
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            audioSource.volume = savedVolume;

            if (volumeSlider != null)
            {
                volumeSlider.value = savedVolume; // Zaktualizuj suwak
                volumeSlider.onValueChanged.AddListener(SetVolume); // Dodaj listener
            }
        }
    }



    // Funkcja do ustawiania g≥oúnoúci
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume; // Ustaw g≥oúnoúÊ
            PlayerPrefs.SetFloat("MusicVolume", volume); // Zapisz ustawienie
        }
    }
}

