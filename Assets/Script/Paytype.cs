using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class Paytype : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject input;
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
        text.text = (dropdown.value + 1).ToString();
    }
    public void Mytype()
    {
        text.text = (1+dropdown.value).ToString();
    }
    private void Update()
    {
        if (text.text == "3")
        {
            input.SetActive(true);
        }
        else
        {
            input.SetActive(false);
        }
    }

}
