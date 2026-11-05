using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        // Garante que só exista uma única música tocando no jogo inteiro
        if (instance == null)
        {
            instance = this;
            // Faz com que este objeto NÃO seja destruído ao carregar novas cenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existir um gerenciador de música em outra cena, destrói este duplicado
            Destroy(gameObject);
        }
    }
}