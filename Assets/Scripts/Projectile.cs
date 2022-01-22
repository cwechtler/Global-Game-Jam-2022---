using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float damage = 100f;
	[SerializeField] private GameObject elementType;

	// Start is called before the first frame update
	void Start()
	{
		this.transform.up = this.GetComponent<Rigidbody2D>().velocity;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public float GetDamage()
	{
		return damage;
	}
	public void Hit()
	{
		Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
