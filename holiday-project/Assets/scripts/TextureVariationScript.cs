using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureVariationScript : MonoBehaviour
{
    public List<MeshRenderer> renderers;
    [SerializeField] private Material[] textures;

    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            renderers.Add(child.GetComponent<MeshRenderer>());
        }

        GenerateTiles();
    }

    private void Update()
    {
        //debug thingy comment this whole bitch out if unused
        //P stands for Pandomize
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateTiles();
        }
    }

    private void GenerateTiles()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            int chosenTex = Random.Range(0, textures.Length);
            renderers[i].material = textures[chosenTex];
        }
    }
}
