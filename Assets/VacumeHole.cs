using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumeHole : Projectile
{
	[Space]
	[SerializeField] private float vacumeHoleDuration = 2f;

	void Start()
	{
		StartCoroutine(DestroySkill(vacumeHoleDuration));
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) {
			skillElementType type = collision.GetComponent<Enemy>().SkillElementTypeToDestroy;
			if (skillElementType == type) {
				collision.GetComponent<AIDestinationSetter>().target = transform;
			}		
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(collision.gameObject);
	}

	private IEnumerator DestroySkill(float skillDuration)
	{
		yield return new WaitForSeconds(skillDuration);
		Destroy(gameObject);
	}
}
