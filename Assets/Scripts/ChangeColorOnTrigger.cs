using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeColorOnTrigger : MonoBehaviour
{
    public enum PassStatus { solid, available, passed};

    SpriteRenderer rdr;

    public PassStatus passStatus = PassStatus.solid;

    LevelPercentageCompletion lpc;

    public Tilemap gameMap;
    public Grid grid;
    public TileBase sprite;
    public TileBase roadSprite;




    private void Start()
    {
        lpc = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelPercentageCompletion>();
        //rdr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.name == gameMap.gameObject.name)
        {
            BoundsInt tilemapBounds = gameMap.cellBounds;
            for (int x = 0; x < tilemapBounds.size.x; x++)
            {
                for (int y = 0; y < tilemapBounds.size.y; y++)
                {
                    UnityEngine.Tilemaps.Tile t = gameMap.GetTile<UnityEngine.Tilemaps.Tile>(new Vector3Int(x, y, 0));
                    if (t != null)
                    {
                        Debug.Log(t.name + ", " + t.colliderType);
                    }
                }
            }
        }
    }*/

    private void Update()
    {
        //Debug.Log(gameMap.WorldToCell(transform.position));
        //Debug.Log(grid.WorldToCell(transform.position));

        
        Vector3Int mapPosition = grid.WorldToCell(transform.position);


        /*for (int i = -1; i<=1; i++)
        {
            for (int j = -1; j<=1; j++)
            {
                Vector3Int currentPosition = new Vector3Int(mapPosition.x + i, mapPosition.y + j, 0);
                if (gameMap.GetTile(currentPosition) == roadSprite)
                {
                    gameMap.SetTile(currentPosition, sprite);
                }
            }
        }*/

        if (gameMap.GetTile(mapPosition) == roadSprite)
        {
            gameMap.SetTile(mapPosition, sprite);
        }


        //gameMap.SetColor(mapPosition, Color.yellow);
        //gameMap.GetTile(mapPosition).
    }
}
