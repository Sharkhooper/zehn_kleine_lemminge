using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DeadlyTilemap : MonoBehaviour
{
	private Tilemap deadlyMap;
	private Tilemap groundMap;
	private Tile unDeadlyTile;

	private void Start()
	{
		unDeadlyTile = Resources.Load<Tile>("UnDeadly");
		deadlyMap = GetComponent<Tilemap>();
		groundMap = transform.parent.GetChild(0).GetComponent<Tilemap>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.transform.tag);

		if (other.transform.CompareTag("Player"))
		{
			Vector2 pos2D = other.GetContact(0).point;
			Vector3 pos = new Vector3(pos2D.x, pos2D.y, 0f);

			Vector3Int posOnDeadly = deadlyMap.WorldToCell(pos);
			Matrix4x4 tempMatrix = deadlyMap.GetTransformMatrix(posOnDeadly);
			deadlyMap.SetTile(posOnDeadly, null);

			Vector3Int posOnGround = groundMap.WorldToCell(pos);
			unDeadlyTile.transform = tempMatrix;
			groundMap.SetTile(posOnGround, unDeadlyTile);
		}
		// Last minute fix
		else if (other.transform.parent.parent.parent.name.Equals("DeadBlock"))
		{
			Vector2 pos2D = other.GetContact(0).point;
			Vector3 pos = new Vector3(pos2D.x, pos2D.y, 0f);

			Vector3Int posOnDeadly = deadlyMap.WorldToCell(pos);
			deadlyMap.SetTile(posOnDeadly, null);
		}

		if (other.transform.CompareTag("Player") || other.transform.CompareTag("Group"))
		{
			ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die(gameObject));
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Group"))
		{
			ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die(gameObject));
		}
	}
}
