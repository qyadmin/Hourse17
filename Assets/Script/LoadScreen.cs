using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScreen : MonoBehaviour {
    [SerializeField]
    LaodTiao load;

	public void LoadMainScreen()
	{
        load.IsGetIn = true;
	}

	public void LoadmEUNScreen()
	{
		SceneManager.LoadScene ("MeunScreen");
	}


}
