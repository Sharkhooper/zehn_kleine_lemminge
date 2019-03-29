using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestoryableTilemap : MonoBehaviour,IKillTarget
{
    private Tilemap tilemap;
    // Start is called before the first frame update
   // private Vector2 vectorObject= new Vector2(1,1);
    
    public void Die(GameObject other)
    {
        
        
        tilemap = gameObject.GetComponent<Tilemap>();

        
        
        
        //gameObject.GetComponent<Collider2D>().OverlapCollider()
        
        //collid.GetContacts(Collision.other)

       // Vector3 collision2D = new Vector3(vectorObject.x,vectorObject.y, 0);
       // Vector3 collision = new Vector3(collision2D.x, collision2D.y, 0f);
       Vector3 collision2D = other.gameObject.transform.position;
       Vector3 additionalY = Vector3.up * other.transform.localScale.y/2;
       Vector3 additionalX = Vector3.right * other.transform.localScale.x/2;

      // Debug.Log(additionalY + collision2D);
         
       
        Vector3Int tilePos1 = tilemap.WorldToCell(collision2D-additionalY);
        Vector3Int tilePos2 = tilemap.WorldToCell(collision2D+additionalY);
        Vector3Int tilePos3 = tilemap.WorldToCell(collision2D+additionalX);
        Vector3Int tilePos4 = tilemap.WorldToCell(collision2D-additionalX);
        tilemap.SetTile(tilePos1 , null);
        tilemap.SetTile(tilePos2, null);
        tilemap.SetTile(tilePos3, null);
        tilemap.SetTile(tilePos4, null);

        //Debug.Log(collision2D);
        //Debug.DrawLine(collision2D-additionalY,collision2D+additionalY,Color.red,10);
        //Debug.DrawLine(collision2D-additionalX,collision2D+additionalX,Color.blue,10);
    }

}
