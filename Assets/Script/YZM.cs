using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YZM : MonoBehaviour {

	private Texture2D TextureMap;
	public RawImage show;

	public InputField YZMSAVE;

	public Color[] vColor;
	void Start()
	{
		CreatYZM ();
	}
	// Use this for initialization
	public void CreatYZM()
	{
		Text[] aa = GetComponentsInChildren<Text> ();
		string NUB = string.Empty;
		foreach (Text child in aa) {
			child.text = Random.Range (0, 9).ToString();
			child.fontSize = Random.Range (20,30);
			child.rectTransform.position += new Vector3 (0,Random.Range(-3,3),0);
			child.rectTransform.eulerAngles = new Vector3 (0, 0, 0);
			child.rectTransform.eulerAngles += new Vector3 (0, 0, Random.Range (-20, 20));
			//child.color = new Color (Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.6f,0.9f));
			child.color = vColor[Random.Range(0,vColor.Length-1)];
			NUB += child.text;
		}
		YZMSAVE.text = NUB;
		TextureMap = new Texture2D(40, 20);  
		for(int i=0;i<60;i++)
			TextureMap.SetPixel (Random.Range(0,40), Random.Range(0,20), new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),1)); 
		TextureMap.Apply ();
		show.texture = TextureMap;
	}
		
}
