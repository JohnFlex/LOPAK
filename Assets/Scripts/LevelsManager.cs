using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public List<GameObject> levelArrows = new List<GameObject>();

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
        timeText.SetText("Time : \n" + System.Math.Round(timeRemaining, 0));

        //Conditions de fin de niveau (soit le joueur a complété le niveau, soit le temps est écoulé)
        if ((nbOfTiles <= 0 || timeRemaining <= 0) && isGenerated)
        {
            //Cette variable calcule le pourcentage de cases remplies par le joueur
            int percentage = (int)System.Math.Round(player.GetComponent<ChangeColorOnTrigger>().percentageTiles);

            //On regarde le score du joueur à la fin u niveau

            //On regarde si le score du joueur est son meilleur score ou non
            if ( percentage > scores[levelNumber])
            {
                scores[levelNumber] = percentage;
                scoreTexts[levelNumber].SetText(percentage + "%");
            }

            //On enlève le niveau et on fait apparaitre le menu
            LEVELS_MANAGER_INSTANCE.UnloadLevel();
            ctlt.LoadCorpso();

            //On change la couleur de la flèche menant vers le niveau qui vient d'etre fini en fonction du score du joueur
            if (scores[levelNumber] >= 90)
            {
                levelArrows[levelNumber].GetComponent<Image>().color = new Color32(0, 205, 255, 255);
            }
            else
            {
                if (scores[levelNumber] >= 75)
                {
                    levelArrows[levelNumber].GetComponent<Image>().color = new Color32(255, 255, 0, 255);
                }
                else
                {
                    if (scores[levelNumber] >= 60)
                    {
                        levelArrows[levelNumber].GetComponent<Image>().color = new Color32(125, 255, 215, 255);
                    }
                    else
                    {
                        if (scores[levelNumber] >= 35)
                        {
                            levelArrows[levelNumber].GetComponent<Image>().color = new Color32(185, 75, 3, 255);
                        }
                    }
                }
            }


        }
    }

    //Fonction qui gènère le niveau
    public void GenerateLevel(int levelIndex)
    {
        //On initialise les variables nécessaire au début du niveau
        nbOfTiles = 0;
        timeRemaining = 140;
        levelNumber = levelIndex;

        //On nettoie la carte du jeu avant de la redessiner
        gameMap1.ClearAllTiles();
        gameMap2.ClearAllTiles();

        //On transforme le plan du niveau en chaine de caractere
        string levelAsEncoded = RemoveSpecialCharacters(levelsAsEncoded[levelIndex]) ;

        //On parcours chaque caractère de la chaine et on les interprète comme un type de case qu'on pose à l'endroit correspondant
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

        //On définit le nombre total de case à compléter sur le nombre de case dans le niveau
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

    

    //Enleve les espace et les entrées dans la string représentant le niveau
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
