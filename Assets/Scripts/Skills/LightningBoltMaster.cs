using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltMaster : SkillConfig
{
	[SerializeField] private float duration = 5f;
	private bool hitObject = false;
	private ParticleSystem feetParticleSystem;

	public bool HitObject { get => hitObject; set => hitObject = value; }

	void Start()
	{
		transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		feetParticleSystem = GetComponentInChildren<ParticleSystem>();
		StartCoroutine(DestroyLightning(duration));
	}

	void Update()
	{
		
	}

	private IEnumerator DestroyLightning(float skillDuration)
	{
		yield return new WaitForSeconds(skillDuration);
		feetParticleSystem.Stop();
		yield return new WaitForSeconds(.3f);
		Destroy(gameObject);
	}
}
