using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CanvasAbove : MonoBehaviour
{
    PhotonView view;
    Transform character;
    Vector3 offset;
    void Awake()
    {
        character = transform.parent;
        offset = transform.position - character.position;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (character)
        {
        transform.position = character.position + offset;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
