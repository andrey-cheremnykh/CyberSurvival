using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Vector3 origin = transform.GetChild(0).position;
        GameObject newBullet =
        PhotonNetwork.Instantiate(bulletPrefab.name, origin, transform.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward * 8;
    }
}
