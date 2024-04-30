//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//namespace DFramework.Tools
//{
//	public class GeneratePrefabs : EditorWindow
//	{
//		[MenuItem("我的工具/物体生成")]
//		static void 显示新窗口()
//		{
//			GeneratePrefabs GenerateWindow = GetWindow<GeneratePrefabs>(true, "物体生成");
//			GenerateWindow.Show();//显示一个窗口
//		}
//		[SerializeField]
//		protected List<GameObject> _assetList = new List<GameObject>();

//		protected SerializedObject _serializedObject;

//		protected SerializedProperty _assetLstProperty;

//		protected void OnEnable()
//		{
//			_serializedObject = new SerializedObject(this);
//			_assetLstProperty = _serializedObject.FindProperty("_assetList");
//		}

//		private string mParentName;

//		void GenerateAssets()
//		{
//			float progress = 0;

//			EditorUtility.DisplayProgressBar("进度条", "生成中", progress);

//			if (mParentName == null || mParentName == "")
//			{
//				mParentName = "资源父物体";
//			}

//			Transform mParent = new GameObject(mParentName).transform;
//			float childCount = _assetList.Count;

//			for (int i = 0; i < _assetList.Count; i++)
//			{
//				if (_assetList[i] != null)
//				{
//					GameObject mChild = Instantiate(_assetList[i]);

//					mChild.transform.SetParent(mParent);
//				}
//				progress = (i + 1) / childCount;
//				EditorUtility.DisplayProgressBar("进度条", "生成中", progress);
//			}
//			EditorUtility.ClearProgressBar();
//		}

//		void OnGUI()
//		{
//			mParentName = EditorGUILayout.TextField("资源父物体名称：", mParentName);

//			GUILayout.Label("将资源添加到集合中");

//			_serializedObject.Update();

//			EditorGUI.BeginChangeCheck();

//			EditorGUILayout.PropertyField(_assetLstProperty, true);

//			if (EditorGUI.EndChangeCheck())
//			{
//				_serializedObject.ApplyModifiedProperties();
//			}


//			if (GUILayout.Button("开始生成"))
//			{
//				GenerateAssets();
//			}
//		}
//	}
//}