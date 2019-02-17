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

	public SpriteRenderer scratchVFXRenderer;
	public float scratchVFXAnimSpeed = 1f;

	public AudioSource audioSourceWalk;
	public float addWalkVolumePerSecond = 1f;
	public AudioSource audioSourceScratch;
	public float scratchMeowChancePerSecond = 1f;

	private float maxVolumeWalk;
	private float maxVolumeScratch;

	private Material cachedMaterial;
	private SpriteRenderer cachedRenderer;

	private BoxCollider2D collisionTrigger;

	private Transform cachedVFXTransform;
	private Material cachedVFXMaterial;
	private float scratchVFXOriginalX = 0f;


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
		collisionTrigger = GetComponent<BoxCollider2D>();

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
			audioSourceScratch.volume = 0f;
		}
		if (scratchVFXRenderer != null) {
			cachedVFXMaterial = scratchVFXRenderer.material;
			cachedVFXTransform = scratchVFXRenderer.GetComponent<Transform>();
			scratchVFXOriginalX = cachedVFXTransform.localPosition.x;
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
			if (!audioSourceScratch.isPlaying && Random.value < scratchMeowChancePerSecond * Time.deltaTime) {
				audioSourceScratch.PlayOneShot(audioSourceScratch.clip);
				audioSourceScratch.volume = maxVolumeScratch;
			}
			if (scratchVFXRenderer != null) {
				scratchVFXRenderer.enabled = true;
				scratchVFXRenderer.flipX = cachedRenderer.flipX;
				cachedVFXTransform.transform.localPosition = new Vector3(
						scratchVFXOriginalX * (cachedRenderer.flipX ? -1f : 1f),
						cachedVFXTransform.localPosition.y,
						cachedVFXTransform.localPosition.z
					);
				cachedVFXMaterial.SetVector("_UVXMod", new Vector2(
					.25f,
					Mathf.Floor((Time.time * scratchVFXAnimSpeed) % 4) * .25f
					)
				);
			}
		}

		if (direction == CatDirections.Left)
		{
			cachedRenderer.flipX = false;
			collisionTrigger.offset = new Vector2(-3.991034f, -0.4677124f); 
		}
		else {
			cachedRenderer.flipX = true;
			collisionTrigger.offset = new Vector2(3.991034f, -0.4677124f);
		}

		//cleanup!
		if (action != CatActions.Walking) {
			audioSourceWalk.volume -= addWalkVolumePerSecond * Time.deltaTime;
		}

		if (action != CatActions.Scratching) {
			if (scratchVFXRenderer != null) {
				scratchVFXRenderer.enabled = false;
			}
			audioSourceScratch.volume -= addWalkVolumePerSecond;
		}

		audioSourceWalk.volume = Mathf.Clamp(audioSourceWalk.volume, 0f, maxVolumeWalk);
		audioSourceScratch.volume = Mathf.Clamp(audioSourceScratch.volume, 0f, maxVolumeScratch);
	}
}
