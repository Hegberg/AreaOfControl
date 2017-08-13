using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//smooths opposing characters movement plus rotation, not really needed 
//but have in place in case things change and for practice
public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine)
        {
            //not our character so update movement, this smooths the movement
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            //this is our player, need to send position to network
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //this is some other player on network, needs to recieve this players position
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
