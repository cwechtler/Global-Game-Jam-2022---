using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumeHole : Projectile
{
	[Space]
	[SerializeField] private float vacumeHoleDuration = 2f;

	//private List<GameObject> enemies = new List<GameObject>();
	private ParticleSystem[] vacumeHoleParticleSystems;

	void Start()
	{
		vacumeHoleParticleSystems = GetComponentsInChildren<ParticleSystem>();
		StartCoroutine(DestroySkill(vacumeHoleDuration));
	}

	void Update()
	{

	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy")) {
			skillElementType type = collision.GetComponent<Enemy>().SkillElementTypeToDestroy;
			if (skillElementType == type) {
				//enemies.Add(collision.gameObject);
				collision.gameObject.layer = 11;
				collision.GetComponent<AIDestinationSetter>().target = transform;
				collision.GetComponent<AIPath>().maxSpeed = 6f;
			}		
		}
	}

	private IEnumerator DestroySkill(float skillDuration)
	{
		yield return new WaitForSeconds(skillDuration);
		//foreach (var enemy in enemies) {
		//	if (enemy != null) {
		//		enemy.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
		//		enemy.GetComponent<AIPath>().maxSpeed = 4f;
		//	}	
		//}
		foreach (var vacumeHoleParticleSystem in vacumeHoleParticleSystems) {
			vacumeHoleParticleSystem.Stop();
		}	
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
