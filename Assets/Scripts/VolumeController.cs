using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSons;
    [SerializeField] private Slider sliderVoz;

    private void Start()
    {
        // 1. Carrega os valores salvos anteriormente (ou usa 0.75f como padrão se for a primeira vez)
        float savedMusica = PlayerPrefs.GetFloat("VolumeMusica", 0.75f);
        float savedSons = PlayerPrefs.GetFloat("VolumeSons", 0.75f);
        float savedVoz = PlayerPrefs.GetFloat("VolumeVoz", 0.75f);

        // 2. Atualiza a posição visual dos Sliders para refletir o volume salvo
        if (sliderMusica != null) sliderMusica.value = savedMusica;
        if (sliderSons != null) sliderSons.value = savedSons;
        if (sliderVoz != null) sliderVoz.value = savedVoz;

        // 3. Aplica o volume de fato no Audio Mixer ao iniciar a cena
        AplicarVolume("VolumeMusica", savedMusica);
        AplicarVolume("VolumeSons", savedSons);
        AplicarVolume("VolumeVoz", savedVoz);

        // 4. Adiciona os ouvintes para quando o jogador mover as barras
        if (sliderMusica != null)
            sliderMusica.onValueChanged.AddListener(val => SalvarEaplicarVolume("VolumeMusica", val));

        if (sliderSons != null)
            sliderSons.onValueChanged.AddListener(val => SalvarEaplicarVolume("VolumeSons", val));

        if (sliderVoz != null)
            sliderVoz.onValueChanged.AddListener(val => SalvarEaplicarVolume("VolumeVoz", val));
    }

    private void SalvarEaplicarVolume(string parameterName, float sliderValue)
    {
        AplicarVolume(parameterName, sliderValue);

        // Salva a preferência no PlayerPrefs para lembrar da próxima vez que abrir o jogo
        PlayerPrefs.SetFloat(parameterName, sliderValue);
        PlayerPrefs.Save();
    }

    private void AplicarVolume(string parameterName, float sliderValue)
    {
        // Converte o valor linear (0 a 1) para Decibéis (-80 a 20) de forma segura
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(parameterName, volume);
    }
}