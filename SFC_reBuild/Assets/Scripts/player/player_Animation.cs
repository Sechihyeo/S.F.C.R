using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Animation : MonoBehaviour
{

    Animator animator;
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.isMine)return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");        
        animator.SetBool("is_Run",(Mathf.Abs(h)>0||Mathf.Abs(v)>0));
        
    }
}
