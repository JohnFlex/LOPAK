using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    [TextArea(13, 15)]
    string[] levelsAsEncoded;

    public static LevelsManager LEVELS_MANAGER_INSTANCE;
    List<GameObject> poolOfObjects;
    public GameObject objectToPool;
    int objectsAmount;

    const int LEVEL_HEIGHT = 38;
    const int LEVEL_WIDTH = 32;

    public Tilemap gameMap1;
    public TileBase sprite1;

    public Tilemap gameMap2;
    public TileBase sprite2;

    public int nbOfTiles = 0;
    public int totalTiles;
    private bool isGenerated = false;

    [SerializeField]
    CorpsoToLevelsTransition ctlt;

    private float timeRemaining;
    public TextMeshProUGUI timeText;

    private int[] scores = new int[6];
    int levelNumber;
    public GameObject player;
    public List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>();

    private void Awake()
    {
        LEVELS_MANAGER_INSTANCE = this;
        for (int i = 0; i <6; i++)
        {
            scores[i] = 0;
        }


    }

    private void Update()
    {
        //TIMER
        timeRemaining -= Time.deltaTime;
        timeText.SetText("Time : " + System.Math.Round(timeRemaining, 0));

        if ((nbOfTiles <= 0 || timeRemaining <= 0) && isGenerated)
        {
            int percentage = (int)System.Math.Round(player.GetComponent<ChangeColorOnTrigger>().percentageTiles);
            if ( percentage > scores[levelNumber])
            {
                scores[levelNumber] = percentage;
                scoreTexts[levelNumber].SetText(percentage + "%");
            }
            LEVELS_MANAGER_INSTANCE.UnloadLevel();
            ctlt.LoadCorpso();

            
        }
    }

    public void GenerateLevel(int levelIndex)
    {
        nbOfTiles = 0;
        timeRemaining = 140;
        levelNumber = levelIndex;

        gameMap1.ClearAllTiles();
        gameMap2.ClearAllTiles();
        string levelAsEncoded = RemoveSpecialCharacters(levelsAsEncoded[levelIndex]) ;
        int k = LEVEL_HEIGHT * LEVEL_WIDTH - 1;
        for (int i = 0; i < LEVEL_HEIGHT; i++)
        {
            for (int j = 0; j < LEVEL_WIDTH ; j++)
            {
                Vector3Int position = new Vector3Int(-j, i, 0);
                if (levelAsEncoded[k] == '0')
                {
                    
                    gameMap1.SetTile(position, sprite1);
                    
                    
                }
                else
                {
                    if (levelAsEncoded[k] == '1')
                    {
                        gameMap2.SetTile(position, sprite2);
                        nbOfTiles++;
                        
                    }
                }
                
                k--;
            }
        }
        isGenerated = true;
        gameMap1.RefreshAllTiles();
        gameMap2.RefreshAllTiles();
        totalTiles = nbOfTiles;
    }

    public void UnloadLevel()
    {
       /* foreach (GameObject item in poolOfObjects)
        {
            *//*GetComponent<ChangeColorOnTrigger>().passStatus = ChangeColorOnTrigger.PassStatus.solid;
            item.GetComponent<SpriteRenderer>().color = Color.black;
            item.SetActive(false);*//*
        }*/


        ctlt.LoadCorpso();
    }

    

    public static string RemoveSpecialCharacters(string str)
    {
        char[] buffer = new char[str.Length];
        int idx = 0;

        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9'))
            {
                buffer[idx] = c;
                idx++;
            }
        }

        return new string(buffer, 0, idx);
    }
}
