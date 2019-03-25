using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerPotion : MonoBehaviour
{
    private GameManager inventory;
    private string name;
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("GameManager");
        inventory =player.GetComponent<GameManager>();
    }


    void Start()
    {

        string name = "Power";
        
            

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        inventory.UnlockAbility(name);

    }
}
