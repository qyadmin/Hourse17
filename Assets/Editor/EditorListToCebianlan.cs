using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using TypeClass;
[CustomEditor(typeof(HttpModelToCebianlan))]
public class TestListInspector2 : Editor
{
	private ReorderableList ActionOtherList;
	private ReorderableList BaseDataList;
	private ReorderableList TextList;
    private ReorderableList m_NameList;
	private ReorderableList GetDataList;
	private ReorderableList SetDataList;
	private ReorderableList ListDataList;
	private ReorderableList ListSingleList;
    private SerializedProperty GetMessageShow;
	private SerializedProperty GetBaseDataShow;
	private SerializedProperty GetListDataShow;
    private void OnEnable()
    {
		ActionOtherList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("ActionAdd"),
			true, true, true, true);

		BaseDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("BaseDataList"),
			true, true, true, true);
		
		TextList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("CheckText"),
			true, true, true, true);

        m_NameList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("SendData"),
            true, true, true, true);
		
		GetDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("GetDataList"),
          true, true, true, true);

		SetDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("BackDataGet"),
			true, true, true, true);

		ListDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("MyListMessage").FindPropertyRelative("NameList"),
			true, true, true, true);

		ListSingleList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("MyListMessage").FindPropertyRelative("NameSingleList"),
			true, true, true, true);


		GetDataList.drawElementCallback += DrawNameElementObj;

        m_NameList.drawElementCallback += DrawNameElement;

		SetDataList.drawElementCallback += DrawNameElementSetBack;

		ListDataList.drawElementCallback += DrawNameElementList;

		ListSingleList.drawElementCallback += DrawNameSingleList;

		TextList.drawElementCallback += DrawNameElementText;

		BaseDataList.drawElementCallback += DrawNameElementBaseData;

		ActionOtherList.drawElementCallback += DrawNameElementOther;

		TextList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "需要判断是否为空的参数");
		};

		ListSingleList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "添加获取返回数据的列表");
		};

        m_NameList.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "添加请求数据的列表");
        };

		GetDataList.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "返回数据列表");
        };

		SetDataList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "返回数据赋值列表");
		};

		ListDataList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "需要取得的列表数据");
		};

		BaseDataList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "添加base接收列表");
		};

		ActionOtherList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "添加传送给其他HttpModel处理的列表");
		};
    }

	private void DrawNameElementOther(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = ActionOtherList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(rect, element, GUIContent.none);
	}




	//BaseDATALIST
	private void DrawNameElementBaseData(Rect rect, int index, bool selected, bool focused)
	{
//		GUI.backgroundColor = Color.white;
		SerializedProperty element = BaseDataList.serializedProperty.GetArrayElementAtIndex(index);
		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
//
//		if (!element.FindPropertyRelative ("IsGet").boolValue) 
//			GUI.backgroundColor = Color.red;
			EditorGUI.PropertyField (new Rect (rect.x + 2 * rect.width / 3.5f, rect.y, rect.width / 3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("GetType"), GUIContent.none);
//		EditorGUI.LabelField (new Rect(rect.width-10, rect.y, 40, EditorGUIUtility.singleLineHeight),"ToInt");
//		EditorGUI.PropertyField(new Rect(rect.width-25, rect.y, 10, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsInt"),GUIContent.none);

			EditorGUI.PropertyField (new Rect (rect.x, rect.y, rect.width / 3.6f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			GetBackType ShowType = (GetBackType)element.FindPropertyRelative ("GetType").enumValueIndex;
			if (ShowType == GetBackType.Text)
				EditorGUI.PropertyField (new Rect (rect.x + rect.width / 3.5f, rect.y, rect.width / 3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("UseTextGet"), GUIContent.none);
			if (ShowType == GetBackType.InputText)
				EditorGUI.PropertyField (new Rect (rect.x + rect.width / 3.5f, rect.y, rect.width / 3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("UseInputGet"), GUIContent.none);
//		GUI.backgroundColor = Color.white;
	}




	private void DrawNameSingleList(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = ListSingleList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x+2*rect.width/3f, rect.y, rect.width/5.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);
		EditorGUI.PropertyField(new Rect(rect.x+2*rect.width/3f-25, rect.y, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsSave"), GUIContent.none);
		EditorGUI.LabelField (new Rect(rect.width-10, rect.y, 40, EditorGUIUtility.singleLineHeight),"ToInt");
		EditorGUI.PropertyField(new Rect(rect.width-25, rect.y, 10, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsInt"),GUIContent.none);

		EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width/3.6f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Name"), GUIContent.none);
		GetBackType ShowType =(GetBackType) element.FindPropertyRelative ("MyType").enumValueIndex;
		if(ShowType==GetBackType.Text)
			EditorGUI.PropertyField (new Rect(rect.x+rect.width/3.5f, rect.y,rect.width/3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Showtext"), GUIContent.none);
		if(ShowType==GetBackType.InputText)
			EditorGUI.PropertyField (new Rect(rect.x+rect.width/3.5f, rect.y, rect.width/3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ShowInputtext"), GUIContent.none);
		if (ShowType == GetBackType.Event) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 3.5f, rect.y, rect.width / 6f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventObject"), GUIContent.none);
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 4.5f*2, rect.y, rect.width / 5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventName"), GUIContent.none);
		}
	}

	private void DrawNameElementList(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = ListDataList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Name"), GUIContent.none);
		EditorGUI.PropertyField (new Rect(rect.x+rect.width/4+10, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("MyObjeType"), GUIContent.none);
		EditorGUI.PropertyField (new Rect(rect.width-50, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("MySaveType"), GUIContent.none);

		if ((SaveNameType)element.FindPropertyRelative ("MySaveType").enumValueIndex == SaveNameType.SaveOtherName) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-150, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("OtherName"), GUIContent.none);
		}
		if ((SaveNameType)element.FindPropertyRelative ("MySaveType").enumValueIndex == SaveNameType.ActionEvent) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-150, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventName"), GUIContent.none);
		}

		//EditorGUI.PropertyField(rect, element, GUIContent.none);
	}



    private void DrawNameElementObj(Rect rect, int index, bool selected, bool focused)
    {
		SerializedProperty element = GetDataList.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(rect, element, GUIContent.none);
//        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("SaveButtonEvent"), GUIContent.none);
//        EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("GetGasValve"), GUIContent.none);
    }

	private void DrawNameElementSetBack(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = SetDataList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);
		EditorGUI.PropertyField (new Rect(rect.width-20, rect.y, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("IsSave"), GUIContent.none);

		GetBackTypeValue ShowType =(GetBackTypeValue) element.FindPropertyRelative ("MyType").enumValueIndex;
		if ( ShowType== GetBackTypeValue.GetValue) 
		{
			SetDataList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"获取的值赋给Text",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-100, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetText"), GUIContent.none);
		}
		if (ShowType == GetBackTypeValue.NoGetValue) 
		{
			SetDataList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"获取的值保存到列表",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
		}
	}


	private void DrawNameElementText(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = TextList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width/3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);
		GetBackType ShowType =(GetBackType) element.FindPropertyRelative ("MyType").enumValueIndex;
		if(ShowType==GetBackType.Text)
			EditorGUI.PropertyField (new Rect(rect.x+rect.width/3.5f, rect.y,rect.width/3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Showtext"), GUIContent.none);
		if(ShowType==GetBackType.InputText)
			EditorGUI.PropertyField (new Rect(rect.x+rect.width/3.5f, rect.y, rect.width/3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ShowInputtext"), GUIContent.none);
	}
		
    private void DrawNameElement(Rect rect, int index, bool selected, bool focused)
    {
        SerializedProperty element = m_NameList.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);
		EditorGUI.LabelField (new Rect(rect.width, rect.y, 40, EditorGUIUtility.singleLineHeight),"Save");
		EditorGUI.PropertyField (new Rect(rect.width-20, rect.y, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("IsSave"), GUIContent.none);
		EditorGUI.LabelField (new Rect(rect.width-85, rect.y, 80, EditorGUIUtility.singleLineHeight),"TagWarm");
		EditorGUI.PropertyField (new Rect(rect.width-160, rect.y, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("IsEmpty"), GUIContent.none);

		WarmType GetWarmType =(WarmType) element.FindPropertyRelative ("IsEmpty").enumValueIndex;
		if (GetWarmType == WarmType.Empty) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-160, rect.y+20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EmptyTag"), GUIContent.none);
		}
		if (GetWarmType == WarmType.XandY) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-160, rect.y+20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EmptyTag"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-80, rect.y+20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Y_Name"), GUIContent.none);
		}

        if (GetWarmType == WarmType.Length)
        {
            EditorGUI.PropertyField(new Rect(rect.width - 160, rect.y + 20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("EmptyTag"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.width - 80, rect.y + 20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Length"), GUIContent.none);
        }

        if (GetWarmType == WarmType.Value) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-160, rect.y+20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EmptyTag"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-80, rect.y+20, 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ValueName"), GUIContent.none);
		}

		GetTypeValue ShowType =(GetTypeValue) element.FindPropertyRelative ("MyType").enumValueIndex;
		if ( ShowType== GetTypeValue.GetFromValue) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从输入的值中获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.x+110, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetValue"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFormText) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"拖入的Text中获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.x+110, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetText"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFromInputField) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从输入Text中获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.x+110, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetInputField"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFromList) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从储存列表中按自己的的名字获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFromListOther) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从储存列表中按输入的名字获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.x+110, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("OtherName"), GUIContent.none);
		}
        //EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("ShowError"), GUIContent.none);
        //EditorGUI.PropertyField(rect, itemData, GUIContent.none);
    }




    public override void OnInspectorGUI()
    {
        serializedObject.Update();
		//TextList.DoLayoutList();
        m_NameList.DoLayoutList();

		//EditorGUILayout.PropertyField(serializedObject.FindProperty("WarmText"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("IsLock"));
		GetMessageShow = serializedObject.FindProperty("Data");
		EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("Action"));
		EditorGUI.BeginDisabledGroup (GetMessageShow.FindPropertyRelative("Action").boolValue);
		EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("CutCount"));
		EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("DataName"));
		EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("URL"));

		EditorGUILayout.PropertyField (GetMessageShow.FindPropertyRelative ("IsDefuset"));
		GetBaseDataShow = GetMessageShow.FindPropertyRelative ("GetBase");
		if (GetMessageShow.FindPropertyRelative ("IsDefuset").boolValue) 
		{
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("code"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("result"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("msg"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("url"));
//
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("codetext"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("resulttext"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("msgtext"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("urltext"));
//			EditorGUILayout.PropertyField (GetBaseDataShow.FindPropertyRelative ("msgInputtext"));
		}
		EditorGUI.EndDisabledGroup ();

		BaseDataList.DoLayoutList ();
		ActionOtherList.DoLayoutList ();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("DataType"));
		if ((TypeGo)(serializedObject.FindProperty ("DataType").enumValueIndex) ==TypeGo.GetTypeB) 
		{
			GUI.backgroundColor = Color.green;
			GetListDataShow = GetMessageShow.FindPropertyRelative ("MyListMessage");
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("FatherObj"));
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("InsObj"));
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("MyListType"));
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("ObjTag"));

			ListDataList.DoLayoutList ();
			GUI.backgroundColor = Color.white;
		}
		if ((TypeGo)(serializedObject.FindProperty ("DataType").enumValueIndex) == TypeGo.GetTypeC) 
		{
			GUI.backgroundColor = Color.yellow;
			ListSingleList.DoLayoutList ();
			GUI.backgroundColor = Color.white;
		}
        if ((TypeGo)(serializedObject.FindProperty("DataType").enumValueIndex) == TypeGo.GetTypeD)
        {
            EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("SendMessageObj"));
            EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("MessageName"));
        }

        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("ShowMessage"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("Suc"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("Fal"));
		GUI.backgroundColor = Color.red;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("DoAction"));
		GUI.backgroundColor = Color.white;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("NoShow"));
        serializedObject.ApplyModifiedProperties();
    }
}



