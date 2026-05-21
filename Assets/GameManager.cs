using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;

    [Header("Movimento do jogador")]
    [SerializeField] private Behaviour[] locomotionComponents;

    [Header("Interação de menu")]
    [SerializeField] private Behaviour[] menuInteractionComponents;

    private bool gameStarted = false;
    private bool gameEnded = false;

    private void Start()
    {
        gameStarted = false;
        gameEnded = false;

        if (startMenu != null)
        {
            startMenu.SetActive(true);
        }

        if (endMenu != null)
        {
            endMenu.SetActive(false);
        }

        SetLocomotionEnabled(false);
        SetMenuInteractionEnabled(true);
    }

    public void StartGame()
    {
        gameStarted = true;
        gameEnded = false;

        if (startMenu != null)
        {
            startMenu.SetActive(false);
        }

        SetLocomotionEnabled(true);
        SetMenuInteractionEnabled(false);
    }

    public void EndGame()
    {
        if (!gameStarted || gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (endMenu != null)
        {
            endMenu.SetActive(true);
        }

        SetLocomotionEnabled(false);
        SetMenuInteractionEnabled(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetLocomotionEnabled(bool enabled)
    {
        foreach (Behaviour component in locomotionComponents)
        {
            if (component != null)
            {
                component.enabled = enabled;
            }
        }
    }

    private void SetMenuInteractionEnabled(bool enabled)
    {
        foreach (Behaviour component in menuInteractionComponents)
        {
            if (component != null)
            {
                component.enabled = enabled;
            }
        }
    }
}