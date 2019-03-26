using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : MonoBehaviour
{
    private GameManager gameManager;
    private string name;
    private void Awake()
    {
    }


    void Start()
    {

        gameManager = FindObjectOfType<GameManager>(); 
        name = "Fire";
        
            

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.UnlockAbility(name);
        Destroy(gameObject);

    }
}
