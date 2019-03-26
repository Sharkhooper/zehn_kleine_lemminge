using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPotion : MonoBehaviour
{
    private GameManager inventory;
    private string name;
    private void Awake()
    {
       inventory = FindObjectOfType<GameManager>(); 
    }


    void Start()
    {

        name = "SuperJump";
        
            

    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        inventory.UnlockAbility(name);
        Destroy(gameObject);

    }
}
