using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAim : MonoBehaviour,IPunObservable
{
    PhotonView view;
    Camera cam;
    Vector3 aimPoint;
    [SerializeField] float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        view = GetComponent<PhotonView>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Aim();
        }
        else
        {
                
        }
        RotateToAim();
    }
    void Aim()
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        float dist = 100;
        LayerMask aimLayer = LayerMask.GetMask("Aim");
        RaycastHit hitInfo;
        Physics.Raycast(r, out hitInfo, dist, aimLayer);
        if (hitInfo.transform)
        {
            aimPoint = hitInfo.point;
        }
    }
    void RotateToAim()
    {
        Vector3 dir = aimPoint - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            lookRot, rotateSpeed * Time.deltaTime); ;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(aimPoint);
        }
        else
        {
            aimPoint = (Vector3)stream.ReceiveNext();
        }
    }
}
