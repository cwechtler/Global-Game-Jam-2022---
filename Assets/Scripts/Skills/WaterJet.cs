using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJet : SkillConfig
{
	[Space]
	[SerializeField] private float waterJetDuration = 1.5f; 
	[SerializeField] private float maxAudioVolume = 1f;

	private GameObject player;
	private PlayerController playerController;
	private AudioSource audioSource;
	private ParticleSystem[] waterjetParticleSystems;
	//private ParticleSystem waterShieldParticleSystem;
	private bool clear = false;
	private float audioVolume = .4f;
	private float soundEffectFadeInTime = 1f;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<PlayerController>();
		audioSource = GetComponent<AudioSource>();
		waterjetParticleSystems = GetComponentsInChildren<ParticleSystem>();
		StartCoroutine(DestroySkill(waterJetDuration));
	}

	void Update()
	{
		float fireY = playerController.FireY; //Input.GetAxis("SpellVertical");
		float fireX = playerController.FireX; //Input.GetAxis("SpellHorizontal");

		transform.position = player.transform.position;
		if (fireX != 0 || fireY != 0) {
			transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-fireX, fireY) * 180 / Mathf.PI);
		}
		if (!clear) {
			if (audioVolume <= maxAudioVolume) {
				audioVolume += soundEffectFadeInTime * Time.deltaTime;
				audioSource.volume = audioVolume;
			}
			else {
				audioVolume = maxAudioVolume;
			}

			if (audioSource.clip != null) {
				if (!audioSource.isPlaying) {
					audioSource.Play();
					audioSource.volume = 0f;
					audioVolume = 0f;
				}
			}
		}
		else {
			if (audioVolume > 0) {
				audioVolume -= (waterjetParticleSystems[0].main.duration * .1f) * Time.deltaTime;
				audioSource.volume = audioVolume;
			}
		}
	}

	private IEnumerator DestroySkill(float skillDuration)
	{
		yield return new WaitForSeconds(skillDuration);
		clear = true;
		foreach (var waterjetParticleSystem in waterjetParticleSystems) {
			waterjetParticleSystem.Stop();
		}	
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
