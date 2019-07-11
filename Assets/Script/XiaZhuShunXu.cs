using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;



public class XiaZhuShunXu : MonoBehaviour {

    public static XiaZhuShunXu Instance;

    [SerializeField]
    Transform father;

    public string shunxu;



    public void Start()
    {
        Instance = this;
    }

    public void getjson(JsonData obj)
    {
        JsonData resdata = obj["res"][0];
        shunxu = JsonMapper.ToJson(resdata["matches_ranking"]).Replace("\"", "");
        HouseMove.GetHouseMove.SetRunLoop_old(shunxu);
        string[] list = JsonMapper.ToJson(resdata["matches_ranking"]).Replace("\"","").Split(',');
        if(father != null)
        for (int i =0; i<list.Length;i++)
        {
            
            foreach (Transform j in father)
            {
                //Debug.Log("string"+i+"    "+"name"+j);
                if (j.name == list[i])
                {
                    j.transform.SetSiblingIndex(i+1);
                    Debug.Log(j.name+"   "+(i+1));
                }
                
            }
        }
    }
}
