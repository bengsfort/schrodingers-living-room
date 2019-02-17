using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrController : MonoBehaviour
{

	public enum SchrActions
	{
		Standing,
		Talking
	}

	public SchrActions action = SchrActions.Standing;
	public float talkAnimSpeed = 1f;

	public AudioSource audioSourceTalk;
	public float addTalkVolumePerSecond = 1f;

	private float maxVolumeTalk;

	private Material cachedMaterial;
	private SpriteRenderer cachedRenderer;

	public void SetTalking() {
		action = SchrActions.Talking;
	}
	public void SetStanding() {
		action = SchrActions.Standing;
	}

	void Start()
	{
		cachedRenderer = GetComponent<SpriteRenderer>();
		if (cachedRenderer != null)
		{
			cachedMaterial = cachedRenderer.material;
			cachedRenderer.material.shader = Shader.Find("Sch/Characters/Cat");
		}
		if (audioSourceTalk != null)
		{
			maxVolumeTalk = audioSourceTalk.volume;
			audioSourceTalk.loop = true;
			audioSourceTalk.volume = 0f;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (action == SchrActions.Standing)
		{
			cachedMaterial.SetVector("_UVXMod", new Vector2(.5f, 0f));
		}
		else if (action == SchrActions.Talking)
		{
			cachedMaterial.SetVector("_UVXMod", new Vector2(
					.5f,
					Mathf.Floor((Time.time * talkAnimSpeed) % 2) * .5f
				)
			);
			audioSourceTalk.volume += addTalkVolumePerSecond * Time.deltaTime;
		}

		//cleanup!
		if (action != SchrActions.Talking)
		{
			audioSourceTalk.volume -= addTalkVolumePerSecond * Time.deltaTime;
		}

		audioSourceTalk.volume = Mathf.Clamp(audioSourceTalk.volume, 0f, maxVolumeTalk);

	}
}
