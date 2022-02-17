using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumeHole : SkillConfig
{
	[Space]
	[SerializeField] private float vacumeHoleDuration = 2f;

	private ParticleSystem[] vacumeHoleParticleSystems;

	void Start()
	{
		vacumeHoleParticleSystems = GetComponentsInChildren<ParticleSystem>();
		StartCoroutine(DestroySkill(vacumeHoleDuration));
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy")) {
			skillElementType type = collision.GetComponent<Enemy>().SkillElementTypeToDestroy;
			if (skillElementType == type) {
				collision.gameObject.layer = 11;
				collision.GetComponent<AIDestinationSetter>().target = transform;
				collision.GetComponent<AIPath>().maxSpeed = 6f;
			}		
		}
	}

	private IEnumerator DestroySkill(float skillDuration)
	{
		yield return new WaitForSeconds(skillDuration);
		foreach (var vacumeHoleParticleSystem in vacumeHoleParticleSystems) {
			vacumeHoleParticleSystem.Stop();
		}	
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
