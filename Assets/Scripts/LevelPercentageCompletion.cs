using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPercentageCompletion : MonoBehaviour
{
    List<ChangeColorOnTrigger> colorsOnTriggerAvailable;

    [SerializeField]
    CorpsoToLevelsTransition ctlt;

    void Start()
    {
        
        colorsOnTriggerAvailable = new List<ChangeColorOnTrigger>();
        foreach (ChangeColorOnTrigger item in FindObjectsOfType<ChangeColorOnTrigger>())
        {
            if (item.passStatus == ChangeColorOnTrigger.PassStatus.available)
            {
                colorsOnTriggerAvailable.Add(item);
                
            }
        }
    }

    public void UpdateHolder(ChangeColorOnTrigger colorOnTrigger)
    {
        colorsOnTriggerAvailable.Remove(colorOnTrigger);
        if (colorsOnTriggerAvailable.Count < 1)
        {
            LevelsManager.LEVELS_MANAGER_INSTANCE.UnloadLevel();
            ctlt.LoadCorpso();
        }
    }
}
