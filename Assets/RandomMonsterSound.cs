using System.Collections;
using UnityEngine;

public class RandomMonsterSound : MonoBehaviour
{
    [SerializeField] private AudioSource monsterAudioSource;

    [Header("Sons do monstro")]
    [SerializeField] private AudioClip[] monsterGroanClips;

    [Header("Tempo entre sons")]
    [SerializeField] private float minTimeBetweenSounds = 25f;
    [SerializeField] private float maxTimeBetweenSounds = 60f;

    [Header("Volume")]
    [SerializeField] private float volume = 0.5f;

    private void Start()
    {
        StartCoroutine(PlayMonsterSounds());
    }

    private IEnumerator PlayMonsterSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
            yield return new WaitForSeconds(waitTime);

            PlayRandomMonsterSound();
        }
    }

    private void PlayRandomMonsterSound()
    {
        if (monsterAudioSource == null) return;
        if (monsterGroanClips == null || monsterGroanClips.Length == 0) return;

        int randomIndex = Random.Range(0, monsterGroanClips.Length);
        AudioClip selectedClip = monsterGroanClips[randomIndex];

        if (selectedClip != null)
        {
            monsterAudioSource.PlayOneShot(selectedClip, volume);
        }
    }
}