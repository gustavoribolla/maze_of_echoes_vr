using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;

    [Header("Menu Ray")]
    [SerializeField] private GameObject menuRay;

    [Header("Movimento do jogador")]
    [SerializeField] private Behaviour[] locomotionComponents;

    [Header("Timer")]
    [SerializeField] private TMP_Text finalTimeText;

    private bool gameStarted = false;
    private bool gameEnded = false;

    private float startTime;
    private float finalTime;

    private void Start()
    {
        gameStarted = false;
        gameEnded = false;

        if (startMenu != null)
            startMenu.SetActive(true);

        if (endMenu != null)
            endMenu.SetActive(false);

        if (finalTimeText != null)
            finalTimeText.text = "";

        SetLocomotionEnabled(false);
        SetMenuRayEnabled(true);
    }

    public void StartGame()
    {
        Debug.Log("StartGame chamado");

        gameStarted = true;
        gameEnded = false;

        startTime = Time.time;

        if (startMenu != null)
            startMenu.SetActive(false);

        if (finalTimeText != null)
            finalTimeText.text = "";

        SetLocomotionEnabled(true);
        SetMenuRayEnabled(false);
    }

    public void EndGame()
    {
        Debug.Log("EndGame chamado");

        if (!gameStarted || gameEnded)
            return;

        gameEnded = true;

        finalTime = Time.time - startTime;
        UpdateFinalTimeText();

        if (endMenu != null)
            endMenu.SetActive(true);

        SetLocomotionEnabled(false);
        SetMenuRayEnabled(true);
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

    private void UpdateFinalTimeText()
    {
        if (finalTimeText == null)
            return;

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);

        finalTimeText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    private void SetLocomotionEnabled(bool enabled)
    {
        foreach (Behaviour component in locomotionComponents)
        {
            if (component != null)
                component.enabled = enabled;
        }
    }

    private void SetMenuRayEnabled(bool enabled)
    {
        if (menuRay != null)
        {
            menuRay.SetActive(enabled);
            Debug.Log("MenuRay ativo: " + enabled);
        }
        else
        {
            Debug.LogWarning("MenuRay não foi atribuído no GameManager.");
        }
    }
}