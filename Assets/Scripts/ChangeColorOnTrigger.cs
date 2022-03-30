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

    public float percentageTiles;


    private void Start()
    {
        lpc = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelPercentageCompletion>();
        //rdr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //On Calcule la position du joueur sur la grid
        Vector3Int mapPosition = grid.WorldToCell(transform.position);

        //Si le joueur tombe sur une case qui n'a pas encore été infectée
        if (gameMap.GetTile(mapPosition) == roadSprite)
        {
            //On change son apparence pour une apparence de case infectée
            gameMap.SetTile(mapPosition, sprite);

            //On diminue le nombre de case qui doivent encore êtres remplies
            levelManager.GetComponent<LevelsManager>().nbOfTiles--;

            //On récupere les valeurs du nombre de case totale et à remplir afin de calculer le pourcentage restant
            float nbTiles = levelManager.GetComponent<LevelsManager>().nbOfTiles;
            float total = levelManager.GetComponent<LevelsManager>().totalTiles;
            percentageTiles = ((total - nbTiles) / total) * 100;

            //On affiche ce nouveau score sur l'interface
            ScoreText.SetText("Corruption : " + System.Math.Round(percentageTiles,0) + "%");
        }
    }
}
