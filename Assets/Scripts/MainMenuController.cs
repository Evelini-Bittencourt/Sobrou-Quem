using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Método que será chamado quando o botão for clicado
    public void SairDoJogo()
    {
        Debug.Log("O botão de sair foi acionado!");

        // Fecha o jogo caso seja um build (executável)
        Application.Quit();

        // Se estivermos rodando dentro do Editor da Unity, para o modo Play
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}