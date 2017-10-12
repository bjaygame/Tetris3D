using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour 
{
    private TileControl tileControl;

	void Start()
	{
        tileControl = GetComponentInParent<TileControl>();
        
	}
	

	void Update() 
	{
		
	}

    private void OnTriggerEnter(Collider other) {
            
        
    }


}
