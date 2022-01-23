using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : Projectile
{
	[Space]
	[Tooltip("The particle system to play.")]
	[SerializeField] private ParticleSystem ProjectileParticleSystem;
	[Tooltip("The particle system to play upon collision.")]
	[SerializeField] private ParticleSystem ProjectileExplosionParticleSystem;

	[Tooltip("The sound to play upon collision.")]
	[SerializeField] private AudioSource ProjectileCollisionSound;

	void Start()
	{
		if (ProjectileParticleSystem) {
			ProjectileParticleSystem.Play();
		}
	}

	void Update()
	{
		if (ProjectileParticleSystem && !ProjectileParticleSystem.isPlaying) {
			Destroy(gameObject);
		}
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (ProjectileParticleSystem != null) {
			Destroy(ProjectileParticleSystem.gameObject);
		}

		if (ProjectileExplosionParticleSystem != null) {
			ProjectileExplosionParticleSystem.Play();
		}

		if (ProjectileCollisionSound != null) {
			ProjectileCollisionSound.Play();
		}

		Destroy(gameObject, ProjectileCollisionSound.clip.length);
	}

	//protected override void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (ProjectileParticleSystem != null) {
	//		Destroy(ProjectileParticleSystem.gameObject);
	//	}

	//	if (ProjectileExplosionParticleSystem != null) {
	//		ProjectileExplosionParticleSystem.Play();
	//	}

	//	if (ProjectileCollisionSound != null) {
	//		ProjectileCollisionSound.Play();
	//	}

	//	Destroy(gameObject, ProjectileCollisionSound.clip.length);

	//}
}
