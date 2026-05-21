using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.EndGame();
        }
    }
}