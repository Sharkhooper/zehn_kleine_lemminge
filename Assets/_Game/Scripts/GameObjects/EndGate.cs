using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    // Start is called before the first frame update

    private GameManager gameManagerScript;
    private void Awake()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Group"))
        {

            Debug.Log("Testus out if");
            
            if (!(gameManagerScript.existSingleLemming))
            {



                Debug.Log("Testus in if");
                gameManagerScript.LoadNextLevel();
            }
        }
    }
}
