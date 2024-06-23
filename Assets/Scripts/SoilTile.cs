using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SoilTile : TileBase
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite tilledSprite;
    [SerializeField] private Sprite wateredSprite;

    public enum Soil_States
    {
        Normal, Tilled, Watered
    };

    private Soil_States soilState;

    public Soil_States GetSoilState()
    {
        Debug.Log("Brah");
        return soilState;
    }

    public void SetSoilState(Soil_States state)
    {
        soilState = state;
    }

    private Sprite GetSoilSprite()
    {
        switch (soilState)
        {
            case Soil_States.Normal:
                return normalSprite;

            case Soil_States.Tilled:
                return tilledSprite;

            case Soil_States.Watered:
                return wateredSprite;

            default:
                return null;
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        UpdateTile(ref tileData);
    }
 
    private void UpdateTile(ref TileData tileData)
    {
        tileData.transform = Matrix4x4.identity;
        tileData.color = Color.white;
        tileData.sprite = GetSoilSprite();
        tileData.flags = TileFlags.LockTransform | TileFlags.LockColor;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }

    [MenuItem("Assets/Create/SoilTile")]
    public static void CreateSoilTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Soil Tile", "New Soil Tile", "Asset", "Save Soil Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SoilTile>(), path);
    }
}
