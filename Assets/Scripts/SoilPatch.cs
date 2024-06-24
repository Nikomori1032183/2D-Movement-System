using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class SoilPatch : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer plantRenderer;

    public List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> plantSprites = new List<Sprite>();

    public int state = 0;
    public int plantState = -1;

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
        if (FarmingPrototype.current.currentTool == FarmingPrototype.Tool.Hoe)
        {
            Hoe();
        }

        else if (FarmingPrototype.current.currentTool == FarmingPrototype.Tool.Watering_Can)
        {
            Water();
        }

        else if (FarmingPrototype.current.currentTool == FarmingPrototype.Tool.Plant_Seeds)
        {
            Plant();
        }
    }

    [Button]
    public void Hoe()
    {
        if (state == 0)
        {
            state = 1;
        }

        UpdateSprite();
    }

    [Button]
    public void Water()
    {
        if (state == 1)
        {
            state = 2;
        }

        UpdateSprite();
    }

    [Button]
    public void Plant()
    {
        if (plantState == -1 && state > 0)
        {
            plantState = 0;
        }

        UpdateSprite();
    }

    [Button]
    public void Grow()
    {
        if (plantState > -1 && plantState < 5 && state > 1)
        {
            plantState++;
        }

        UpdateSprite();
    }

    public void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[state];

        if (plantState > -1)
        {
            plantRenderer.sprite = plantSprites[plantState];
        }
    }
}