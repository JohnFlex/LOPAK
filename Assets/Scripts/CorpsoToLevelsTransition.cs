using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsoToLevelsTransition : MonoBehaviour
{

    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameObject corpsoCanvas;


    //On intialise divers type de variable au moment o� le niveau se chargera
    public void LoadLevel(int levelID)
    {
        FadeOut.FADE_OUT_ELEMENT.FadeToBlack(1f);
        gameManager.SetActive(true);
        gameManager.GetComponent<LevelsManager>().GenerateLevel(levelID);
        corpsoCanvas.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.position = gameManager.transform.position;
        cam.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        cam.transform.position =cam.transform.parent.position;
        FadeOut.FADE_OUT_ELEMENT.FadeFromBlack(1f);
        cam.transform.localPosition = new Vector3(0, 0, -10);
    }

    //On initialise certaines variables au moment o� le corps se g�n�re
    public void LoadCorpso()
    {
        FadeOut.FADE_OUT_ELEMENT.FadeToBlack(1f);
        cam.transform.SetParent(null);
        gameManager.SetActive(false);
        corpsoCanvas.SetActive(true);
        FadeOut.FADE_OUT_ELEMENT.FadeFromBlack(1f);
    }
    
}
