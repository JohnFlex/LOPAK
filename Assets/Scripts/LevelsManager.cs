using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        LEVELS_MANAGER_INSTANCE = this;

        objectsAmount = LEVEL_WIDTH * LEVEL_HEIGHT;

        poolOfObjects = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < objectsAmount; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            poolOfObjects.Add(tmp);
        }
        GenerateLevel(0);

    }

    public void GenerateLevel(int levelIndex)
    {
        Debug.Log(objectsAmount);
        string levelAsEncoded = RemoveSpecialCharacters(levelsAsEncoded[levelIndex]) ;
        int k = 0;
        for (int i = 0; i < LEVEL_HEIGHT; i++)
        {
            for (int j = 0; j < LEVEL_WIDTH ; j++)
            {
                
                poolOfObjects[k].SetActive(true);
                poolOfObjects[k].transform.position = new Vector3(j,-i);
                poolOfObjects[k].GetComponent<SpriteRenderer>().color = levelAsEncoded[k] == '0' ? Color.black : Color.blue;
                if (levelAsEncoded[k] == '1')
                {
                    poolOfObjects[k].GetComponent<ChangeColorOnTrigger>().passStatus = ChangeColorOnTrigger.PassStatus.available;
                    poolOfObjects[k].GetComponent<Collider2D>().isTrigger = true;
                }
                k++;
            }
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
