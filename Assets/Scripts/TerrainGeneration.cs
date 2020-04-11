using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGeneration : MonoBehaviour
{
    Terrain m_Terrain;

    public int Width = 256;
    public int Height = 256;
    public int Depth = 20;

    public float NoiseSampleScale = 20.0f;
    public float NoiseXOffset = 100f;
    public float NoiseYOffset = 100f;

    private void Awake()
    {
        m_Terrain = GetComponent<Terrain>();
    }

    private void Update()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        m_Terrain.terrainData.heightmapResolution = Width + 1;
        m_Terrain.terrainData.size = new Vector3(Width, Depth, Height);
        m_Terrain.terrainData.SetHeights(0, 0, GenerateHeights());
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                heights[x, y] = GenerateHeight(x, y);
            }
        }

        return heights;
    }

    private float GenerateHeight(int x, int y)
    {
        float xCoordinate = (float)x / (float)Width * NoiseSampleScale + NoiseXOffset;
        float yCoordinate = (float)y / (float)Height * NoiseSampleScale + NoiseYOffset;

        float value = Mathf.PerlinNoise(xCoordinate, yCoordinate);
        return value;
    }
}
