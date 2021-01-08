
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TerrainGridTool))]
public class TerrainGridToolEditor :Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TerrainGridTool terrainGridTool = (TerrainGridTool) target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate TileMap"))
        {
            terrainGridTool.Generate();
        }
        if (GUILayout.Button("GetHeightap"))
        {
            terrainGridTool.GetHeightMap();
        }
        GUILayout.EndHorizontal();
       GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reste TileMap"))
        {
            terrainGridTool.RestTileMap();
        }
        if (GUILayout.Button("Clear Tiles Info"))
        {
            terrainGridTool.ClearTile();
        }
        GUILayout.EndHorizontal();
    }
}
