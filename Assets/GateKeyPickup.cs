using UnityEngine;

public class GateKeyPickup : MonoBehaviour
{
    [SerializeField] private FinalGateController gateController;
    [SerializeField] private AudioSource pickupAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (pickupAudio != null)
        {
            pickupAudio.Play();
        }

        if (gateController != null)
        {
            gateController.OpenGate();
        }

        Destroy(gameObject);
    }
}