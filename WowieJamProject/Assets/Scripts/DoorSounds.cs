using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSounds : MonoBehaviour
{

    [SerializeField] GameObject DoorRingSound;
    [SerializeField] GameObject DoorSound;
    [SerializeField] bool RandomPitch;

    public void PlayDoorRingSound()
    {
        GameObject sound = Instantiate(DoorRingSound, transform.position, Quaternion.identity, transform);
        if(RandomPitch)
            sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.3f);
        Destroy(sound, 1f);
    }

    public void PlayDoorSound()
    {
        GameObject sound = Instantiate(DoorSound, transform.position, Quaternion.identity, transform);
        if (RandomPitch)
            sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 2f);
        Destroy(sound, 1f);
    }
}
