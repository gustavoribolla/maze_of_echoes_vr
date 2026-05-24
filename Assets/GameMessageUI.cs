using System.Collections;
using TMPro;
using UnityEngine;

public class GameMessageUI : MonoBehaviour
{
    public static GameMessageUI Instance;

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float defaultDuration = 2.5f;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        Instance = this;

        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    public void ShowMessage(string message)
    {
        ShowMessage(message, defaultDuration);
    }

    public void ShowMessage(string message, float duration)
    {
        if (messageText == null) return;

        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        messageText.text = message;

        yield return new WaitForSeconds(duration);

        messageText.text = "";
        messageCoroutine = null;
    }
}