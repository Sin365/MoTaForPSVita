//using UnityEngine;
//using System.IO;
//using System;
//public class PrefabsToLow : MonoBehaviour
//{
//	void Start()
//	{

//		var resourcesPath = Application.dataPath;
//		var absolutePaths = System.IO.Directory.GetFiles(resourcesPath, "*.prefab", System.IO.SearchOption.AllDirectories);
//		for (int i = 0; i < absolutePaths.Length; i++)
//		{
//			string path = "Assets" + absolutePaths[i].Remove(0, resourcesPath.Length);
//			path = path.Replace("\\", "/");
//			try
//			{


//				Debug.Log(path);
//				string newpath = path.Replace("Assets", "Assets/Newprefab");
//				string newdir = Path.GetDirectoryName(newpath);
//				if (!Directory.Exists(newdir))
//					Directory.CreateDirectory(newdir);
//				Debug.Log(newpath);

//				GameObject prefab = Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject);
//				if (!prefab)
//				{
//					Debug.LogError($"生成错误[读取Null]：{path}");
//					continue;
//				}
//				UnityEditor.PrefabUtility.CreatePrefab(newpath, prefab);
//				DestroyImmediate(prefab);
//			}
//			catch (Exception ex)
//			{
//				Debug.LogError($"生成错误[报错]：{path},{ex.ToString()}");
//				continue;
//			}
//		}

//		return;
//		//GameObject prefab = Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Resources/UI/MainEventSystem.prefab", typeof(GameObject)) as GameObject);
//		//UnityEditor.PrefabUtility.CreatePrefab("Assets/Prefabs/MainEventSystemClone.prefab", prefab);
//	}
//}