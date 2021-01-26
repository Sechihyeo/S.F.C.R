using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOrbit : MonoBehaviour
{
    public float destroy_time=2f;
	public float sizeReduction =0.01f,targetFigure=0f;
    public bool colored=true;
	public float orix=0.65f;

    SpriteRenderer mySprite;
    [SerializeField]public Color myColor,oriColor;
    Color ToColor;
    float speed =17.5f;
    // Start is called before the first frame update
    void Start()
    {
        //orix=transform.localScale.x;
        mySprite=GetComponent<SpriteRenderer>();
        ToColor=myColor;
        speed = (float)Application.targetFrameRate/8.22f;
    }
    public void Init(bool isColoerd,Vector3 position,Vector3 Scale)
    {
        transform.localScale=new Vector3(orix,orix,1); 
        colored=isColoerd;
        mySprite=GetComponent<SpriteRenderer>();
        mySprite.color=oriColor;
        transform.position=position;
        transform.localScale=Scale;
        sizeReduction =0.015f/((float)Application.targetFrameRate/144);
    }
    public void Relese()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(colored)
        mySprite.color = Color.Lerp(mySprite.color,myColor,Time.deltaTime*speed);
        transform.localScale -= new Vector3(sizeReduction,sizeReduction, 0);
		if(transform.localScale.x<=targetFigure)
		{
		    PoolingManager.Instance.ObjectRelease(this.gameObject);
		}
    }
}
