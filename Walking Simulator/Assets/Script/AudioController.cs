using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip to play

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true; // Set loop to true to make it loop
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No audio clip assigned to the AudioController on " + gameObject.name);
        }
    }
}
