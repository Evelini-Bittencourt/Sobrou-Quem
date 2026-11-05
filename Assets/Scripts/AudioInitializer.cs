using UnityEngine;
using UnityEngine.Audio;

public class AudioInitializer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        // Pega os valores salvos (ou 0.75f se for a primeira vez jogando)
        float savedMusica = PlayerPrefs.GetFloat("VolumeMusica", 0.75f);
        float savedSons = PlayerPrefs.GetFloat("VolumeSons", 0.75f);
        float savedVoz = PlayerPrefs.GetFloat("VolumeVoz", 0.75f);

        // Aplica imediatamente no Audio Mixer
        AplicarVolume("VolumeMusica", savedMusica);
        AplicarVolume("VolumeSons", savedSons);
        AplicarVolume("VolumeVoz", savedVoz);
    }

    private void AplicarVolume(string parameterName, float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(parameterName, volume);
    }
}