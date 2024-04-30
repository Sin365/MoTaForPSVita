//using OfficeOpenXml;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;



//public class UnityTools : MonoBehaviour
//{
//	static string _mapInfoPath = Application.dataPath + "/Settings/地图信息.txt";
//	static string _propertyInfoPath = Application.dataPath + "/Settings/属性列表.txt";
//	static string _propertyListPath = "Assets/Settings/属性列表.xlsx";

//	[UnityEditor.MenuItem("我的工具/转换当前地图信息为 Json 文本文件")]
//	public static void TransferMapInfoToJsonTxt()
//	{
//		List<MapResourceInfo> mapInfos = new List<MapResourceInfo>();
//		// 获取场景信息
//		GameObject levelObj = GameObject.Find("Level");
//		int levelNum = 0;
//		while (true)
//		{
//			// 按序号获取 level
//			Transform levelChildren = levelObj.transform.Find(levelNum.ToString());
//			if (levelChildren != null)
//			{
//				ResourceController[] resources = levelChildren.GetComponentsInChildren<ResourceController>();
//				foreach (var resource in resources)
//				{
//					MapResourceInfo mapInfo = new MapResourceInfo
//					{
//						Level = levelNum,
//						ID = resource.ID,
//						Point = resource.transform.position,
//					};
//					switch (resource.tag)
//					{
//						case "Environment":
//							mapInfo.Type = EResourceType.Environment;
//							break;
//						case "Item":
//							mapInfo.Type = EResourceType.Item;
//							break;
//						case "Enemy":
//							mapInfo.Type = EResourceType.Enemy;
//							break;
//						case "Player":
//							mapInfo.Type = EResourceType.Actor;
//							break;
//						case "Actor":
//							mapInfo.Type = EResourceType.Actor;
//							break;
//						default:
//							print("这玩意儿是个啥？" + resource.gameObject.name);
//							break;
//					}
//					mapInfos.Add(mapInfo);
//				}
//				levelNum++;
//			}
//			else
//			{
//				break;
//			}
//		}
//		// 转 json
//		string mapJson = JsonUtility.ToJson(new Serialization<MapResourceInfo>(mapInfos));
//		// 打开 txt
//		using (FileStream fs = new FileStream(_mapInfoPath, FileMode.OpenOrCreate))
//		{
//			// 清空之前内容 不清空会造成 JSON 格式化异常 BUG
//			fs.Seek(0, SeekOrigin.Begin);
//			fs.SetLength(0);
//			// 编码转 utf-8
//			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(mapJson);
//			fs.Write(bytes, 0, bytes.Length);
//		}
//	}

