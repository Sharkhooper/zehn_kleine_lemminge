using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : MonoBehaviour
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

        string name = "Fire";
        
            

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        inventory.UnlockAbility(name);

    }
}
