using UnityEngine;
using UnityEngine.Video;

public class TutorialVideoController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject botaoJogar;

    private bool videoEstaRodando = false;

    private void Awake()
    {
        Debug.Log("1. O script TutorialVideoController foi inicializado (Awake).");
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();
    }

    // Chamado pelo botão "Jogar" da tela inicial
    public void TocarVideo()
    {
        Debug.Log("2. O botão chamou a função TocarVideo com sucesso!");
        
        if (videoPlayer != null && botaoJogar != null)
        {
            botaoJogar.SetActive(false);
            videoPlayer.time = 0;
            videoPlayer.Play();
            videoEstaRodando = true;
            Debug.Log("3. Comando Play enviado ao Video Player.");
        }
        else
        {
            Debug.LogError("ERRO: O VideoPlayer ou o BotaoJogar estão vazios (None) no Inspector do script!");
        }
    }

    private void Update()
    {
        if (videoEstaRodando && videoPlayer != null)
        {
            if (videoPlayer.length > 0 && (videoPlayer.time >= videoPlayer.length - 0.1f || !videoPlayer.isPlaying && videoPlayer.time > 0.5f))
            {
                Debug.Log("4. O vídeo terminou de rodar.");
                videoEstaRodando = false;
                LiberarBotaoJogar();
            }
        }
    }

    private void LiberarBotaoJogar()
    {
        if (botaoJogar != null)
        {
            botaoJogar.SetActive(true);
            Debug.Log("5. Botão de Jogar ativado na tela.");
        }
    }
}