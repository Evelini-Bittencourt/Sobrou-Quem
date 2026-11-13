using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Gui;

public class FluxoJogarManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private LeanWindow modalTutorial; // Arraste o objeto "Modal" aqui
    [SerializeField] private VideoPlayerController videoController; // Arraste o objeto que tem o VideoPlayerController aqui

    [Header("Próxima Cena")]
    [SerializeField] private string nomeCenaNiveis = "2-tela-selecao-nivel"; // Nome exato da sua cena de níveis

    private const string chaveTutorial = "TutorialJaFoiVisto";

    // LIGUE ESTE MÉTODO NO BOTÃO "JOGAR" DA TELA INICIAL
    public void AoClicarEmJogar()
    {
        int jaViuTutorial = PlayerPrefs.GetInt(chaveTutorial, 0);

        if (jaViuTutorial == 0)
        {
            // Primeira vez: Abre o modal do Lean GUI e dá o play no vídeo
            if (modalTutorial != null)
                modalTutorial.TurnOn();

            if (videoController != null)
                videoController.TocarVideo();
        }
        else
        {
            // Já viu antes: Pula o tutorial e vai direto para a seleção de níveis!
            SceneManager.LoadScene(nomeCenaNiveis);
        }
    }

    // LIGUE ESTE MÉTODO NO BOTÃO "JOGAR" QUE APARECE NO FINAL DO VÍDEO (DENTRO DO MODAL)
    public void ConcluirTutorialEJogar()
    {
        // Salva na memória que o tutorial já foi visto (1 = Sim)
        PlayerPrefs.SetInt(chaveTutorial, 1);
        PlayerPrefs.Save();

        // Vai para a cena de seleção de níveis
        SceneManager.LoadScene(nomeCenaNiveis);
    }
}