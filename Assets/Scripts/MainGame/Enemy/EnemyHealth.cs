using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 20;
    // Start is called before the first frame update
    void Start()
    {
    }
    [PunRPC]
    public void GetDamage(float damage)
    {
        hp -= damage;
        if (hp <= Mathf.Epsilon)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
