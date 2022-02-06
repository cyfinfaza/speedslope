using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileManager : MonoBehaviour
{

	public GameObject tilePrefab;
	public GameObject player;

	private Dictionary<Vector2, GameObject> tileDictionary = new Dictionary<Vector2, GameObject>();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		int playerX = Mathf.FloorToInt(player.transform.position.x / 1000);
		int playerZ = Mathf.FloorToInt(player.transform.position.z / 1000);
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
				GameObject newTile = Instantiate(tilePrefab, new Vector3(tile.x * 1000, 0, tile.y * 1000), Quaternion.identity);
				tileDictionary.Add(tile, newTile);
			}
		}
		foreach (Vector2 tile in tileDictionary.Keys)
		{
			if (!tilesNeeded.Contains(tile))
			{
				Destroy(tileDictionary[tile]);
				tileDictionary.Remove(tile);
			}
		}
	}

}
