using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValidadorDesafio : MonoBehaviour
{
    public enum TipoAnimal
    {
        Abelha,
        Caracol,
        Joaninha,
        Borboleta,
        Sapo
    }

    [System.Serializable]
    public struct MetaAnimal
    {
        public TipoAnimal animal;
        public int quantidadeEsperada;
    }

    [Header("Objetivos da Carta Atual")]
    [SerializeField] private List<MetaAnimal> metasDaCarta = new List<MetaAnimal>();

    [Header("Pontos de Encaixe do Tabuleiro (Snaps)")]
    [Tooltip("Arraste os 4 objetos Snap (Snap 1, Snap 2, Snap 3, Snap 4) para aqui")]
    [SerializeField] private List<Transform> pontosSnap = new List<Transform>();

    [Header("Cenas de Transição")]
    [SerializeField] private string cenaParabens = "6-tela-parabens";
    [SerializeField] private string cenaTenteNovamente = "7-tela-tente-novamente";

    [Header("Identificação do Nível e Carta")]
    [Tooltip("Ex: Facil, Medio, Dificil, MuitoDificil")]
    [SerializeField] private string nomeDoNivel = "MuitoDificil"; 
    [SerializeField] private int numeroDaCarta = 1;

    public void VerificarVitoria()
    {
        Physics2D.SyncTransforms();

        // 1. REQUISITO 1: Todas as 4 peças precisam estar encaixadas no tabuleiro
        if (!TodasPecasNoTabuleiro())
        {
            Debug.Log("[Validador] DERROTA: Nem todas as peças foram colocadas no tabuleiro!");
            SceneManager.LoadScene(cenaTenteNovamente);
            return;
        }

        bool acertouTudo = true;

        // 2. REQUISITO 2: Verificar as metas da carta (ex: Joaninha = 6)
        foreach (MetaAnimal meta in metasDaCarta)
        {
            int visiveis = ContarAnimaisVisiveis(meta.animal);
            Debug.Log($"[Validador] Meta {meta.animal} | Visíveis: {visiveis} | Esperado: {meta.quantidadeEsperada}");

            if (visiveis != meta.quantidadeEsperada)
            {
                acertouTudo = false;
            }
        }

        // 3. REQUISITO 3: Garantir que NENHUM outro animal indesejado esteja visível
        foreach (TipoAnimal tipo in Enum.GetValues(typeof(TipoAnimal)))
        {
            bool ehMeta = metasDaCarta.Exists(m => m.animal == tipo);

            if (!ehMeta)
            {
                int visiveisIncorretos = ContarAnimaisVisiveis(tipo);
                if (visiveisIncorretos > 0)
                {
                    Debug.Log($"[Validador] DERROTA: Animal indesejado visível: {tipo} ({visiveisIncorretos} visíveis)");
                    acertouTudo = false;
                }
            }
        }

        if (acertouTudo)
        {
            string chaveSave = $"CartaConcluida_{nomeDoNivel}_{numeroDaCarta}";
            PlayerPrefs.SetInt(chaveSave, 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(cenaParabens);
        }
        else
        {
            Debug.Log(">> DERROTA!");
            SceneManager.LoadScene(cenaTenteNovamente);
        }
    }

    private bool TodasPecasNoTabuleiro()
    {
        GameObject[] pecas = GameObject.FindGameObjectsWithTag("PecaVerde");

        if (pecas.Length < 4) return false;

        foreach (GameObject peca in pecas)
        {
            bool encaixada = false;

            // Verifica se a peça está próxima de pelo menos um dos 4 pontos Snap do tabuleiro
            foreach (Transform snap in pontosSnap)
            {
                if (snap != null && Vector2.Distance(peca.transform.position, snap.position) < 50f)
                {
                    encaixada = true;
                    break;
                }
            }

            // Se alguma peça verde estiver longe dos Snaps (fora do tabuleiro), falha
            if (!encaixada)
            {
                return false;
            }
        }

        return true;
    }

    private int ContarAnimaisVisiveis(TipoAnimal tipo)
    {
        int contador = 0;

        GameObject[] animais = GameObject.FindGameObjectsWithTag(tipo.ToString());
        GameObject[] pecas = GameObject.FindGameObjectsWithTag("PecaVerde");

        foreach (GameObject animal in animais)
        {
            if (!animal.activeInHierarchy) continue;

            bool coberto = false;
            Vector2 posAnimal = animal.transform.position;

            foreach (GameObject peca in pecas)
            {
                if (!peca.activeInHierarchy) continue;

                PolygonCollider2D colisorPeca = peca.GetComponent<PolygonCollider2D>();

                if (colisorPeca != null && colisorPeca.OverlapPoint(posAnimal))
                {
                    coberto = true;
                    break;
                }
            }

            if (!coberto)
            {
                contador++;
            }
        }

        return contador;
    }
}