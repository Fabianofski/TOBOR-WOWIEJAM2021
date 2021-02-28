using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{

    [SerializeField] GameObject[] Sounds;

    public void PlayRandom()
    {
        GameObject sound = Instantiate(Sounds[Random.Range(0, Sounds.Length)], transform.position, Quaternion.identity, transform);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);
    }

}
