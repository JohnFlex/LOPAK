using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnTrigger : MonoBehaviour
{
    public enum PassStatus { solid, available, passed};

    SpriteRenderer rdr;

    public PassStatus passStatus = PassStatus.solid;

    LevelPercentageCompletion lpc;


    private void Start()
    {
        lpc = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelPercentageCompletion>();
        rdr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && passStatus == PassStatus.available)
        {
            passStatus = PassStatus.passed;
            rdr.color = Color.green;
            lpc.UpdateHolder(this);
        }
    }
}
