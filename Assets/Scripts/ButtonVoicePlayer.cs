using UnityEngine;

public class ButtonVoicePlayer : MonoBehaviour
{
    [Header("Áudio de Narração")]
    [SerializeField] private AudioClip audioVoiceClip; // Arraste o arquivo .wav aqui
    [SerializeField] private AudioSource audioSource;    // O AudioSource que vai tocar o som

    private void Awake()
    {
        // Se você não arrastar um AudioSource no Inspector, ele tenta pegar um do próprio objeto
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Se ainda não existir, cria um automaticamente para evitar erros
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura o AudioSource para usar o canal de Voz do seu Audio Mixer (se configurado)
        audioSource.playOnAwake = false;
    }

    // Método que será acionado quando o botão for clicado
    public void TocarVoz()
    {
        if (audioVoiceClip != null && audioSource != null)
        {
            // Toca o áudio da narração por cima (ideal para cliques de repetição)
            audioSource.PlayOneShot(audioVoiceClip);
        }
        else
        {
            Debug.LogWarning("Faltou configurar o Audio Clip ou o Audio Source no botão " + gameObject.name);
        }
    }
}