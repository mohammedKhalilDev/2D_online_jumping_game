using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject GameCanvase;
    public GameObject SceneCamera;


    public Text PingText;
    public GameObject DisconnectUI;
    public GameObject sureCanv;
    private bool off = false;

    private void Awake()
    {
        GameCanvase.SetActive(true);
    }

    private void Update()
    {
        CheckInput();
        PingText.text = "Ping : " + PhotonNetwork.GetPing();
    }

    private void CheckInput()
    {
        if(off && Input.GetKeyDown(KeyCode.Escape))
        {
            DisconnectUI.SetActive(false);
            off = false;
        }
        else if (!off && Input.GetKeyDown(KeyCode.Escape))
        {
            DisconnectUI.SetActive(true);
            off = true;
        }
    }
    public void SpawnPlayer()
    {
        float rand = Random.Range(-5f, 5f);

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * rand, this.transform.position.y), Quaternion.identity, 0);
        GameCanvase.SetActive(false);
        SceneCamera.SetActive(false);
    }

    public void LeaveRoom()
    {
        sureCanv.SetActive(true);
       
    }

    public void yesLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }
    public void noLeaveRoom()
    {
        sureCanv.SetActive(false);
    }
    public void resumeGame()
    {
        DisconnectUI.SetActive(false);
    }


}
