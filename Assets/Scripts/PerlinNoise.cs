using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    Renderer m_Renderer;

    public int Width = 256;
    public int Height = 256;

    public float Scale = 20.0f;
    public float XOffset = 100.0f;
    public float YOffset = 100.0f;

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        m_Renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(Width, Height);

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Color color = GenerateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    private Color GenerateColor(int x, int y)
    {
        float xCoordinate = (float)x / (float)Width * Scale + XOffset;
        float yCoordinate = (float)y / (float)Height * Scale + YOffset;

        float value = Mathf.PerlinNoise(xCoordinate, yCoordinate);
        return new Color(value, value, value);
    }
}
