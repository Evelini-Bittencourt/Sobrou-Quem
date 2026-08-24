using UnityEngine;
using UnityEngine.EventSystems;

public class PecaRotacionavel : MonoBehaviour, IPointerClickHandler
{
    private float tempoUltimoClique = 0f;

    [SerializeField] private float intervaloDuploClique = 0.4f;

    private int rotacaoAtual = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        float tempoAtual = Time.time;

        // Verifica se foi um segundo clique rápido
        if (tempoAtual - tempoUltimoClique <= intervaloDuploClique)
        {
            GirarPeca();

            // Reseta para não contar um terceiro clique como continuação
            tempoUltimoClique = 0f;
        }
        else
        {
            tempoUltimoClique = tempoAtual;
        }
    }

    private void GirarPeca()
    {
        rotacaoAtual += 90;

        if (rotacaoAtual >= 360)
        {
            rotacaoAtual = 0;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, rotacaoAtual);
    }
}