using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
        StartCoroutine(DestroyAfterTime(source.clip.length));
    }

    IEnumerator DestroyAfterTime(float time) {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(this.gameObject);
    }
}
