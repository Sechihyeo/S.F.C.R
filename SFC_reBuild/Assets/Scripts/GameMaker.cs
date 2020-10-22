using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaker : MonoBehaviour
{
     ///<summary>가장 가까운 오브젝트 찾기</summary> 
	public GameObject NearestObject(string tag) { 
        GameObject nearest = null; //가장 가까운 오브젝트 
        float nearestDistance=0; //그 거리 
        GameObject[] objectArray = GameObject.FindGameObjectsWithTag(tag); //대상 오브젝트 배열 얻기

        //만약 대상 오브젝트가 하나도 없으면 null로 리턴 
        if (objectArray.Length>0) {//있다면 
            nearest = objectArray[0]; //처음오브젝트를 가장 가까움으로 일단 설정 
            nearestDistance = (nearest.transform.position - transform.position).sqrMagnitude; //초기 가장 가까운 거리 설정 
            foreach(GameObject inst in objectArray) //가장 가까운 오브젝트 찾기
            {
                float instDistance = (inst.transform.position - transform.position).sqrMagnitude;
                if (instDistance<nearestDistance) //만약 더 가깝다면
                {
                    nearest = inst; //너로 정했다 
                    nearestDistance = (nearest.transform.position - transform.position).sqrMagnitude; //가장 가까운 거리 설정 
                }
            }
        }

        return nearest; //리턴
    }
    public float PointDirection(Vector2 pos1,Vector2 pos2) {
        Vector2 pos = pos2 - pos1;
        float angle = (float)Mathf.Atan2(pos.y,pos.x)*Mathf.Rad2Deg;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }

    public Vector3 VectorRotation(float _angle) {
        
        _angle-=90;
        _angle*=-1;
        if (_angle<0f)
            _angle+=360f;
	    return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), Mathf.Cos(_angle * Mathf.Deg2Rad),0);
    }
 
}
