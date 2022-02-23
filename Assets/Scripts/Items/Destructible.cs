using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	[SerializeField] private skillElementType skillRequiredToDestroy;
	[Space]
	[Tooltip("Check box to select a prefab item to be dropped on destroy")]
	[SerializeField] private bool dropItem = true;
	[ConditionalHide("dropItem", true)]
	[SerializeField] private GameObject prefabToDrop;


	private bool dropped;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Skill") && !dropped) {
			if (collision.GetComponentInParent<SkillConfig>().SkillElementType == skillRequiredToDestroy) {
				dropped = true;
				if (dropItem) {
					GameObject itemDrop = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
				}
				GameObject.Destroy(this.gameObject);

			}
		}
	}

	private void OnParticleCollision(GameObject particle)
	{
		SkillConfig particleParent = particle.GetComponentInParent<SkillConfig>();
		if (particleParent.SkillElementType == skillRequiredToDestroy && !dropped) {
			dropped = true;
			if (dropItem) {
				GameObject itemDrop = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
			}
			GameObject.Destroy(this.gameObject);
		}
	}
}
