using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGraphicsController : MonoBehaviour
{

	public enum FurnitureQuantumStates {
		Live,
		Dead,
		Superpositioned
	}

	public FurnitureQuantumStates quantumState = FurnitureQuantumStates.Live;

	public enum CatActions {
		Standing,
		Walking,
		Scratching
	}

	private Material cachedMaterial;
	private SpriteRenderer cachedRenderer;

    // Start is called before the first frame update
    void Start()
    {
		cachedRenderer = GetComponent<SpriteRenderer>();
		if (cachedRenderer != null) {
			cachedMaterial = cachedRenderer.material;
			cachedMaterial.shader = Shader.Find("Sch/Furniture/Furniture");
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (quantumState == FurnitureQuantumStates.Live)
		{
			cachedMaterial.SetFloat("_State", 1);
		}
		else if (quantumState == FurnitureQuantumStates.Dead)
		{
			cachedMaterial.SetFloat("_State", -1);
		}
		else if (quantumState == FurnitureQuantumStates.Superpositioned)
		{
			cachedMaterial.SetFloat("_State", 0f);
		}

	}
}
