using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorWindowAdd : EditorWindow {

    [MenuItem("菜单/属性设置")]
    static void MenueSetting()
    {
        //创建窗口
        Rect wr = new Rect(0, 0, 500, 500);
        EditorWindowAdd window = (EditorWindowAdd)EditorWindow.GetWindowWithRect(typeof(EditorWindowAdd), wr, true, "window name");
        window.Show();
    }

    private int GJL;
    private Object gett;

    SerializedObject _sr;
    private void OnGUI()
    {
        gett = EditorGUILayout.ObjectField("网络返回消息面板", gett, typeof(Object), true);
        
        if (GUILayout.Button("创建配置文件", GUILayout.Width(200)))
        {
            Create(gett);
        }
        if (GUILayout.Button("读取配置文件", GUILayout.Width(200)))
        {
            Load(gett);
        }
        if (_sr != null)
        {
            EditorGUILayout.PropertyField(_sr.FindProperty("attact"));
            _sr.ApplyModifiedProperties();
            _sr.UpdateIfRequiredOrScript();
        }
       
    }

    public void Awake()
    {

    }

    private void OnEnable()
    {
        
    }
    void OnFocus()
    {
       
        Debug.Log("当窗口获得焦点时调用一次");
    }

    void OnLostFocus()
    {
        Debug.Log("当窗口失去焦点时调用一次");
    }

    void OnHierarchyChange()
    {
        Debug.Log("当Hierarchy视图中的任何对象发生改变时调用");
    }

    void OnInspectorUpdate()
    {
        //重新绘制
        this.Repaint();
    }


    void OnSelectionChange()
    {
        //当窗口出去开启壮体啊，并且在Hierarchy视图中选择某个游戏对象时调用
        foreach (Transform t in Selection.transforms)
        {
            Debug.Log("OnSelectionChange" + t.name);
        }
    }

    void OnDestroy()
    {
        Debug.Log("当窗口关闭时调用");
    }


    void Load(Object obj)
    {
        _sr = new SerializedObject(Resources.Load(obj.name));
        
    }



    void Create(Object obj)

    {        // 实例化类  Bullet
        string objname = obj.name;
        ScriptableObject target = ScriptableObject.CreateInstance(objname);

        // 如果实例化 Bullet 类为空，返回

        if (!target)

        {

            Debug.LogWarning("Bullet not found");

            return;

        }

        // 自定义资源保存路径

        string path = Application.dataPath + "/BulletAeeet";

        // 如果项目总不包含该路径，创建一个

        if (!Directory.Exists(path))

        {
            Directory.CreateDirectory(path);

        }

        //将类名 Bullet 转换为字符串

        //拼接保存自定义资源（.asset） 路径

        path = string.Format("Assets/Resources/{0}.asset", (objname));

        // 生成自定义资源到指定路径

        AssetDatabase.CreateAsset(target, path);

    }

}
