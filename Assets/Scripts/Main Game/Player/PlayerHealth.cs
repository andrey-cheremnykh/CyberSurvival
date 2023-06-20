using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

public class PlayerHealth : MonoBehaviour
{
    float maxHp = 100;
    float hp;

    [SerializeField] Slider healthBar;
    PhotonView view;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody[] rigids;
    RigBuilder rig;
    [HideInInspector] public bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponentInChildren<RigBuilder>();
        hp = maxHp;
        healthBar.value = hp / maxHp;
        view = GetComponent<PhotonView>();
         SetupRigging(false);
    }
    void SetupRigging(bool isOn)
    {
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = !isOn;
            rigids[i].GetComponent<Collider>().enabled = isOn;
        }
    }

    [PunRPC]
    public void GetDamage(float damage)
    {
        if(isAlive)
        hp -= damage;
        view.RPC("DisplayHealth", RpcTarget.All, hp);
        if(hp <= Mathf.Epsilon)
        {
            isAlive = false;
            StartCoroutine(DestroyPlayerCorout());
        }

    }
    IEnumerator DestroyPlayerCorout()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.Destroy(gameObject);
    }
    void DisableCharacter()
    {
        SetupRigging(true);
        anim.enabled = false;
        GetComponent<CharacterMovement>().enabled = false;
        GetComponent<PlayerAim>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
    }

    [PunRPC]
    void DisplayHealth(float newHP)
    {
        healthBar.value =  newHP / maxHp;
        if (newHP < Mathf.Epsilon)
        {
            Destroy(healthBar.gameObject);
            DisableCharacter();
            isAlive = false; 
        }
    }  
}