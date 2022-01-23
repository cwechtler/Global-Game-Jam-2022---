using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private skillElementType skillElementType;
	[SerializeField] private float health = 50f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void reduceHealth(float damage) {
		health -= damage;

		Debug.Log("Damage: " + damage);
		if (health <= 0) {
			Destroy(gameObject);
		}

		Debug.Log("health: " + health);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Projectile"))
			reduceHealth(collision.GetComponent<Projectile>().GetDamage());
	}
}
