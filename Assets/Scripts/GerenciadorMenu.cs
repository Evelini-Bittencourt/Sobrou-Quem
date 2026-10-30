using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorMenuEspecialista : MonoBehaviour
{
    [System.Serializable]
    public struct ElementosCarta
    {
        public Button botaoCarta;              // O botão da ficha (ex: ficha9)
        public Image painelFundo;               // O componente Image do 'Panel'
        public GameObject textInterrogacao;    // O objeto do texto com "?"
        public GameObject textDesafio;         // O objeto do texto com a meta (ex: 3 caracóis...)
    }
    [Header("Configuração do Nível")]
    [Tooltip("Digite o mesmo nome configurado no Validador (Ex: Facil, Medio, Dificil, MuitoDificil)")]
    [SerializeField] private string nomeDoNivel = "MuitoDificil";

    [Header("Barra de Progresso")]
    [SerializeField] private Slider sliderProgresso;
    [SerializeField] private Text textoProgresso;

    [Header("Cores dos Cartões")]
    [SerializeField] private Color corCartaoOculto = new Color(0.82f, 0.8f, 0.76f); // Cinza
    [SerializeField] private Color corCartaoRevelado = new Color(0.72f, 0.22f, 0.2f); // Vermelho

    [Header("Lista das 10 Cartas")]
    [SerializeField] private ElementosCarta[] cartas;

    private void Start()
    {
        AtualizarInterfaceEProgresso();
    }

    public void AtualizarInterfaceEProgresso()
    {
        int totalConcluidas = 0;

        for (int i = 0; i < cartas.Length; i++)
        {
            int numeroCarta = i + 1;
            
            string chaveSave = $"CartaConcluida_{nomeDoNivel}_{numeroCarta}";
            bool estaConcluida = PlayerPrefs.GetInt(chaveSave, 0) == 1;

            if (estaConcluida)
            {
                totalConcluidas++;

                // CARTÃO REVELADO (Ganha a cor Vermelha)
                if (cartas[i].painelFundo != null) cartas[i].painelFundo.color = corCartaoRevelado;
                if (cartas[i].textDesafio != null) cartas[i].textDesafio.SetActive(true);
                if (cartas[i].textInterrogacao != null) cartas[i].textInterrogacao.SetActive(false);
            }
            else
            {
                // CARTÃO OCULTO (Fica Cinza com a "?")
                if (cartas[i].painelFundo != null) cartas[i].painelFundo.color = corCartaoOculto;
                if (cartas[i].textDesafio != null) cartas[i].textDesafio.SetActive(false);
                if (cartas[i].textInterrogacao != null) cartas[i].textInterrogacao.SetActive(true);
            }

            if (cartas[i].botaoCarta != null)
            {
                cartas[i].botaoCarta.interactable = true;
            }
        }

        // Atualiza a barra de progresso
        if (textoProgresso != null) textoProgresso.text = $"{totalConcluidas} / 10";
        if (sliderProgresso != null)
        {
            sliderProgresso.value = totalConcluidas; // O Slider trata de ajustar o tamanho exato de 0 a 10!
        }
    }

    [ContextMenu("Resetar Progresso Deste Nível")]
    public void ResetarProgresso()
    {
        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.DeleteKey($"CartaConcluida_{nomeDoNivel}_{i}");
        }
        AtualizarInterfaceEProgresso();
    }
    }