using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectMenu;

    [SerializeField] private InputField UserNameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartBTN;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    private void Start()
    {
        UsernameMenu.SetActive(true);
    }
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("connected");
    }

    public void ChangeUsernameInput()
    {
        if (UserNameInput.text.Length > 0)
        {
            StartBTN.SetActive(true);
        }
        else
        {
            StartBTN.SetActive(false);

        }
    }
    public void SetUsername()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UserNameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void JoinGame() 
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;

        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
