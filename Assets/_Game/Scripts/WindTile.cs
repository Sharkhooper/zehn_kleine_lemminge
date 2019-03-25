using System;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WindTile : TileBase
{
	[SerializeField] private Sprite sprite;
	[SerializeField] public Vector2 direction;

#if UNITY_EDITOR
	[MenuItem("Assets/Create/WindTile")]
	public static void CreateWindTile() {
		string path = EditorUtility.SaveFilePanelInProject("Save Wind Tile", "New Wind Tile", "Asset", "Save Wind Tile", "Assets");
		if (path == "") return;
		AssetDatabase.CreateAsset(CreateInstance<WindTile>(), path);
	}
#endif
}
