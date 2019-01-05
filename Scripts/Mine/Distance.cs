using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour {

    public GameObject _gameObject;
		
	void Update()
    {  
       print(Vector3.Distance(_gameObject.transform.position, this.transform.position));
	}
}
