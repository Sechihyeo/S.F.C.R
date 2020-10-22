using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focus_Gun : MonoBehaviour
{
    Vector3 mPosition;
    Vector3 oPosition;
    Vector3 target;
    public float rotateDegree;
    Vector3 left, right;
    player_Move p_Player;
    [HideInInspector]
    public float toDegree;
    void Start()
    {
        p_Player = GetComponentInParent<player_Move>();
        p_Player.gunfocus=this;
        left = p_Player.transform.localScale - new Vector3(p_Player.transform.localScale.x * 2, 0, 0);
        right = p_Player.transform.localScale;
    }
    void Update()
    {
        mouseTarget();
    }
    void FixedUpdate()
    {

    }
    ///<summary>객체를 마우스 방향으로 회전시키는 함수.</summary>
    void mouseTarget()
    {
        if (p_Player.pv.isMine)
        {
            mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            oPosition = p_Player.transform.localPosition;
            target = mPosition - oPosition;
            rotateDegree = -1 * Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg + 90;
        }
        else
        {
            rotateDegree += (toDegree-rotateDegree)/10;
        }
            if (Mathf.Abs(rotateDegree) > 90)
            {
                p_Player.transform.localScale = left;
                transform.localScale = new Vector3(-1, -1);
            }
            else
            {
                p_Player.transform.localScale = right;
                transform.localScale = new Vector3(1, 1);
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }
}
