using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
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
}
