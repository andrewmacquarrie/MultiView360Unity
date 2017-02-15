using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSwitch : MonoBehaviour {

    public PopMovieSwitch popMovieSwitch;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            popMovieSwitch.SwitchToSphere(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            popMovieSwitch.SwitchToSphere(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            popMovieSwitch.SwitchToSphere(2);
        }
    }
}
