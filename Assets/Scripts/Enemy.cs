using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private skillElementType skillElementTypeToDestroy;
	[SerializeField] private float health = 1f;

	private AIDestinationSetter destinationSetter;
	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		destinationSetter = GetComponent<AIDestinationSetter>();
		destinationSetter.target = player.transform;
	}

	void Update()
	{

	}

	public void reduceHealth(float damage) {
		health -= damage;
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Projectile")) {
			Projectile projectile = collision.GetComponent<Projectile>();

			if (projectile.SkillElementType == skillElementTypeToDestroy) {
				reduceHealth(collision.GetComponent<Projectile>().GetDamage());
			}
		}		
	}
}
