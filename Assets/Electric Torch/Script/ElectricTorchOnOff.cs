using UnityEngine;
using UnityEngine.InputSystem;

public class ElectricTorchOnOff : MonoBehaviour
{
    BatteryPowerPickup _batteryPower;

    public enum LightChoose
    {
        noBattery,
        withBattery
    }

    [Header("Mode")]
    public LightChoose modoLightChoose;

    [Header("VR Input")]
    public InputActionReference toggleLightAction;

    [Header("Battery Pickup Compatibility")]
    public bool _PowerPickUp = false;

    [Header("Light")]
    public float intensityLight = 2.5f;
    private bool _flashLightOn = false;

    [Header("Battery System")]
    public float maxBattery = 100f;
    public float currentBattery = 100f;
    public float batteryDrainPerSecond = 2f;

    [Header("Battery Visual Effect")]
    public bool dimLightWithBattery = true;
    public float minimumIntensityWhenLow = 0.25f;

    private Light _torchLight;
    private bool batteryEmptyMessageShown = false;

    private void Awake()
    {
        _torchLight = GetComponent<Light>();
        _batteryPower = FindFirstObjectByType<BatteryPowerPickup>();
    }

    private void Start()
    {
        if (toggleLightAction != null)
        {
            toggleLightAction.action.Enable();
        }

        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);
        UpdateLightState();
    }

    private void Update()
    {
        HandleInput();

        switch (modoLightChoose)
        {
            case LightChoose.noBattery:
                NoBatteryLight();
                break;

            case LightChoose.withBattery:
                WithBatteryLight();
                break;
        }
    }

    private void HandleInput()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleLight();
        }

        if (toggleLightAction != null && toggleLightAction.action.WasPressedThisFrame())
        {
            ToggleLight();
        }
    }

    private void ToggleLight()
    {
        if (modoLightChoose == LightChoose.withBattery && currentBattery <= 0f)
        {
            _flashLightOn = false;
            UpdateLightState();

            if (!batteryEmptyMessageShown)
            {
                batteryEmptyMessageShown = true;

                if (GameMessageUI.Instance != null)
                {
                    GameMessageUI.Instance.ShowMessage("Battery depleted. Find a battery.");
                }
            }

            return;
        }

        _flashLightOn = !_flashLightOn;
        UpdateLightState();
    }

    private void NoBatteryLight()
    {
        UpdateLightState();
    }

    private void WithBatteryLight()
    {
        if (_PowerPickUp && _batteryPower != null)
        {
            RechargeBattery(maxBattery);
            intensityLight = _batteryPower.PowerIntensityLight;
            _PowerPickUp = false;
        }

        if (_flashLightOn)
        {
            currentBattery -= batteryDrainPerSecond * Time.deltaTime;

            if (currentBattery <= 0f)
            {
                currentBattery = 0f;
                _flashLightOn = false;

                if (!batteryEmptyMessageShown)
                {
                    batteryEmptyMessageShown = true;

                    if (GameMessageUI.Instance != null)
                    {
                        GameMessageUI.Instance.ShowMessage("Battery depleted. Find a battery.");
                    }
                }
            }
        }

        UpdateLightState();
    }

    private void UpdateLightState()
    {
        if (_torchLight == null) return;

        bool canTurnOn = modoLightChoose == LightChoose.noBattery || currentBattery > 0f;

        if (_flashLightOn && canTurnOn)
        {
            if (modoLightChoose == LightChoose.withBattery && dimLightWithBattery)
            {
                float batteryPercent = GetBatteryPercent();
                float intensityMultiplier = Mathf.Lerp(minimumIntensityWhenLow, 1f, batteryPercent);
                _torchLight.intensity = intensityLight * intensityMultiplier;
            }
            else
            {
                _torchLight.intensity = intensityLight;
            }
        }
        else
        {
            _torchLight.intensity = 0f;
        }
    }

    public void RechargeBattery(float amount)
    {
        currentBattery += amount;
        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);

        batteryEmptyMessageShown = false;

        if (GameMessageUI.Instance != null)
        {
            GameMessageUI.Instance.ShowMessage("Lantern recharged.");
        }

        UpdateLightState();
    }

    public float GetBatteryPercent()
    {
        if (maxBattery <= 0f)
        {
            return 0f;
        }

        return currentBattery / maxBattery;
    }

    public bool IsLightOn()
    {
        return _flashLightOn;
    }
}