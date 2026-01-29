using System.Collections;
using UnityEngine;

public class Audio : MonoBehaviour {
    public AudioSource audioSource;
    public Coroutine doPlay;
    public ObjectPool objectPool;

    public void Initialize(ObjectPool op) {
        objectPool = op;
    }

    public void Play() {
        audioSource.Play();
        doPlay = StartCoroutine(DoPlay());
    }

    private IEnumerator DoPlay() {
        var wait = new WaitForSeconds(audioSource.clip.length);
        yield return wait;

        if (objectPool) {
            objectPool.Return(gameObject);
        } else {
            Debug.LogError("havent object pool");
        }

        Clear();
    }

    private void Clear() {
        objectPool = null;
        audioSource.clip = null;
    }
}
