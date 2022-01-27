using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltMaster : Projectile
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
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerController>().allowfire = true;
		feetParticleSystem.Stop();
		yield return new WaitForSeconds(.3f);
		Destroy(gameObject);
	}
}
