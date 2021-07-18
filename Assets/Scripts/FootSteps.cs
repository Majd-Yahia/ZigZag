using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    [SerializeField] private AudioClip[] m_steps;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Step()
    {
        audioSource.PlayOneShot(GetRandomClip());
    }

    private AudioClip GetRandomClip()
    {
        // Adding more Clips here!
        // since i dont have but only one clip, i will only return it, but if you have more you can.
        // float random = Random.Range(0, m_steps.Length);
        // return m_steps[random]
        return m_steps[0];
    }
}
