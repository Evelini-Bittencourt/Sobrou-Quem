using System.Collections;
using UnityEngine;

public class VoiceNarrator : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Configuração de Atraso")]
    [Tooltip("Tempo em segundos que o jogo espera antes de começar a falar após entrar na tela.")]
    [SerializeField] private float tempoDeEspera = 1.0f; 

    private void Awake()
    {
        // Pega o componente AudioSource que está no mesmo objeto
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Inicia a contagem do tempo antes de tocar a narração
        if (audioSource != null && audioSource.clip != null)
        {
            StartCoroutine(TocarNarracaoComDelay());
        }
        else
        {
            Debug.LogWarning("Nenhum AudioSource ou Audio Clip encontrado no Narrador!");
        }
    }

    private IEnumerator TocarNarracaoComDelay()
    {
        // Espera os segundos definidos no Inspector (ex: 1 segundo para a criança se orientar na tela)
        yield return new WaitForSeconds(tempoDeEspera);

        // Toca o áudio da narração
        audioSource.Play();
    }
}