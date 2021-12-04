using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseTexture : MonoBehaviour
{
    public int pixWidth;
    public int pixHeight;

    public float xOrg;
    public float yOrg;

    float scale = 3f;

    float multiplier = 1;
    public float power = 1;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
    }

    void CalcNoise()
    {
        float y = 0f;

        while (y < noiseTex.height)
        {
            float x = 0f;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord) * multiplier;//ReturnStepValue(Mathf.Pow(Mathf.PerlinNoise(xCoord, yCoord) * multiplier, power));
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    void Update()
    {
        CalcNoise();

        xOrg += Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        yOrg += Input.GetAxisRaw("Vertical") * Time.deltaTime;
    }

    float ReturnStepValue(float a)
    {
        return Mathf.Floor(a * 10) / 10f;
    }

    public float ReturnNoiseMapValue(int a)
    {
        return pix[a].r;
    }
}
