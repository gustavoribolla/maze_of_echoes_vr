using UnityEngine;
using UnityEngine.InputSystem;

public class ElectricTorchOnOff : MonoBehaviour
{
    EmissionMaterialGlassTorchFadeOut _emissionMaterialFade;
    BatteryPowerPickup _batteryPower;

    public enum LightChoose
    {
        noBattery,
        withBattery
    }

    public LightChoose modoLightChoose;

    [Header("VR Input")]
    public InputActionReference toggleLightAction;

    [Header("Battery")]
    public bool _PowerPickUp = false;

    [Header("Light")]
    public float intensityLight = 2.5F;
    private bool _flashLightOn = false;

    [SerializeField] float _lightTime = 0.05f;

    private void Awake()
    {
        _batteryPower = FindFirstObjectByType<BatteryPowerPickup>();
    }

    void Start()
    {
        GameObject _scriptControllerEmissionFade = GameObject.Find("default");

        if (_scriptControllerEmissionFade != null)
        {
            _emissionMaterialFade = _scriptControllerEmissionFade.GetComponent<EmissionMaterialGlassTorchFadeOut>();
        }

        if (_scriptControllerEmissionFade == null)
        {
            Debug.Log("Cannot find 'EmissionMaterialGlassTorchFadeOut' script");
        }

        if (toggleLightAction != null)
        {
            toggleLightAction.action.Enable();
        }
    }

    void Update()
    {
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

    void InputKey()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            _flashLightOn = !_flashLightOn;
        }

        if (toggleLightAction != null && toggleLightAction.action.WasPressedThisFrame())
        {
            _flashLightOn = !_flashLightOn;
        }
    }

    void NoBatteryLight()
    {
        if (_flashLightOn)
        {
            GetComponent<Light>().intensity = intensityLight;

            if (_emissionMaterialFade != null)
            {
                _emissionMaterialFade.OnEmission();
            }
        }
        else
        {
            GetComponent<Light>().intensity = 0.0f;

            if (_emissionMaterialFade != null)
            {
                _emissionMaterialFade.OffEmission();
            }
        }

        InputKey();
    }

    void WithBatteryLight()
    {
        if (_flashLightOn)
        {
            GetComponent<Light>().intensity = intensityLight;
            intensityLight -= Time.deltaTime * _lightTime;

            if (_emissionMaterialFade != null)
            {
                _emissionMaterialFade.TimeEmission(_lightTime);
            }

            if (intensityLight < 0)
            {
                intensityLight = 0;
            }

            if (_PowerPickUp == true && _batteryPower != null)
            {
                intensityLight = _batteryPower.PowerIntensityLight;
            }
        }
        else
        {
            GetComponent<Light>().intensity = 0.0f;

            if (_emissionMaterialFade != null)
            {
                _emissionMaterialFade.OffEmission();
            }

            if (_PowerPickUp == true && _batteryPower != null)
            {
                intensityLight = _batteryPower.PowerIntensityLight;
            }
        }

        InputKey();
    }
}