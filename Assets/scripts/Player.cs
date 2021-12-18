using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Photon.MonoBehaviour
{
    public PhotonView photonView;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public Transform feet;
    public LayerMask groundLayers;

    public float MoveSpeed;
    public float JumpForce;

    public LayerMask tresureLayer;
    public GameObject winUI;
    public GameObject loseUI;



    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.cyan;
        }

       // winUI = GameObject.FindGameObjectWithTag("winnerCanvas"); 
      //  loseUI = GameObject.FindGameObjectWithTag("loserCanvas");
       // winUI.SetActive(false);
       // loseUI.SetActive(false);

    }

    private void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();
        }

        isWining();

    }

    private void CheckInput()
    {

        //move left & right
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;


        //flip player left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }
        //flip player right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }


        //run animation on / off
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }


        //jump 
        if (Input.GetButtonDown("Jump") && IsGround())
        {
            Vector2 movement = new Vector2(rb.velocity.x, JumpForce);
            rb.velocity = movement;
            anim.SetTrigger("isJumping");
        }
        else
        {
            anim.ResetTrigger("isJumping");
        }

    }

    private bool IsGround()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position , 0.5f , groundLayers);
        if(groundCheck != null)
        {
            return true;
        }

        return false;
    }

    private void isWining()
    {
        Collider2D winCheck = Physics2D.OverlapCircle(transform.position, 0.5f, tresureLayer);
        if (winCheck != null)
        {
            if (photonView.isMine)
            {
                SceneManager.LoadScene("winScene");
                photonView.RPC("lose", PhotonTargets.Others);
            }
            
           // Debug.Log("hi");
        }

    }

    [PunRPC]
    public void lose()
    {
        Debug.Log("hhhhh");
        //loseUI.SetActive(true);
        SceneManager.LoadScene("loseScene");

    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }
}
