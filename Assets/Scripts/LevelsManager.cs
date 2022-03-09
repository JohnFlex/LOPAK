using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public TileBase transparentSprite;

    private void Awake()
    {
        LEVELS_MANAGER_INSTANCE = this;

        //objectsAmount = LEVEL_WIDTH * LEVEL_HEIGHT;

        //poolOfObjects = new List<GameObject>();

        //GameObject tmp;
        //for (int i = 0; i < objectsAmount; i++)
        //{
        //    tmp = Instantiate(objectToPool);
        //    tmp.SetActive(false);
        //    poolOfObjects.Add(tmp);
        //}
        //GenerateLevel(0);

        //sprite1

    }

    public void GenerateLevel(int levelIndex)
    {
        gameMap1.ClearAllTiles();
        gameMap2.ClearAllTiles();
        string levelAsEncoded = RemoveSpecialCharacters(levelsAsEncoded[levelIndex]) ;
        int k = LEVEL_HEIGHT * LEVEL_WIDTH - 1;
        for (int i = 0; i < LEVEL_HEIGHT; i++)
        {
            for (int j = 0; j < LEVEL_WIDTH ; j++)
            {

                //poolOfObjects[k].SetActive(true);
                //poolOfObjects[k].transform.position = new Vector3(j,-i);
                //poolOfObjects[k].GetComponent<SpriteRenderer>().color = levelAsEncoded[k] == '0' ? Color.black : Color.red;
                //{
                //    poolOfObjects[k].GetComponent<ChangeColorOnTrigger>().passStatus = ChangeColorOnTrigger.PassStatus.available;
                //    poolOfObjects[k].GetComponent<Collider2D>().isTrigger = true;
                //}

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
                        
                    }
                }
                
                k--;
            }
        }

        gameMap1.RefreshAllTiles();
        gameMap2.RefreshAllTiles();
    }

    public void UnloadLevel()
    {
        foreach (GameObject item in poolOfObjects)
        {
            GetComponent<ChangeColorOnTrigger>().passStatus = ChangeColorOnTrigger.PassStatus.solid;
            item.GetComponent<SpriteRenderer>().color = Color.black;
            item.SetActive(false);
        }
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
