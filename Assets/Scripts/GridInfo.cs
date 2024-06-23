using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInfo : MonoBehaviour
{
    public static GridInfo current;

    public Grid grid;
    public Tilemap groundTilemap;

    public Dictionary<Vector3Int, CellInfo> gridInfo = new Dictionary<Vector3Int, CellInfo>();

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        grid = GetComponent<Grid>();
        groundTilemap = GetComponentsInChildren<Tilemap>()[0];

        //gridInfo = new TileInfo[grid.,0,0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public TileInfo GetTileInfo(Tilemap tilemap, Vector3Int position)
    //{
    //    return gridInfo[position.x, position.y, TilemapToIndex(tilemap)];
    //}

    //public void SetTileInfo(Tilemap tilemap, TileInfo tileInfo, Vector3Int position)
    //{
    //    gridInfo[position.x, position.y, TilemapToIndex(tilemap)] = tileInfo;
    //    tilemap.SetTile(position, tileInfo.tile);
    //}

    private int TilemapToIndex(Tilemap tilemap)
    {
        if (tilemap == groundTilemap)
        {
            return 0;
        }

        else
        {
            return 1;
        }
    }
}
