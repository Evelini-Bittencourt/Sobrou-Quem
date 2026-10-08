using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Gui;

public class SnapPeca : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public RectTransform[] pontosSnap;

    private RectTransform rectTransform;

    private Vector2 posicaoInicial;

    private PontoSnap pontoAtual;

    private LeanDrag drag;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        drag = GetComponent<LeanDrag>();

        posicaoInicial = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // libera a casa antiga
        if (pontoAtual != null)
        {
            pontoAtual.ocupado = false;
            pontoAtual = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform melhorPonto = null;
        float menorDistancia = float.MaxValue;

        foreach (RectTransform ponto in pontosSnap)
        {
            PontoSnap snap = ponto.GetComponent<PontoSnap>();

            if (snap.ocupado)
                continue;

            float distancia = Vector2.Distance(
                rectTransform.anchoredPosition,
                ponto.anchoredPosition
            );

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                melhorPonto = ponto;
            }
        }

        // distância máxima para encaixar
        float distanciaMaxima = 250f;

        if (melhorPonto != null && menorDistancia < distanciaMaxima)
        {
            rectTransform.anchoredPosition =
                melhorPonto.anchoredPosition;

            pontoAtual = melhorPonto.GetComponent<PontoSnap>();
            pontoAtual.ocupado = true;
        }
        else
        {
            rectTransform.anchoredPosition = posicaoInicial;
        }
    }
}