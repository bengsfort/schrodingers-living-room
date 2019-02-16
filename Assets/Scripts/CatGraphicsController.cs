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

	public Texture2D walkTexture;
	public float walkAnimSpeed = 1f;
	public Texture2D scratchTexture;
	public float scratchAnimSpeed = 1f;

	private Material cachedMaterial;
	private SpriteRenderer cachedRenderer;

    // Start is called before the first frame update
    void Start()
    {
		cachedRenderer = GetComponent<SpriteRenderer>();
		if (cachedRenderer != null) {
			cachedMaterial = cachedRenderer.material;
			cachedRenderer.material.shader = Shader.Find("Sch/Characters/Cat");
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (action == CatActions.Standing)
		{
			cachedMaterial.SetTexture("_MainTexture", walkTexture);
			cachedMaterial.SetFloat("_Frame", 0);
		}
		else if (action == CatActions.Walking)
		{
			cachedMaterial.SetTexture("_MainTexture", walkTexture);
			cachedMaterial.SetFloat("_Frame", Mathf.Floor((Time.time * walkAnimSpeed) % 4));
		}
		else if (action == CatActions.Scratching)
		{
			cachedMaterial.SetTexture("_MainTexture", scratchTexture);
			cachedMaterial.SetFloat("_Frame", Mathf.Floor((Time.time * scratchAnimSpeed) % 4));
		}

		if (direction == CatDirections.Left)
		{
			cachedRenderer.flipX = false;
		}
		else {
			cachedRenderer.flipX = true;
		}
	}
}
