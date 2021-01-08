using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AEtoileComponent : MonoBehaviour
{
    public Vector3[] Positions;
    public float Speed=0.2f;
    public float Tolerance=0.2f;
    public GrideTile[,] Terrain; 
    public TerrainGridTool TerrainGridTool;

    private List<GrideTile> _openList=new List<GrideTile>();
    private List<GrideTile> _closeList=new List<GrideTile>();
    private Vector2Int _calculatedPos;
    
    public void GetPath(Vector2Int goalCell)
    {
        Terrain = TerrainGridTool.TerrainTiles;
        _calculatedPos = TerrainGridTool.GetTilePos(transform.position);

        if (!Terrain[_calculatedPos.x-1, _calculatedPos.y].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x-1, _calculatedPos.y])) CalculTileStraite(Terrain[_calculatedPos.x-1, _calculatedPos.y]);
        if (!Terrain[_calculatedPos.x+1, _calculatedPos.y].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x+1, _calculatedPos.y])) CalculTileStraite(Terrain[_calculatedPos.x+1, _calculatedPos.y]);
        if (!Terrain[_calculatedPos.x, _calculatedPos.y-1].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x, _calculatedPos.y-1])) CalculTileStraite(Terrain[_calculatedPos.x, _calculatedPos.y-1]);
        if (!Terrain[_calculatedPos.x, _calculatedPos.y+1].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x, _calculatedPos.y+1])) CalculTileStraite(Terrain[_calculatedPos.x, _calculatedPos.y+1]);
        if (!Terrain[_calculatedPos.x+1, _calculatedPos.y+1].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x+1, _calculatedPos.y+1])) CalculTileDiago(Terrain[_calculatedPos.x+1, _calculatedPos.y+1]);
        if (!Terrain[_calculatedPos.x-1, _calculatedPos.y+1].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x-1, _calculatedPos.y+1])) CalculTileDiago(Terrain[_calculatedPos.x-1, _calculatedPos.y+1]);
        if (!Terrain[_calculatedPos.x+1, _calculatedPos.y-1].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x+1, _calculatedPos.y-1])) CalculTileDiago(Terrain[_calculatedPos.x+1, _calculatedPos.y-1]);
        if (!Terrain[_calculatedPos.x-1, _calculatedPos.y-1].IsWalkable&&_openList.Contains(Terrain[_calculatedPos.x-1, _calculatedPos.y-1])) CalculTileDiago(Terrain[_calculatedPos.x-1, _calculatedPos.y-1]);
        
    }

    private void CalculTileStraite(GrideTile calculTile)
    {
        calculTile.GCost = Terrain[_calculatedPos.x, _calculatedPos.y].GCost + 10;
        calculTile.HCost = CalculatedDidsanceCost(_calculatedPos, calculTile.Gridposition);
        calculTile.FCost = calculTile.GCost + calculTile.HCost;
    }
    private void CalculTileDiago(GrideTile calculTile)
    {
        calculTile.GCost = Terrain[_calculatedPos.x, _calculatedPos.y].GCost + 10;
        calculTile.HCost = CalculatedDidsanceCost(_calculatedPos, calculTile.Gridposition);
        calculTile.FCost = calculTile.GCost + calculTile.HCost;
    }

    private int CalculatedDidsanceCost(Vector2Int a, Vector2Int b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return 14 * Mathf.Min(xDistance, yDistance) + 10 * remaining;
    }
}
