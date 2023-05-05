using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Test : MonoBehaviour,IPunObservable
{
    PhotonView view;
    MeshRenderer rend;
    bool isRed = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isRed);
        }  
        else
        {
            isRed = (bool) stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        rend = GetComponent<MeshRenderer>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (isRed) rend.material.color = Color.red;
        else rend.material.color = Color.blue;
        if (!view.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRed = !isRed;
        }
    }
}
