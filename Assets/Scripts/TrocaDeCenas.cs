using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrocaDeCenas : MonoBehaviour
{
    public void TrocarCena(string qualCena)
    {
        StartCoroutine(TrocarCenaComDelay(qualCena));
    }

    private IEnumerator TrocarCenaComDelay(string qualCena)
    {
        yield return new WaitForSeconds(0.2f); 

        SceneManager.LoadScene(qualCena);
    }
}