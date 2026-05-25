using UnityEngine;
using UnityEngine.InputSystem;

public class DebugGameShortcuts : MonoBehaviour
{
    [SerializeField] private bool enableKeyboardDebug = true;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;

    private void Update()
    {
        if (!enableKeyboardDebug) return;
        if (Keyboard.current == null || gameManager == null) return;

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (startMenu != null && startMenu.activeSelf)
            {
                gameManager.StartGame();
            }
            else if (endMenu != null && endMenu.activeSelf)
            {
                gameManager.RestartGame();
            }
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            gameManager.EndGame();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            gameManager.RestartGame();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            gameManager.QuitGame();
        }
    }
}