using UnityEngine;
using UnityEngine.UI;

public class FactoryResetManager : MonoBehaviour
{
    [Header("Sliders de Configuração (Opcional)")]
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSom;
    [SerializeField] private Slider sliderVoz;

    [Header("Níveis do Jogo para Limpar as Cartas")]
    [Tooltip("Coloque exatamente os nomes dos níveis que você usa no GerenciadorMenuEspecialista")]
    [SerializeField] private string[] nomesDosNiveis = { "Facil", "Medio", "Dificil", "MuitoDificil" };

    // LIGUE ESTE MÉTODO NO BOTÃO DE RESET DA TELA DE CONFIGURAÇÕES
    public void ResetarJogoParaDeFábrica()
    {
        // 1. REINICIA O TUTORIAL (Apaga a chave para o vídeo voltar a aparecer na próxima vez)
        PlayerPrefs.DeleteKey("TutorialJaFoiVisto");

        // 2. APAGA O PROGRESSO DAS CARTAS DE TODOS OS NÍVEIS (1 a 10)
        foreach (string nivel in nomesDosNiveis)
        {
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.DeleteKey($"CartaConcluida_{nivel}_{i}");
            }
        }

        // 3. RESTAURA O VOLUME PADRÃO NOS SLIDERS E APAGA AS CHAVES DE ÁUDIO
        // (Ajuste o valor padrão se preferir, ex: 1f para o volume máximo)
        if (sliderMusica != null) sliderMusica.value = 1f;
        if (sliderSom != null) sliderSom.value = 1f;
        if (sliderVoz != null) sliderVoz.value = 1f;

        PlayerPrefs.DeleteKey("VolumeMusica");
        PlayerPrefs.DeleteKey("VolumeSom");
        PlayerPrefs.DeleteKey("VolumeVoz");

        // Salva todas as alterações na memória
        PlayerPrefs.Save();

        Debug.Log("Sucesso: Jogo restaurado para o padrão de fábrica!");
    }
}