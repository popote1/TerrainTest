using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSComtroler : MonoBehaviour
{

    public Camera Camera;
    public List<GameObject> Untes;
    public TerrainGridTool TerrainGridTool;
    public GameObject ClickMarquer;
    public bool MouseOnUI;


    private GrideTile _selectTile;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&&!MouseOnUI)
        {
            Vector3 VecPoint;
            RaycastHit hit;
            Ray ray= Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,Mathf.Infinity))
            {

                VecPoint = hit.point;
                Destroy(Instantiate(ClickMarquer,hit.point,Quaternion.identity),1);
                if (_selectTile != null)
                {
                    _selectTile.Sprite.GetComponent<SpriteRenderer>().color = Color.green;
                }

                if (TerrainGridTool.TerrainTiles[TerrainGridTool.GetXPos(VecPoint), TerrainGridTool.GetYPos(VecPoint)]
                    .IsWalkable)
                {
                    _selectTile = TerrainGridTool.TerrainTiles[TerrainGridTool.GetXPos(VecPoint),
                        TerrainGridTool.GetYPos(VecPoint)];
                    _selectTile.Sprite.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else
                {
                    _selectTile = null;
                }

            }
            
        }
    }

    public void SetMouseOnUI(bool mouseOnUi)
    {
        MouseOnUI = mouseOnUi;
    }
}
