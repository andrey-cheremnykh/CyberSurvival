using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] Button createButton;
    [SerializeField] Button joinRandomButton;
    [SerializeField] TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        createButton.interactable = false;
        joinRandomButton.interactable = false;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();


    }

    public override void OnConnectedToMaster()
    {
        print("Connect to master server");
        createButton.interactable = true;
        joinRandomButton.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Some Errors in Connect");
        print("Restart the game");
        print(cause);
    }

    void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;

        PhotonNetwork.NickName = GetName();
        PhotonNetwork.CreateRoom(null, options);
    }

    void JoinRandom()
    {
        PhotonNetwork.NickName = GetName();
        PhotonNetwork.JoinRandomRoom();
    }


    string GetName()
    {
        string nick = inputField.text;
        if (string.IsNullOrWhiteSpace(nick))
        {
            nick = "Player " + Random.Range(1, 9999);
        }
        return nick;
    }
    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.NickName + "Entered the room");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Failed on room enter: " + returnCode);
        print(message);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Failed on room enter: " + returnCode);
        print(message);
    }

}
