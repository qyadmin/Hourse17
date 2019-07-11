using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetAction : MonoBehaviour {

	[SerializeField]
	private Text ShowMh,DX;
	public void CheckType(string GetType)
	{
		if (GetType == "1") 
		{
			DX.text = string.Empty;
		}
		if (GetType == "2") 
		{
			ShowMh.text = string.Empty;
		}
	}

	public string SaveID;
	public void Save_Chip_id(string Getid)
	{
		SaveID = Getid;
	}

	public Text Chip_id;
	public void SetChipid()
	{
		Chip_id.text = SaveID;
	}
    [SerializeField]
    Button ClickButton;

    public void chip_show_time(string obj)
    {
        if (int.Parse(obj) <= int.Parse(Static.Instance.GetValue("system_time")))
        {
            ClickButton.interactable = true;
        }
        else
            ClickButton.interactable = false;
    }

}
