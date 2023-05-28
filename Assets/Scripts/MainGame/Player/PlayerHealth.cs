using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    float hp = 100;
    PhotonView view;
    [SerializeField] Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    [PunRPC]
    public void GetDamage(float damage)
    {
        hp -= damage;
        view.RPC("DisplayHealth", RpcTarget.All, hp);
        if(hp <= Mathf.Epsilon)
        {
            
        }
    }
    [PunRPC]
    void DisplayHealth(float newHp)
    {
        healthBar.value = newHp / 100;
    }
}