[CustomEditor(typeof(SoundSettingSpriteObj))]
public class SoundSettingInspectpr : Editor
{
    private Sound[] sounds;
    private int soundclasscount = 0;

    private Source[] sources;
    private int sourceclasscount = 0;

    public override void OnInspectorGUI()
    {
        SoundSettingSpriteObj soundsetting = (SoundSettingSpriteObj)target;

        soundsetting = CreatePathLevePointClass(soundsetting);
    }
    private SoundSettingSpriteObj CreatePathLevePointClass(SoundSettingSpriteObj soundsetting)
    {

        EditorGUILayout.BeginVertical("box");
        if (DrawHeader("音量控制器", true, 0))
        {
            EditorGUI.indentLevel++;
            soundclasscount = soundsetting.Allsound.Length;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Size", soundclasscount.ToString());
            if (GUILayout.Button("添 加", GUILayout.Width(50), GUILayout.Height(20)))
            {
                soundclasscount++;
                soundsetting.Allsound = SetGetPathLevelPoint(soundsetting.Allsound, soundclasscount);
            }
            if (GUILayout.Button("删 除", GUILayout.Width(50), GUILayout.Height(20)))
            {
                if (soundclasscount > 0)
                {
                    if (soundclasscount > 0)
                    {
                        soundclasscount--;
                        soundsetting.Allsound = SetGetPathLevelPoint(soundsetting.Allsound, soundclasscount);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            if (soundclasscount > 0)
            {
                for (int i = 0; i < soundsetting.Allsound.Length; i++)
                {
                    string soundtypename = "声音种类" + (i+1).ToString();

                    EditorGUILayout.BeginVertical("box");

                    if (DrawHeader(soundtypename, true, 0))
                    {
                        soundsetting.Allsound[i] = CreatePathLevelPointClass(soundsetting.Allsound[i],i);
                    }
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(5);
                }
            }
        }


        EditorGUILayout.EndVertical();
        return soundsetting;
    }

    private Sound[] SetGetPathLevelPoint(Sound[] soundindex, int count)
    {
        if (count > 0)
        {
            sounds = new Sound[count];
            for (int i = 0; i < count; i++)
            {
                sounds[i] = new Sound();
            }

            for (int i = 0; i < soundindex.Length; i++)
            {
                if (i < sounds.Length)
                {
                    sounds[i] = soundindex[i];
                }
            }
            soundindex = sounds;
        }
        else
        {
            sounds = new Sound[0];
            soundindex = new Sound[0];
        }

        return soundindex;
    }


    private Source[] SetSource(Source[] sourceindex, int count)
    {
        if (count > 0)
        {
            sources = new Source[count];
            for (int i = 0; i < count; i++)
            {
                sources[i] = new Source();
            }
            if (sourceindex != null)
            {
                for (int i = 0; i < sourceindex.Length; i++)
                {
                    if (i < sources.Length)
                    {
                        sources[i] = sourceindex[i];
                    }
                }
            }
            
            sourceindex = sources;
        }
        else
        {
            sources = new Source[0];
            sourceindex = new Source[0];
        }

        return sourceindex;
    }




    private Source[] creatsource(Source[] sourcesetting,int num)
    {

        EditorGUILayout.BeginVertical("box");
        if (DrawHeader("声音", true, 10))
        {
            EditorGUI.indentLevel++;
            if (sourcesetting != null)
            {
                if (sourcesetting.Length > 0)
                {
                    sourceclasscount = sourcesetting.Length;
                }
                else
                {
                    sourceclasscount = 0;
                }
            }
            else
                sourceclasscount = 0;


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Size", sourceclasscount.ToString());
            if (GUILayout.Button("添 加", GUILayout.Width(50), GUILayout.Height(20)))
            {
                sourceclasscount++;
                sourcesetting = SetSource(sourcesetting, sourceclasscount);
            }
            if (GUILayout.Button("删 除", GUILayout.Width(50), GUILayout.Height(20)))
            {
                if (sourceclasscount > 0)
                {
                    if (sourceclasscount > 0)
                    {
                        sourceclasscount--;
                        sourcesetting = SetSource(sourcesetting, sourceclasscount);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            if (sourceclasscount > 0)
            {
                for (int i = 0; i < sourcesetting.Length; i++)
                {
                    string sourcetypename = "喇叭" + (i+1).ToString();

                    EditorGUILayout.BeginVertical("box");
                    if (DrawHeader(sourcetypename, true, 20))
                    {
                        sourcesetting[i] = CreatSource(sourcesetting[i], i);
                    }
                    EditorGUILayout.EndVertical();

                    GUILayout.Space(5);
                }
            }
        }
        EditorGUILayout.EndVertical();
        return sourcesetting;
    }




    private Source CreatSource(Source sourceindex,int num)
    {
        sourceindex.audio = (AudioSource)EditorGUILayout.ObjectField("AudioSource", sourceindex.audio, typeof(AudioSource));
        sourceindex.volume_Max = EditorGUILayout.FloatField(sourceindex.volume_Max);
        return sourceindex;
    }


    private Sound CreatePathLevelPointClass(Sound soundindex,int num)
    {
        //soundindex.CameraPathObj = (GameObject)EditorGUILayout.ObjectField("Camera Path Obj", pathLevelPointClass.CameraPathObj, typeof(Object));
        soundindex.Volume = EditorGUILayout.IntSlider("音量", soundindex.Volume, 0, 100);
        soundindex.sources = creatsource(soundindex.sources,num);
        //pathLevelPointClass.PointPosition = CreatePointPostion(pathLevelPointClass.PointPosition, num);
        return soundindex;
    }





    //摘自NGUI
    public bool DrawHeader(string text, bool isTrue, float offset) { return DrawHeader(text, text, offset, false, isTrue); }

    //摘自NGUI
    public bool DrawHeader(string text, string key, float offset, bool forceOn, bool minimalistic)
    {
        bool state = EditorPrefs.GetBool(key, true);

        if (!minimalistic) GUILayout.Space(3f);
        if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal();
        GUI.changed = false;

        if (minimalistic)
        {
            if (state) text = "\u25BC" + (char)0x200a + text;
            else text = "\u25BA" + (char)0x200a + text;

            GUILayout.BeginHorizontal();
            GUI.contentColor = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.7f) : new Color(0f, 0f, 0f, 0.7f);
            GUILayout.Label("", GUILayout.Width(offset));
            if (!GUILayout.Toggle(true, text, "PreToolbar2", GUILayout.MinWidth(20f))) state = !state;
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();
        }
        else
        {
            text = "<b><size=11>" + text + "</size></b>";
            if (state) text = "\u25BC " + text;
            else text = "\u25BA " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
        }

        if (GUI.changed) EditorPrefs.SetBool(key, state);

        if (!minimalistic) GUILayout.Space(2f);
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!forceOn && !state) GUILayout.Space(3f);
        return state;
    }

}