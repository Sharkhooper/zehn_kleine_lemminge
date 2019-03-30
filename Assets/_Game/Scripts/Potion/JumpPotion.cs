using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPotion : MonoBehaviour
{
   // private GameManager gameManager;
    private string abilityName;
   
    private GameManager gameManagerScript;

    void Start()
    {

       // gameManager = FindObjectOfType<GameManager>(); 
        abilityName = "SuperJump";
        
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>(); 

    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManagerScript.UnlockAbility(abilityName);
        gameManagerScript.JumpButtonEnabled();
        Destroy(gameObject);

    }
}
