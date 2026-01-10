using System.Collections;
using UnityEngine;

public class Audio : MonoBehaviour {
    public AudioSource audioSource;
    public Coroutine doPlay;

    public void Play() {
        audioSource.Play();
        doPlay = StartCoroutine(DoPlay());
    }

    private IEnumerator DoPlay() {
        var wait = new WaitForSeconds(audioSource.clip.length);
        yield return wait;
        Destroy(gameObject);
    }
}
