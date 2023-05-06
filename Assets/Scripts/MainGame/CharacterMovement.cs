using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 6;
    Rigidbody rb;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) 
        {
            float inputX = Input.GetAxis("Horizontal") * maxSpeed;
            float inputZ = Input.GetAxis("Vertical") * maxSpeed;
            rb.velocity = new Vector3(inputX, 0, inputZ);
        }
        
    }
}
