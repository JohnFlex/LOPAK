using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

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

    public GameObject levelManager;
    public TextMeshProUGUI ScoreText;


    private void Start()
    {
        lpc = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelPercentageCompletion>();
        //rdr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3Int mapPosition = grid.WorldToCell(transform.position);

        if (gameMap.GetTile(mapPosition) == roadSprite)
        {
            gameMap.SetTile(mapPosition, sprite);
            levelManager.GetComponent<LevelsManager>().nbOfTiles--;
            float nbTiles = levelManager.GetComponent<LevelsManager>().nbOfTiles;
            float total = levelManager.GetComponent<LevelsManager>().totalTiles;
            float percentageTiles = ((total - nbTiles) / total) * 100;

            ScoreText.SetText("Corruption : " + System.Math.Round(percentageTiles,0) + "%");
        }
    }
}
