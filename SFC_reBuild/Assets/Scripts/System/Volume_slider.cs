using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Volume_slider : MonoBehaviour
{
    [SerializeField]
    string pref_name;
    Slider mySlider;
    float myValue;
    // Start is called before the first frame update
    void Start()
    {
        mySlider = gameObject.GetComponent<Slider>();
        mySlider.value = PlayerPrefs.GetFloat(pref_name);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat(pref_name,mySlider.value);
    }
}
