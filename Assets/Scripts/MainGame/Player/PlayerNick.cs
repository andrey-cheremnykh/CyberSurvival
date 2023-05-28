using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNick : MonoBehaviour
{
    [SerializeField] PhotonView playerView;
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text myText = GetComponent<TMP_Text>();
        myText.text = playerView.Owner.NickName;
        if(playerView.IsMine == false)
        {
            myText.color = Color.cyan;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
