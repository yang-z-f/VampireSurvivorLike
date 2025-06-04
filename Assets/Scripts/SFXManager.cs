using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource[] sound;
    private void Awake()
    {
        instance = this;
    }
    public void PlaySFX(int sfx)
    {
        sound[sfx].Stop();
        sound[sfx].Play();
    }
    public void Pitch(int sfx)
    {
        sound[sfx].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfx);
    }
}