//	[UnityEditor.MenuItem("我的工具/将属性列表应用于预制体")]
//	public static void UsePropertyToPrefab()
//	{
//		// 判断文件是否存在
//		if (!File.Exists(_propertyListPath))
//		{
//			print("属性列表文件不存在，无法应用于预制体");
//			return;
//		}
//		// 授权
//		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//		// 加载 excel 禁止循环加载浪费内存
//		using (ExcelPackage propertyListExcel = new ExcelPackage(new FileInfo(_propertyListPath)))
//		{
//			// 获取资源信息
//			ExcelWorksheet environmentSheet = propertyListExcel.Workbook.Worksheets[0];
//			ExcelWorksheet itemSheet = propertyListExcel.Workbook.Worksheets[1];
//			ExcelWorksheet actorSheet = propertyListExcel.Workbook.Worksheets[2];
//			ExcelWorksheet enemySheet = propertyListExcel.Workbook.Worksheets[3];
//			// 应用于预制体
//			for (int i = 2; i <= environmentSheet.Dimension.Rows; i++)
//			{
//				if (null == environmentSheet.Cells[i, 4].Value) continue;
//				GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + environmentSheet.Cells[i, 4].Value.ToString() + ".prefab");
//				tempObj.GetComponent<EnvironmentController>().ID = int.Parse(environmentSheet.Cells[i, 1].Value.ToString());
//				tempObj.GetComponent<EnvironmentController>().Name = environmentSheet.Cells[i, 2].Value.ToString();
//				tempObj.GetComponent<EnvironmentController>().Info = environmentSheet.Cells[i, 3].Value?.ToString();
//				EditorUtility.SetDirty(tempObj);
//				AssetDatabase.SaveAssets();
//				AssetDatabase.Refresh();
//			}
//			for (int i = 2; i <= itemSheet.Dimension.Rows; i++)
//			{
//				if (null == itemSheet.Cells[i, 4].Value) continue;
//				GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + itemSheet.Cells[i, 4].Value.ToString() + ".prefab");
//				tempObj.GetComponent<ItemController>().ID = int.Parse(itemSheet.Cells[i, 1].Value.ToString());
//				tempObj.GetComponent<ItemController>().Name = itemSheet.Cells[i, 2].Value.ToString();
//				tempObj.GetComponent<ItemController>().Info = itemSheet.Cells[i, 3].Value?.ToString();
//				tempObj.GetComponent<ItemController>().IconPath = itemSheet.Cells[i, 5].Value.ToString();
//				tempObj.GetComponent<ItemController>().UseCount = int.Parse(itemSheet.Cells[i, 6].Value.ToString());
//				EditorUtility.SetDirty(tempObj);
//				AssetDatabase.SaveAssets();
//				AssetDatabase.Refresh();
//			}
//			for (int i = 2; i <= actorSheet.Dimension.Rows; i++)
//			{
//				if (null == actorSheet.Cells[i, 4].Value) continue;
//				GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + actorSheet.Cells[i, 4].Value.ToString() + ".prefab");
//				tempObj.GetComponent<ActorController>().ID = int.Parse(actorSheet.Cells[i, 1].Value.ToString());
//				tempObj.GetComponent<ActorController>().Name = actorSheet.Cells[i, 2].Value.ToString();
//				tempObj.GetComponent<ActorController>().Info = actorSheet.Cells[i, 3].Value?.ToString();
//				tempObj.GetComponent<ActorController>().IconPath = actorSheet.Cells[i, 5].Value.ToString();
//				EditorUtility.SetDirty(tempObj);
//				AssetDatabase.SaveAssets();
//				AssetDatabase.Refresh();
//			}
//			for (int i = 2; i <= enemySheet.Dimension.Rows; i++)
//			{
//				if (null == enemySheet.Cells[i, 4].Value) continue;
//				GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + enemySheet.Cells[i, 4].Value.ToString() + ".prefab");
//				tempObj.GetComponent<EnemyController>().ID = int.Parse(enemySheet.Cells[i, 1].Value.ToString());
//				tempObj.GetComponent<EnemyController>().Name = enemySheet.Cells[i, 2].Value.ToString();
//				tempObj.GetComponent<EnemyController>().Info = enemySheet.Cells[i, 3].Value?.ToString();
//				tempObj.GetComponent<EnemyController>().IconPath = enemySheet.Cells[i, 5].Value?.ToString();
//				tempObj.GetComponent<EnemyController>().Health = int.Parse(enemySheet.Cells[i, 6].Value.ToString());
//				tempObj.GetComponent<EnemyController>().MaxHealth = int.Parse(enemySheet.Cells[i, 6].Value.ToString());
//				tempObj.GetComponent<EnemyController>().Attack = int.Parse(enemySheet.Cells[i, 7].Value.ToString());
//				tempObj.GetComponent<EnemyController>().Defence = int.Parse(enemySheet.Cells[i, 8].Value.ToString());
//				tempObj.GetComponent<EnemyController>().Gold = int.Parse(enemySheet.Cells[i, 9].Value.ToString());
//				EditorUtility.SetDirty(tempObj);
//				AssetDatabase.SaveAssets();
//				AssetDatabase.Refresh();
//			}
//		}
//	}

//	[UnityEditor.MenuItem("我的工具/转换属性列表为 Json 文本文件")]
//	public static void TransferPropertyInfoToJsonTxt()
//	{
//		List<ResourceInfo> resourceInfos = new List<ResourceInfo>();
//		// 判断文件是否存在
//		if (!File.Exists(_propertyListPath))
//		{
//			print("属性列表文件不存在，无法转换为 Json 文本文件");
//			return;
//		}
//		// 授权
//		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//		// 加载 excel 禁止循环加载浪费内存
//		using (ExcelPackage propertyListExcel = new ExcelPackage(new FileInfo(_propertyListPath)))
//		{
//			// 获取资源信息
//			for (int i = 0; i < 4; i++)
//			{
//				ExcelWorksheet sheet = propertyListExcel.Workbook.Worksheets[i];
//				EResourceType type = (EResourceType)i;
//				for (int j = 2; j <= sheet.Dimension.Rows; j++)
//				{
//					resourceInfos.Add(new ResourceInfo
//					{
//						Type = type,
//						ID = int.Parse(sheet.Cells[j, 1].Value.ToString()),
//						Name = sheet.Cells[j, 2].Value.ToString(),
//						Info = sheet.Cells[j, 3].Value?.ToString(),
//						Path = sheet.Cells[j, 4].Value?.ToString(),
//						IconPath = sheet.Cells[j, 5].Value?.ToString(),
//					});
//				}
//			}
//		}
//		// 转 json
//		string resourceJson = JsonUtility.ToJson(new Serialization<ResourceInfo>(resourceInfos));
//		// 打开 txt
//		using (FileStream fs = new FileStream(_propertyInfoPath, FileMode.OpenOrCreate))
//		{
//			// 清空之前内容 不清空会造成 JSON 格式化异常 BUG
//			fs.Seek(0, SeekOrigin.Begin);
//			fs.SetLength(0);
//			// 编码转 utf-8
//			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(resourceJson);
//			fs.Write(bytes, 0, bytes.Length);
//		}
//	}
//}
