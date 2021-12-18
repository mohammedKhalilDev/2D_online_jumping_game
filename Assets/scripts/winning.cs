using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winning : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject player;
    public GameObject winCanv;
    public GameObject loseCanv;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Win()
    {
        Collider2D winCheck = Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer);
        if (winCheck != null)
        {       
            Debug.Log("hiiiiii");

            Animation anim = GetComponent<Animation>();
            anim.Play();
        }

    }
  
    public void theWin()
    {
        winCanv.SetActive(true);
    }
    public void theLose()
    {
        loseCanv.SetActive(true);
    }
}
