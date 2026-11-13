using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [Header("Configuração do Botão")]
    [SerializeField] private GameObject botaoJogar; // Arraste o seu "Botão Jogar" do modal aqui

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Chamado pelo botão "Jogar" da tela inicial
    public void TocarVideo()
    {
        if (videoPlayer != null)
        {
            // 1. Esconde o botão de jogar logo no início do vídeo
            if (botaoJogar != null)
                botaoJogar.SetActive(false);

            // 2. Reinicia o vídeo do começo e dá o play
            videoPlayer.time = 0; 
            videoPlayer.Play();   

            // 3. Inicia o monitoramento seguro do término
            StopAllCoroutines();
            StartCoroutine(AguardarFimDoVideo());
        }
    }

    private IEnumerator AguardarFimDoVideo()
    {
        // TRAVA DE SEGURANÇA: Espera o vídeo realmente começar a rodar (evita o falso término do início)
        yield return new WaitUntil(() => videoPlayer.isPlaying);

        // Fica esperando enquanto o vídeo estiver rodando ou até o tempo chegar perto do final
        while (videoPlayer.isPlaying || (videoPlayer.time < videoPlayer.length - 0.2f && videoPlayer.length > 0))
        {
            yield return null;
        }

        // Quando o vídeo termina de verdade, ativa o botão
        if (botaoJogar != null)
        {
            botaoJogar.SetActive(true);
            Debug.Log("Vídeo terminou! Botão de jogar liberado.");
        }
    }

    // Método para parar o vídeo quando fechar o modal
    public void PararVideo()
    {
        if (videoPlayer != null)
        {
            StopAllCoroutines();
            videoPlayer.Stop();
        }
    }

    // ATALHO MÁGICO NOS 3 PONTINHOS DO INSPECTOR:
    // Clique nos 3 pontinhos do script na Unity e selecione "Resetar Tutorial" para testar de novo!
    [ContextMenu("Resetar Tutorial")]
    public void ResetarTutorial()
    {
        PlayerPrefs.DeleteKey("TutorialJaFoiVisto");
        PlayerPrefs.Save();
        Debug.Log("Status do tutorial resetado! O próximo clique em 'Jogar' abrirá o vídeo novamente.");
    }
}