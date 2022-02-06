using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZS.Tools;
public class NoiseTerrain : MonoBehaviour
{
	private Terrain terrain;
	private Transform transform;
	private TerrainCollider terrainCollider;

	public float heightScale = 0.05f;
	public float perlinScale = 10f;

	private float yPos;
	// public GameObject terrainObject;
	// public GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		terrain = GetComponent<Terrain>();
		transform = GetComponent<Transform>();
		terrainCollider = GetComponent<TerrainCollider>();
		// log the positions of the player and the terrain
		// Debug.Log("Player position: " + player.transform.position);
		// Debug.Log("Terrain position: " + terrain.transform.position);
		// log the width and depth of the terrain]
		// Debug.Log("Terrain width: " + terrain.terrainData.size.x);
		// Debug.Log("Terrain height: " + terrain.terrainData.size.y);
		// Debug.Log("Terrain depth: " + terrain.terrainData.size.z);
		// log the height map resolution
		// Debug.Log("Terrain height map resolution: " + terrain.terrainData.heightmapResolution);
		yPos = -1 * (1 - heightScale) * terrain.terrainData.size.y * ((transform.position.z + terrain.terrainData.size.z) / terrain.terrainData.size.z);
		terrain.terrainData = GenerateTerrain();
		terrainCollider.terrainData = terrain.terrainData;
		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
		Debug.Log("Started");
	}

	// Update is called once per frame
	void Update()
	{
		// terrain.terrainData = GenerateTerrain();
	}

	TerrainData GenerateTerrain()
	{
		// TerrainData existingTerrain = terrain.terrainData;
		// TerrainData existingTerrain = new TerrainData();
		// existingTerrain.size = new Vector3(terrain.terrainData.size.x, terrain.terrainData.size.y, terrain.terrainData.size.z);
		// existingTerrain.heightmapResolution = terrain.terrainData.heightmapResolution;
		TerrainData existingTerrain = TerrainDataCloner.Clone(terrain.terrainData);
		existingTerrain.SetHeights(0, 0,
			GenerateTerrainHeights(existingTerrain.heightmapResolution,
				existingTerrain.heightmapResolution,
				terrain.transform.position.x,
				terrain.transform.position.z
			)
		);
		// Debug.Log("TerrainHeight at 0: " + terrain.terrainData.GetHeight(0, 0));
		return existingTerrain;
	}

	float[,] GenerateTerrainHeights(int width, int depth, float widthOffset, float depthOffset)
	{
		float maxHeight = 0;
		float minHeight = 1;
		float[,] heights = new float[width, depth];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < depth; y++)
			{
				float perlinX = ((float)x / (width - 1) * width + (widthOffset / terrain.terrainData.size.x * terrain.terrainData.heightmapResolution)) / (width) * perlinScale;
				float perlinY = ((float)y / (depth - 1) * depth + (depthOffset / terrain.terrainData.size.z * terrain.terrainData.heightmapResolution)) / (depth) * perlinScale;
				float height = (Mathf.Clamp(Mathf.PerlinNoise(perlinX, perlinY), 0, 1) * heightScale + ((float)((depth - 1) - y) / (depth - 1)) * (1 - heightScale));
				// if (x == 0 && y == 0)
				// {
				// 	Debug.Log("End Height: " + (height * 800 + yPos));
				// 	Debug.Log("End Perlin X: " + perlinX);
				// 	Debug.Log("End Perlin Y: " + perlinY);
				// }
				// if (x == 0 && y == depth - 1)
				// {
				// 	Debug.Log("Start Height: " + (height * 800 + yPos));
				// 	Debug.Log("Start Perlin X: " + perlinX);
				// 	Debug.Log("Start Perlin Y: " + perlinY);
				// }
				heights[y, x] = height;
				// heights[x, y] = 1;
				if (height > maxHeight)
				{
					maxHeight = heights[y, x];
				}
				if (height < minHeight)
				{
					minHeight = heights[y, x];
				}
			}
		}
		Debug.Log("MaxHeight: " + maxHeight + " MinHeight: " + minHeight);
		return heights;
	}

}
