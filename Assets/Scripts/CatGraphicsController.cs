using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGraphicsController : MonoBehaviour
{

	public enum CatDirections {
		Left,
		Right
	}

	public CatDirections direction = CatDirections.Left;

	public enum CatActions {
		Standing,
		Walking,
		Scratching
	}

	public CatActions action = CatActions.Standing;

	private Material cachedMaterial;

    // Start is called before the first frame update
    void Start()
    {
		Renderer r = GetComponent<Renderer>();
		if (r != null) {
			cachedMaterial = r.material;
			r.material.shader = Shader.Find("Sch/Characters/Cat");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
