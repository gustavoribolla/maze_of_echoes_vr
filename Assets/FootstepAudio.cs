using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepAudio : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip footstepClip;

    [Header("Configuração dos passos")]
    [SerializeField] private float stepInterval = 0.9f;
    [SerializeField] private float movementThreshold = 0.02f;
    [SerializeField] private float volume = 0.15f;

    [Header("Teste sem VR")]
    [SerializeField] private bool allowKeyboardTest = true;

    private Vector3 lastPosition;
    private float stepTimer;
    private bool wasMoving;

    private void Start()
    {
        lastPosition = transform.position;

        if (footstepAudioSource != null)
        {
            footstepAudioSource.clip = footstepClip;
            footstepAudioSource.loop = false;
            footstepAudioSource.playOnAwake = false;
            footstepAudioSource.volume = volume;
        }
    }

    private void Update()
    {
        bool isMoving = IsPlayerMoving();

        if (allowKeyboardTest && Keyboard.current != null)
        {
            isMoving =
                Keyboard.current.wKey.isPressed ||
                Keyboard.current.aKey.isPressed ||
                Keyboard.current.sKey.isPressed ||
                Keyboard.current.dKey.isPressed;
        }

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f && !footstepAudioSource.isPlaying)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;

            if (wasMoving && footstepAudioSource != null)
            {
                footstepAudioSource.Stop();
            }
        }

        wasMoving = isMoving;
        lastPosition = transform.position;
    }

    private bool IsPlayerMoving()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        return distanceMoved > movementThreshold;
    }

    private void PlayFootstep()
    {
        if (footstepAudioSource == null || footstepClip == null) return;

        footstepAudioSource.clip = footstepClip;
        footstepAudioSource.volume = volume;
        footstepAudioSource.Play();
    }
}