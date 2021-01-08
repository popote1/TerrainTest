
using System;
using UnityEngine;

public class TerrainGridTool : MonoBehaviour
{
    public GameObject NewSpriteTile;
    public GrideTile[,] TerrainTiles;
    public Terrain Terrain;
    public Texture TestTexture;
    public MeshRenderer TestPlane;
    
    [Header("Steep Paramettres")]
    [Range(0, 90)] public float SteepBlocking=45;
    public Color WalkableColor=Color.green;
    public Color NoWalkableColor=Color.red;

    [Header("Grid Info")] 
    public int height;
    public int width;
    public float AltitudeOffset=0.1f;
    public Transform Origin;
    public float CellSize=1;

    //private values
    private float[,] _terrainHeightMap;
    private float[,] _terrainSteepMap;

    public void Start()
    {
       
            Generate();
        
    }

    public void Generate()
    {
        
            TerrainTiles = new GrideTile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameObject sprite = Instantiate(NewSpriteTile,
                        Origin.position + new Vector3(x * CellSize + CellSize / 2, +AltitudeOffset, y * CellSize + CellSize / 2),
                        Quaternion.Euler(new Vector3(90, 0, 0)),Origin.transform);
                    sprite.name = x+"/"+y;
                    sprite.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.2f);
                    TerrainTiles[x, y] = new GrideTile(new Vector2Int(x, y), sprite);
                }
            }
       
    }

    public void GetHeightMap()
    { 
        _terrainHeightMap = new float[width,height];
        _terrainHeightMap = Terrain.terrainData.GetInterpolatedHeights(0.005f, 0.005f, height, width, 0.01f, 0.01f);
       //_terrainHeightMap= Terrain.terrainData.GetHeights(1, 1, width, height);
       for (int x = 0; x < width; x++)
       {
           for (int y = 0; y < height; y++)
           {
               Debug.Log(" la hight est set sur la tile " + TerrainTiles[x, y].Gridposition +" pour mettre a la valeur"+_terrainHeightMap[y,x]);
              // TerrainTiles[x, y].Sprite.GetComponent<SpriteRenderer>().color =Color.green;
              //TerrainTiles[x, y].Sprite.GetComponent<SpriteRenderer>().color = new Color(_terrainHeightMap[y,x],_terrainHeightMap[y,x],_terrainHeightMap[y,x],1);
              TerrainTiles[x, y].Sprite.transform.position = new Vector3(TerrainTiles[x, y].Sprite.transform.position.x,_terrainHeightMap[y,x]+AltitudeOffset,TerrainTiles[x, y].Sprite.transform.position.z);
           }
       }
    }

    public void GetSteepMap()
    {
        _terrainSteepMap = new float[width,height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++)
            {
                _terrainSteepMap[x,y] = Terrain.terrainData.GetSteepness((float)x/100 , (float)y/100 );
            }
        }
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log(" la hight est set sur la tile " + TerrainTiles[x, y].Gridposition +" pour mettre a la valeur"+_terrainSteepMap[y,x]);
                //TerrainTiles[x, y].Sprite.GetComponent<SpriteRenderer>().color = new Color(_terrainSteepMap[x, y]/90,_terrainSteepMap[x, y]/90,_terrainSteepMap[x, y]/90,1);
                if (_terrainSteepMap[x, y] < SteepBlocking)
                {
                    TerrainTiles[x, y].Sprite.GetComponent<SpriteRenderer>().color = WalkableColor;
                    TerrainTiles[x, y].IsWalkable = true;
                }
                else
                {
                    TerrainTiles[x, y].Sprite.GetComponent<SpriteRenderer>().color = NoWalkableColor;
                    TerrainTiles[x, y].IsWalkable = false;
                }
            }
        }
    }

    public void SetTexture()
    {
        Terrain.materialTemplate.mainTexture = TestTexture;
        Texture2D testText = new Texture2D(width,height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_terrainSteepMap[x, y] < SteepBlocking)
                {
                    testText.SetPixel(x,y,WalkableColor);
                }
                else
                {
                    testText.SetPixel(x,y,NoWalkableColor);
                }
            }
        }

        TestPlane.material.mainTexture=testText;
    }

    public void RestTileMap()
    {
        foreach (GrideTile tile in TerrainTiles)
        {
            Destroy(tile.Sprite);
        }
        TerrainTiles = null;
    }

    public void ClearTile()
    {
        TerrainTiles = null;
    }

    public Vector2Int GetTilePos(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(Origin.position.x+pos.x),Mathf.RoundToInt(Origin.position.z+pos.z));
    }

    public int GetXPos(Vector3 pos)
    {
        return Mathf.RoundToInt(Origin.position.x + pos.x);
    }

    public int GetYPos(Vector3 pos)
    {
        return Mathf.RoundToInt(Origin.position.z + pos.z);
    }
}

public class GrideTile
{
    public Vector2Int Gridposition;
    public GameObject Sprite;
    public float Altitude;
    public bool IsWalkable;
    public int GCost;
    public int HCost;
    public int FCost;
    

    public GrideTile(Vector2Int gridposition, GameObject sprite)
    {
        Gridposition = gridposition;
        Sprite = sprite;
        Altitude = 0;
    }
}
