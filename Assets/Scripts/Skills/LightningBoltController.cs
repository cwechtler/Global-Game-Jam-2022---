using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltController : MonoBehaviour
{

	[SerializeField] private LightningBoltScript childBolt;

	private CapsuleCollider2D capsule;
	private LightningBoltScript lightningBoltScript;
	private GameObject lightningEndPoint;

	void Start()
	{
		capsule = GetComponent<CapsuleCollider2D>();
		lightningBoltScript = GetComponent<LightningBoltScript>();
		lightningEndPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().LightningEndPoint;
		lightningBoltScript.EndObject = lightningEndPoint;
		childBolt.EndObject = lightningBoltScript.EndObject;
	}

	void Update()
	{
		if (lightningBoltScript.EndObject == null) {
			lightningBoltScript.EndObject = lightningEndPoint;
			childBolt.EndObject = lightningBoltScript.EndObject;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GetComponentInParent<LightningBoltMaster>().HitObject = true;
		if (collision.gameObject.CompareTag("Enemy")) {
			skillElementType type = collision.GetComponent<Enemy>().SkillElementTypeToDestroy;
			skillElementType skillElementType = GetComponentInParent<SkillConfig>().SkillElementType;
			if (skillElementType == type) {
				lightningBoltScript.EndObject = collision.gameObject;
				childBolt.EndObject = collision.gameObject;
			}
		}
	}
}
