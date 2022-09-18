using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource AS;
    // Start is called before the first frame update
    public void PlayOneShot(AudioClip AudioToPlay)
    {
        AS.PlayOneShot(AudioToPlay);
    }

}
