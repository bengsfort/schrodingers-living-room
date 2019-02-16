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

	public AudioSource audioSourceWalk;
	public float addWalkVolumePerSecond = 1f;
	public AudioSource audioSourceScratch;
	public float scratchMeowChancePerSecond = 1f;

	private float maxVolumeWalk;
	private float maxVolumeScratch;

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
    public void ChangeAnimationScratching()
    {
        action = CatActions.Scratching;
    }
    public void ChangeAnimationWalking()
    {
        action = CatActions.Walking;
    }
    public void ChangeAnimationStanding()
    {
        action = CatActions.Standing;
    }


    // Start is called before the first frame update
    void Start()
    {
		cachedRenderer = GetComponent<SpriteRenderer>();
		if (cachedRenderer != null) {
			cachedMaterial = cachedRenderer.material;
			cachedRenderer.material.shader = Shader.Find("Sch/Characters/Cat");
		}
		if (audioSourceWalk != null) {
			maxVolumeWalk = audioSourceWalk.volume;
			audioSourceWalk.loop = true;
			audioSourceWalk.volume = 0f;
		}
		if (audioSourceScratch != null)
		{
			maxVolumeScratch = audioSourceScratch.volume;
			//audioSourceScratch.volume = 0f;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (action == CatActions.Standing)
		{
			cachedRenderer.sprite = walkSprite;
			cachedMaterial.SetVector("_UVXMod", new Vector2(.25f, 0f));
			audioSourceWalk.volume -= addWalkVolumePerSecond * Time.deltaTime;
		}
		else if (action == CatActions.Walking)
		{
			cachedRenderer.sprite = walkSprite;
			cachedMaterial.SetVector("_UVXMod", new Vector2(
					.25f,
					Mathf.Floor((Time.time * walkAnimSpeed) % 4) * .25f
				)
			);
			audioSourceWalk.volume += addWalkVolumePerSecond * Time.deltaTime;
		}
		else if (action == CatActions.Scratching)
		{
			cachedRenderer.sprite = scratchSprite;
			cachedMaterial.SetVector("_UVXMod", new Vector2(
					.5f,
					Mathf.Floor((Time.time * scratchAnimSpeed) % 2) * .5f
				)
			);
			audioSourceWalk.volume -= addWalkVolumePerSecond * Time.deltaTime;
			if (!audioSourceScratch.isPlaying && Random.value < scratchMeowChancePerSecond * Time.deltaTime) {
				audioSourceScratch.PlayOneShot(audioSourceScratch.clip);
			}
		}

		if (direction == CatDirections.Left)
		{
			cachedRenderer.flipX = false;
		}
		else {
			cachedRenderer.flipX = true;
		}

		audioSourceWalk.volume = Mathf.Clamp(audioSourceWalk.volume, 0f, maxVolumeWalk);
	}
}
