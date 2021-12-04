using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraiGenerator : MonoBehaviour
{

    public int size = 51;

    int num = 51;

    public float scale = 5f;

    public GameObject cube;

    public List<GameObject> terrain;

    public GameObject noiseGen;

    public float multiplier;

    void Start()
    {

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                GameObject temp = Instantiate(cube, new Vector3(i * scale, 0, j * scale), Quaternion.identity);
                temp.transform.localScale = new Vector3(scale, scale, scale);
                terrain.Add(temp);
            }
        }
    }

    void Update()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                float value = noiseGen.GetComponent<NoiseTexture>().ReturnNoiseMapValue(i + num * j);
                terrain[i + size * j].transform.position = new Vector3(i * scale, value * multiplier, j * scale);
            }
        }
    }
}
