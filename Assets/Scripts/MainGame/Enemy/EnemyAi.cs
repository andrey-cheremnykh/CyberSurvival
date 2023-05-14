using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EnemyAi : MonoBehaviour
{
    CharacterMovement[] characters;
    NavMeshAgent ai;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        if (!PhotonNetwork.IsMasterClient) return;
    }
    IEnumerator ChoosePlayerToChase()
    {
        yield return new WaitForSeconds(1);
        characters = FindObjectsOfType<CharacterMovement>();
        if(characters.Length > 0) 
        {
            target = characters[0].transform; 
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (target == null) return;
        ai.SetDestination(target.position);
    }
}
