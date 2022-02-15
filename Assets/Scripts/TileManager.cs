using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileManager : MonoBehaviour
{

	public GameObject tilePrefab;
	public GameObject player;
	public float tileSize = 1000;

	private Dictionary<Vector2, GameObject> tileDictionary = new Dictionary<Vector2, GameObject>();

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(tilesLoop());
	}

	// Update is called once per frame
	void Update()
	{
		// int playerX = Mathf.FloorToInt(player.transform.position.x / tileSize);
		// int playerZ = Mathf.FloorToInt(player.transform.position.z / tileSize);
		// HashSet<Vector2> tilesNeeded = new HashSet<Vector2>();
		// for (int x = playerX - 1; x <= playerX + 1; x++)
		// {
		// 	for (int z = playerZ - 1; z <= playerZ + 1; z++)
		// 	{
		// 		tilesNeeded.Add(new Vector2(x, z));
		// 	}
		// }
		// foreach (Vector2 tile in tilesNeeded)
		// {
		// 	if (!tileDictionary.ContainsKey(tile))
		// 	{
		// 		GameObject newTile = Instantiate(tilePrefab, new Vector3(tile.x * tileSize, 0, tile.y * tileSize), Quaternion.identity);
		// 		tileDictionary.Add(tile, newTile);
		// 	}
		// }
		// HashSet<Vector3> tilesToRemove = new HashSet<Vector3>();
		// foreach (Vector2 tile in tileDictionary.Keys)
		// {
		// 	if (!tilesNeeded.Contains(tile))
		// 	{
		// 		Destroy(tileDictionary[tile]);
		// 		tilesToRemove.Add(tile);
		// 	}
		// }
		// foreach (Vector3 tile in tilesToRemove)
		// {
		// 	tileDictionary.Remove(tile);
		// }
	}

	IEnumerator tilesLoop()
	{
		while (true)
		{
			int playerX = Mathf.FloorToInt(player.transform.position.x / tileSize);
			int playerZ = Mathf.FloorToInt(player.transform.position.z / tileSize);
			HashSet<Vector2> tilesNeeded = new HashSet<Vector2>();
			for (int x = playerX - 1; x <= playerX + 1; x++)
			{
				for (int z = playerZ - 1; z <= playerZ + 1; z++)
				{
					tilesNeeded.Add(new Vector2(x, z));
				}
			}
			foreach (Vector2 tile in tilesNeeded)
			{
				if (!tileDictionary.ContainsKey(tile))
				{
					GameObject newTile = Instantiate(tilePrefab, new Vector3(tile.x * tileSize, 0, tile.y * tileSize), Quaternion.identity);
					tileDictionary.Add(tile, newTile);
					yield return null;
				}
			}
			HashSet<Vector3> tilesToRemove = new HashSet<Vector3>();
			foreach (Vector2 tile in tileDictionary.Keys)
			{
				if (!tilesNeeded.Contains(tile))
				{
					Destroy(tileDictionary[tile]);
					tilesToRemove.Add(tile);
					yield return null;
				}
			}
			foreach (Vector3 tile in tilesToRemove)
			{
				tileDictionary.Remove(tile);
			}
			yield return null;
		}
	}

}
