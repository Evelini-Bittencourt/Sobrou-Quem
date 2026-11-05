using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeParameterName = "VolumeMusica";

    // ESTE MÉTODO RODA ANTES DE QUALQUER COISA NO JOGO (Mesmo antes da Cena 1 carregar!)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InicializarVolumeGlobal()
    {
        // Como é um método estático, buscamos o volume salvo e aplicamos se houver um mixer padrão,
        // mas o nosso MusicManager na cena vai cuidar disso logo em seguida.
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // FORÇA A APLICAÇÃO DO VOLUME ANTES DE TUDO
        AplicarVolumeSalvoIMEDIATO();
    }

    private void Start()
    {
        // Garante aplicação logo no início também
        AplicarVolumeSalvoIMEDIATO();
    }

    private void AplicarVolumeSalvoIMEDIATO()
    {
        if (audioMixer != null)
        {
            // Pega o valor salvo no PlayerPrefs (ou 0.75f como padrão)
            float savedVolume = PlayerPrefs.GetFloat(volumeParameterName, 0.75f);
            
            // Converte para decibéis e aplica no Mixer de forma forçada
            float volume = Mathf.Log10(Mathf.Clamp(savedVolume, 0.0001f, 1f)) * 20;
            audioMixer.SetFloat(volumeParameterName, volume);
        }
    }
}