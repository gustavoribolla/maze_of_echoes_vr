using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GateLever : MonoBehaviour
{
    [SerializeField] private bool enableKeyboardDebug = true;
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private FinalGateController gateController;

    [Header("Input")]
    [SerializeField] private Key keyboardInteractKey = Key.G;
    [SerializeField] private InputActionReference vrInteractAction;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 1.5f;

    [Header("Lever Visual")]
    [SerializeField] private Transform leverHandlePivot;
    [SerializeField] private Vector3 activatedLocalRotation = new Vector3(0f, 0f, -45f);
    [SerializeField] private float animationDuration = 0.35f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool activated = false;
    private bool animating = false;

    private void Start()
    {
        if (leverHandlePivot != null)
        {
            initialRotation = leverHandlePivot.localRotation;
            targetRotation = Quaternion.Euler(activatedLocalRotation);
        }

        if (vrInteractAction != null)
        {
            vrInteractAction.action.Enable();
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (activated || animating) return;
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > interactionDistance) return;

        bool keyboardPressed = false;
        bool vrPressed = false;

        if (enableKeyboardDebug && Keyboard.current != null)
        {
            keyboardPressed = Keyboard.current[keyboardInteractKey].wasPressedThisFrame;
        }

        if (vrInteractAction != null)
        {
            vrPressed = vrInteractAction.action.WasPressedThisFrame();
        }

        if (keyboardPressed || vrPressed)
        {
            ActivateLever();
        }
    }

    public void ActivateLever()
    {
        if (activated || animating) return;

        activated = true;
        StartCoroutine(AnimateLever());

        if (gateController != null)
        {
            gateController.OpenGate();
        }

        if (GameMessageUI.Instance != null)
        {
            GameMessageUI.Instance.ShowMessage("Gate opened.");
        }
    }

    private IEnumerator AnimateLever()
    {
        animating = true;

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float t = timer / animationDuration;

            if (leverHandlePivot != null)
            {
                leverHandlePivot.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            }

            yield return null;
        }

        if (leverHandlePivot != null)
        {
            leverHandlePivot.localRotation = targetRotation;
        }

        animating = false;
    }
}