using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Connect();
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0.0");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Random Failed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnPhotonJoinFailed()
    {
        Debug.Log("Failed");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        PhotonNetwork.Instantiate("RedTile", new Vector3(0,0,0), Quaternion.identity, 0);
    }
}
