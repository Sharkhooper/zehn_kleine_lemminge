using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestoryableTilemap : MonoBehaviour,IKillTarget
{
    private Tilemap tilemap;
    // Start is called before the first frame update
    
    
    public void Die(GameObject other)
    {
        Debug.Log("test");
        
        
        //TODO
        
        tilemap = other.gameObject.GetComponent<Tilemap>();

        Vector3 collision2D = other.gameObject.transform.position;
       // Vector3 collision = new Vector3(collision2D.x, collision2D.y, 0f);
        
        Vector3Int tilePos = tilemap.WorldToCell(collision2D);
        tilemap.SetTile(tilePos, null);

        Debug.Log(collision2D);
    }
}
