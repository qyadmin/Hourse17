using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class XiaZhu_Model : MonoBehaviour
{
    public class ChipData
    {
        public string chip_tel;
        public string chip_money_all;
    }


    public GameObject listobj;
    public Transform parent;
    private HttpModelToCebianlan SendModel;
    public bool CanSend;
    public List<ChipData> data = new List<ChipData>();
    private void Start()
    {
        Static.Instance.AddValue("lastId", "0");
        InvokeRepeating("UpdateChip", 0, 5);
        SendModel = GetComponent<HttpModelToCebianlan>();
        StartCoroutine("InseChip");
    }
    [SerializeField]
    private Text tel;
    public void GetData(JsonData data)
    {
        JsonData jd = data["res"];
        foreach (JsonData child in jd)
        {
            string STR = tel.text;
            string ST = STR.Substring(0, 3) + "****" + STR.Substring(STR.Length - 2, 2);
            if (child["chip_tel"].ToString() == ST)
                continue;
            ChipData g = new ChipData();
            g.chip_tel = child["chip_tel"].ToString();
            g.chip_money_all = child["chip_money_all"].ToString();      
            this.data.Add(g);
            //Debug.Log(child["chip_id"].ToString()+"--"+ child["chip_tel"].ToString()+"---"+ child["chip_money_all"].ToString());
        }

        try
        {
            if(jd!=null&&jd.Count>0)
            Static.Instance.AddValue("lastId", jd[jd.Count - 1]["chip_id"].ToString());
        }
        catch
        {
            Debug.Log(jd.Count+"--------JD");
        }
    }

    public void AddData(Text money)
    {
        ChipData a = new ChipData();
        string STR = tel.text;
        a.chip_tel = STR.Substring(0,3)+"****"+ STR.Substring(STR.Length-2,2);
        a.chip_money_all = money.text + ".00";
        GameObject g = GameObject.Instantiate(listobj);
        GameObject par = GameObject.Instantiate(Parobj);
        par.transform.parent = ParFather.transform;
        par.transform.localPosition = Vector3.zero;
        g.SetActive(true);
        g.transform.SetParent(parent);
        g.transform.localScale = Vector3.one;
        Vector3 pos = g.transform.gameObject.GetComponent<RectTransform>().localPosition;
        g.transform.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(pos.x, pos.y, 0);
        g.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = a.chip_tel;
        g.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = a.chip_money_all;
        Debug.Log(a.chip_tel + "-----------ADDDSINGLE");
    }

    void UpdateChip()
    {
        if (CanSend)
        {
            SendModel.Get();
            if(SoundSetting.Instance.swich)
            Parobj.GetComponent<AudioSource>().volume = 0.5f;
        }            
        else
            Parobj.GetComponent<AudioSource>().volume = 0;

    }

    [SerializeField]
    GameObject Parobj,ParFather;

    IEnumerator InseChip()
    {
        while (true)
        {
            int a = Random.Range(1,8);
            int b = Random.Range(0, 3);
            if (HouseMove.GetHouseMove.time >= 40&& HouseMove.GetHouseMove.time <= 295)
            {
                yield return new WaitForSeconds(a);
                if (data.Count > 5)
                {
                    for (int i = 0; i <= b; i++)
                    {
                        GameObject g = GameObject.Instantiate(listobj);
                        GameObject par = GameObject.Instantiate(Parobj);
                        par.transform.parent = ParFather.transform;
                        par.transform.localPosition = Vector3.zero;
                        g.SetActive(true);
                        g.transform.SetParent(parent);
                        g.transform.localScale = Vector3.one;
                        Vector3 pos = g.transform.gameObject.GetComponent<RectTransform>().localPosition;
                        g.transform.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(pos.x,pos.y,0);
                        g.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = data[0].chip_tel;
                        g.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = data[0].chip_money_all;
                        data.RemoveAt(0);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            yield return 0;
        }
    }

}
