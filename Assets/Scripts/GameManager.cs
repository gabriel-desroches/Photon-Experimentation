using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager Instance;

    void Start()
    {
        Instance = this;
    }

    //Need to return to launcher
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            print("Tried loading a level but was not master client");
        }
        print("Loading level : 0 " + PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("On player entered room()" + newPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("Player has left room " + otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }

}
