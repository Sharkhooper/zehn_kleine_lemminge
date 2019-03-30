using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : MonoBehaviour
{
   // private GameManager gameManager;
    private string abilityName;
   
    private GameManager gameManagerScript;

   
       
    

    void Start()
    {

     //   gameManager = FindObjectOfType<GameManager>(); 
        abilityName = "Fire";
        
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManagerScript.UnlockAbility(abilityName);
        gameManagerScript.FireButtonEnabled();
        Destroy(gameObject);

    }
}
