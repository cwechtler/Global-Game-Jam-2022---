using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillElementType
{
	Fire,
	Water,
	Lightning,
	Suction

}

public class Projectile : MonoBehaviour
{
	[SerializeField] private float damage = 100f;
	[SerializeField] private float firingRate = 0.5f;
	[SerializeField] private bool fireOnce = false;
	[SerializeField] protected skillElementType skillElementType;

	public float FireRate { get { return firingRate; } }
	public bool FireOnce { get { return fireOnce; } }
	public skillElementType SkillElementType { get { return skillElementType; } }


	void Start()
	{
		this.transform.up = this.GetComponent<Rigidbody2D>().velocity;
	}

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
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		//Destroy(gameObject);
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		//Destroy(gameObject);
	}
}
