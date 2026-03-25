using UnityEngine;

public class StepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stepClip;

    // This triggers the moment the physics collider touches any object
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(stepClip);
    }
}


