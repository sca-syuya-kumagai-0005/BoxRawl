using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip parySE;
    public AudioClip jumpSE;
    public AudioClip drop1SE;
    public AudioClip dorp2SE;
    public AudioClip damageSE;
    public AudioClip deadSE;
    public static bool pary;
    public static bool jump;
    public static bool drop1;
    public static bool drop2;
    public static bool damage;
    public static bool dead;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pary = false;
        jump = false;
        drop1 = false;
        drop2 = false;
        damage = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pary)
        {
            audioSource.PlayOneShot(parySE);
            pary = false;
        }
        if (jump)
        {
            audioSource.PlayOneShot(jumpSE);
            jump = false;
        }
        if (drop1)
        {
            audioSource.PlayOneShot(drop1SE);
            drop1 = false;
        }
        if (drop2)
        {
            audioSource.PlayOneShot(parySE);
            drop2 = false;
        }
        if (damage)
        {
            audioSource.PlayOneShot(damageSE);
            damage = false;
        }
        if (dead)
        {
            audioSource.PlayOneShot(deadSE);
            dead = false;
        }
    }
}
