using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    PhotonView view;    
    float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!view.IsMine) return;
        if (other.GetComponent<EnemyHealth>())
        {
            PhotonView enView = other.GetComponent<PhotonView>();
            enView.RPC("GetDamage", RpcTarget.MasterClient, damage);
        }
        PhotonNetwork.Destroy(view); 
    }
}
