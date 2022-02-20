using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	[SerializeField] private GameObject prefabToDrop;
	[SerializeField] private skillElementType skillRequiredToDestroy;

	private bool dropped;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Skill") && !dropped) {
			if (collision.GetComponentInParent<SkillConfig>().SkillElementType == skillRequiredToDestroy) {
				dropped = true;
				GameObject itemDrop = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
				GameObject.Destroy(this.gameObject);

			}
		}
	}

	private void OnParticleCollision(GameObject particle)
	{
		SkillConfig particleParent = particle.GetComponentInParent<SkillConfig>();
		if (particleParent.SkillElementType == skillRequiredToDestroy && !dropped) {
			dropped = true;
			GameObject itemDrop = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
			GameObject.Destroy(this.gameObject);
		}
	}
}
