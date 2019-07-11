using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class control_field : MonoBehaviour
{
    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;
    public InputField inputField4;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnEnable()
    {
        Debug.Log("sss");
        inputField1.text = Regex.Replace(inputField1.text, "[^0-9]", "");
        inputField1.textComponent.text = Regex.Replace(inputField1.text, "[^0-9]", "");

    }

}
