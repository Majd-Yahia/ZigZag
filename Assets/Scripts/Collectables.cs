using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectables : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private int value;
    [SerializeField] private AudioClip m_crystal_1;
    [SerializeField] private AudioClip m_crystal_2;
    [SerializeField] private ParticleSystem m_particle;
    private GameManager gameManager;
    private AudioSource audioSource;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ParticleSystem particles = Instantiate(m_particle, this.transform.position, Quaternion.identity);       // Spawn particles.
            Destroy(particles, 0.5f);

            PlayRandomSound();                                                      // Play a sound on pickup.
            gameManager.IncreaseCrystal(value);                                     // Play animatin!
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;           // Remove the mesh so its invisible when picked up.
            Destroy(this.gameObject, 1f);                                           // Invoke destroy function.
        }
    }

    private void PlayRandomSound()
    {
        // Play a sound Randomly.
        float rand = Random.Range(0, 1);
        switch (rand)
        {
            case 0:
                audioSource.PlayOneShot(m_crystal_1);
                break;
            case 1:
                audioSource.PlayOneShot(m_crystal_2);
                break;
        }
    }
}
