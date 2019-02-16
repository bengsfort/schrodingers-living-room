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

	public Sprite walkSprite;
	public float walkAnimSpeed = 1f;
	public Sprite scratchSprite;
	public float scratchAnimSpeed = 1f;

	private Material cachedMaterial;
	private SpriteRenderer cachedRenderer;

	public void ChangeDirectionLeft() {
		if (direction == CatDirections.Right) {
			direction =  CatDirections.Left;
		}
	}
	public void ChangeDirectionRight() {
		if (direction == CatDirections.Left) {
			direction =  CatDirections.Right;
		}
	}

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
			cachedRenderer.sprite = walkSprite;
			cachedMaterial.SetVector("_UVXMod", new Vector2(.25f, 0f));
		}
		else if (action == CatActions.Walking)
		{
			cachedRenderer.sprite = walkSprite;
			cachedMaterial.SetVector("_UVXMod", new Vector2(
					.25f,
					Mathf.Floor((Time.time * walkAnimSpeed) % 4) * .25f
				)
			);
		}
		else if (action == CatActions.Scratching)
		{
			cachedRenderer.sprite = scratchSprite;
			cachedMaterial.SetVector("_UVXMod", new Vector2(
					.5f,
					Mathf.Floor((Time.time * scratchAnimSpeed) % 2) * .5f
				)
			);
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
