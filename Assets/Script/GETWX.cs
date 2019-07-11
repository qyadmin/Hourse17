using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GETWX : MonoBehaviour {

	public Text[] AllText;
	public void ShowWeiXin(string Obj)
	{

		string[] aaa =Obj.Split(new char[1]{','});
		for (int i = 0; i < AllText.Length; i++) 
		{
			if (i > aaa.Length - 1)
				continue;
			AllText [i].text = aaa [i];
		}

	}

}
