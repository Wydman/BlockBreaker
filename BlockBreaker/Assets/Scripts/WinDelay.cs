using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinDelay : MonoBehaviour {

	
	void Start () {
        StartCoroutine(Delay());
	}
	

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Level_Inf");
    }

}
