using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public GameObject mainNonPlayerCamera;

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
        //spawn player and set mainCamera to be disabled so it does not interfer with the player camera, also disables it's audio listener
        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("RedTile", new Vector3(0,0,-2), Quaternion.identity, 0);
        mainNonPlayerCamera.SetActive(false);

        //keeps other players that are spawned in from having their controlls and camera enabled
        //player object requires move script and camera to be disabled for this to work
        myPlayer.GetComponent<Camera>().enabled = true;
        myPlayer.GetComponent<SimpleMove>().enabled = true;
    }
}
