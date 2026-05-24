using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] private float rechargeAmount = 100f;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float pickupSoundVolume = 0.8f;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (!other.CompareTag("Player")) return;

        collected = true;

        ElectricTorchOnOff torch = FindFirstObjectByType<ElectricTorchOnOff>();

        if (torch != null)
        {
            torch.RechargeBattery(rechargeAmount);
        }

        if (GameMessageUI.Instance != null)
        {
            GameMessageUI.Instance.ShowMessage("Lantern recharged.");
        }

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupSoundVolume);
        }

        Destroy(gameObject);
    }
}