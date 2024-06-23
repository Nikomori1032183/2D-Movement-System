using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerActions : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //// when player left clicks
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    //string message = GridInfo.current.groundTilemap.GetTile<SoilTile>(InputHandler.current.GetMouseGridPosition()).GetSoilState().ToString();
        //    //Debug.Log(message);
        //    //Hoe(GridInfo.current.GetGroundTile(InputHandler.current.GetMouseGridPosition()));
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    GridInfo.current.groundTilemap.GetTile<SoilTile>(InputHandler.current.GetMouseGridPosition()).SetSoilState(SoilTile.Soil_States.Tilled);
        //}
    }

    private void Hoe(TileBase tile)
    {
        //tile.
    }

    private void Plant(TileBase tile)
    {

    }

    private void Water(TileBase tile)
    {

    }
}
