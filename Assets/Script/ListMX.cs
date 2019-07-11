using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListMX : MonoBehaviour {

	public Color Hightcolor;
	public Color LowColor;
	public Color[] nubcolor=new Color[10];
	public mxMessage MyMX;
	public ItemMessage[] allback;
	void Awake()
	{

		allback =gameObject.GetComponentsInChildren<ItemMessage> ();

	}
	public void Set(string Nub,ItemMessage ITEM)
	{
		MyMX = new mxMessage (Hightcolor, LowColor, nubcolor, int.Parse(Nub));
		MyMX.SetMessage (ITEM);
	}

	public void UpdateList(string GeNub)
	{
        Debug.Log(GeNub);


		char[] aa={','};
		string[] ALLNUB = GeNub.Split(aa);
		for (int i = 0; i < ALLNUB.Length; i++) 
		{
            //Debug.Log(allback[i]);
			Set (ALLNUB [i], allback [i]);
		}
	}
		

	public int mc;
	public void SetMC(string GetMC)
	{
		mc = int.Parse (GetMC);
	}

	public void SetMH(string GetMH)
	{
		Set (GetMH, allback[mc-1]);
	}
		
}
