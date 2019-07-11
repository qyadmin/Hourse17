using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setmc : MonoBehaviour {


	public ListMX sss;
	public void SetMH(string GetMH)
	{
		sss.SetMH (GetMH);
		Get_showqishi.text = GET_qishi.text;
	}

	public void UpdateList(string GeNub)
	{
		sss.UpdateList (GeNub);
	}

	public Text GET_qishi;
	public Text Get_showqishi;
}
