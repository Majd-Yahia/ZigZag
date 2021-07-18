using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform rayStart;
    private bool walkingRight = false;
    private GameManager gameManager;
    private Animator anime;
    private RaycastHit hit;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        MoveCharacterWithAnimation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchDirection();
        }

        IsGrounded();
    }

    private void SwitchDirection()
    {
        if (!gameManager.GameStarted) { return; }

        walkingRight = !walkingRight;
        if (walkingRight)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void IsGrounded()
    {
        if (!gameManager.GameStarted) { return; }
        //Debug.DrawRay(rayStart.position, -transform.up, Color.red, 0.0f);
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            anime.SetBool("isFalling", true);
        }
        else
        {
            anime.SetBool("isFalling", false);
        }

        if (transform.position.y < -2) { gameManager.EndGame(); }
    }

    private void MoveCharacterWithAnimation()
    {
        if (!gameManager.GameStarted) { anime.SetBool("isRunning", false); return; }

        rigid.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
        anime.SetBool("isRunning", true);
    }

    public void SetIdleAnim()
    {
        anime.SetBool("isRunning", false);
        anime.SetBool("isFalling", false);
    }

    public void SetIsWalkingRight(bool value) { walkingRight = value; }
}
