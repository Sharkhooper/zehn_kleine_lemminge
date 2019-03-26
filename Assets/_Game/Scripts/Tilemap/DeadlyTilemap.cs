using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeadlyTilemapController : MonoBehaviour, ICollidable
{
	private Tilemap deadlyMap;
	private Tilemap groundMap;

    // Start is called before the first frame update
    void Start()
    {
	    groundMap = GameObject.FindGameObjectWithTag("ground").GetComponent<Tilemap>();
	    deadlyMap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionWithPlayer(GameObject player)
    {
	    // TODO: Kill player
	    // TODO: Trigger death animation
	    // TODO: Change camera focus

	    Vector3 playerPos = player.transform.position;
	    // Remove deadly tile
	    Vector3Int posOnDeadly= deadlyMap.WorldToCell(playerPos);
	    deadlyMap.SetTile(posOnDeadly, null);

	    // TODO: Instantiate not deadly tile
	    Vector3Int posOnGround = groundMap.WorldToCell(playerPos);

    }

    public void OnCollisionWithGroup()
    {
	    throw new System.NotImplementedException();
    }
}
