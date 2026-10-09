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

    [Header("Cenas de Transição")]
    [SerializeField] private string cenaParabens = "6-tela-parabens";
    [SerializeField] private string cenaTenteNovamente = "7-tela-tente-novamente";

    public void VerificarVitoria()
    {
        bool acertouTudo = true;

        foreach (MetaAnimal meta in metasDaCarta)
        {
            int visiveis = ContarAnimaisVisiveis(meta.animal);
            Debug.Log($"[Validador] Animal: {meta.animal} | Visíveis contados: {visiveis} | Esperado: {meta.quantidadeEsperada}");

            if (visiveis != meta.quantidadeEsperada)
            {
                acertouTudo = false;
                break;
            }
        }

        if (acertouTudo)
        {
            Debug.Log(">> VITORIA!");
            SceneManager.LoadScene(cenaParabens);
        }
        else
        {
            Debug.Log(">> DERROTA!");
            SceneManager.LoadScene(cenaTenteNovamente);
        }
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

                // Lê o colisor que acabaste de desenhar
                PolygonCollider2D colisorPeca = peca.GetComponent<PolygonCollider2D>();

                if (colisorPeca != null)
                {
                    // Testa se o ponto central do animal cai dentro da área verde ajustada
                    if (colisorPeca.OverlapPoint(posAnimal))
                    {
                        coberto = true;
                        break;
                    }
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