using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 20;
    float MaxHP = 20;
    PhotonView view;
    Animator anim;
    bool isAlive = true;
    [SerializeField] Slider healthBar;
    [PunRPC]
    //Start is called before the first frame update
    void Start()
    {
        MaxHP = hp;
        healthBar.value = hp / MaxHP;
        view = GetComponent<PhotonView>();
        anim = GetComponentInChildren<Animator>();
    }
    public void GetDamage(float damage)
    {
        if (!isAlive) return;
        hp -= damage;
        view.RPC("DisplayHealth",RpcTarget.All,hp);
        if (hp <= Mathf.Epsilon)
        {
            StartCoroutine(DeathCoroutine());
        }
    }
    IEnumerator DeathCoroutine()
    {
        isAlive = false;
        int rand = Random.Range(0, 2);
        if ((rand == 0)) anim.SetTrigger("death1");
        else anim.SetTrigger("death2");
        GetComponent<EnemyAi>().enabled = false;
        view.RPC("DisableCollider",RpcTarget.All);
        yield return new WaitForSeconds(10);
        PhotonNetwork.Destroy(gameObject);
    }
    [PunRPC]
    void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
    [PunRPC]
    void DisplayHealth(float newHp)
    {
        healthBar.value = newHp / MaxHP ;
    }
}
