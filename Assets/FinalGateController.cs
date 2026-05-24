using UnityEngine;

public class FinalGateController : MonoBehaviour
{
    [SerializeField] private GameObject finalGate;
    [SerializeField] private AudioSource openSound;

    public void OpenGate()
    {
        if (openSound != null)
        {
            openSound.Play();
        }

        if (finalGate != null)
        {
            finalGate.SetActive(false);
        }
    }
}