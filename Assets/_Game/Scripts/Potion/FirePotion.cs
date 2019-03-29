using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : MonoBehaviour
{
    private GameManager gameManager;
    private string abilityName;
    private void Awake()
    {
    }


    void Start()
    {

        gameManager = FindObjectOfType<GameManager>(); 
        abilityName = "Fire";
        
            

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.UnlockAbility(abilityName);
        Destroy(gameObject);

    }
}
