using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class EnemyAi : MonoBehaviour
{
    PlayerHealth[] characters;
    NavMeshAgent ai;
    Transform target;
    [SerializeField] float attackDist = 1.5f;
    bool isAttack; 
    Animator anim;
    float rotateSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ai = GetComponent<NavMeshAgent>();
        ai.stoppingDistance = attackDist;
        if (!PhotonNetwork.IsMasterClient) return;
        StartCoroutine(ChoosePlayerToChase()); 
    }
    IEnumerator ChoosePlayerToChase()
    {
        yield return new WaitForSeconds(1);
        characters = FindObjectsOfType<PlayerHealth>();
        if(characters.Length > 0) 
        {
            target = GetClosestPlayer();
        }
        StartCoroutine(ChoosePlayerToChase());
    }
    Transform GetClosestPlayer()
    {
        Transform closePlayer = characters[0].transform;
        float minDist = Vector3.Distance(closePlayer.position, transform.position);
        for (int i = 0; i < characters.Length; i++)
        {
            float dist = Vector3.Distance(characters[i].transform.position, transform.position);
            if(dist < minDist)
            {
                closePlayer = characters[i].transform;
                minDist = dist;
            }
        }
            return closePlayer;

    }
    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (target == null) return;
        if (!isAttack) return;
        ai.SetDestination(target.position);
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= attackDist)
        {
            StartCoroutine(AttackCorout());
        }
        AnimHandle();
    }
    private void OnDisable()
    {
        ai.SetDestination(transform.position);
    }
    void AnimHandle()
    {
        if(ai.desiredVelocity.magnitude < 0.1f)
        {
            anim.SetBool("walk", false);

        }
        else
        {
            anim.SetBool("walk", true);

        }
    }
    IEnumerator AttackCorout()
    {
        StartCoroutine(RotateToPlayerCoroutine());
         isAttack = true;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(13f/30f);
        float dist = Vector3.Distance(target.position, transform.position);
        if(dist <= attackDist)
        {
            PhotonView playerView = target.GetComponent<PhotonView>();
            playerView.RPC("GetDamage", playerView.Owner, 20f);
        }
        yield return new WaitForSeconds(19f/30f);
        isAttack = false;
    }
    IEnumerator RotateToPlayerCoroutine()
    {
        float timer = 0;
        while (timer <= 1)
        {
            timer += Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            lookRot = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
