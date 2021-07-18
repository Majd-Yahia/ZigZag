using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCollision : MonoBehaviour
{
    private Procedural procedural;
    private GameManager gameManager;
    private bool collided = false;
    void Start()
    {
        procedural = FindObjectOfType<Procedural>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !collided)
        {
            collided = true;
            gameManager.CheckPoint = this.transform.position;
            procedural.LoopSpawn(10);
            procedural.BeginDestroyProcedure();
        }
    }
}
