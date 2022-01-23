using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Vector2 speed = new Vector2(10, 10);
	[SerializeField] private float projectileSpeed;
	[Space]
	[Tooltip("Skill Prefabs")]
	[SerializeField] private GameObject[] Skills;

	private Rigidbody2D myrididbody2D;
	private bool allowfire = true;
	private float firingRate;
	public GameObject activeSkill;
	private Projectile activeSkillProjectile;
	private GameObject projectileToDelete;
	private int skillIndex;

	void Start()
	{
		myrididbody2D = GetComponent<Rigidbody2D>();
		activeSkill = Skills[0];
		activeSkillProjectile = activeSkill.GetComponent<Projectile>();
		firingRate = activeSkillProjectile.FireRate;
	}

	void Update()
	{
		float inputY = Input.GetAxis("Vertical");
		float inputX = Input.GetAxis("Horizontal");

		float fireY = Input.GetAxis("FireVertical");
		float fireX = Input.GetAxis("FireHorizontal");

		myrididbody2D.velocity = new Vector2(speed.x * inputX, speed.y * inputY);

		if ((fireX != 0 || fireY != 0) && allowfire) {
			StartCoroutine(Fire(fireX, fireY, activeSkillProjectile.FireOnce));
		}

		if (Input.GetButtonDown("Jump")) {
			if (Skills.Length > skillIndex + 1) {
				skillIndex++;
			}
			else {
				skillIndex = 0;
			} 
			activeSkill = Skills[skillIndex];
			activeSkillProjectile = activeSkill.GetComponent<Projectile>();
			firingRate = activeSkillProjectile.FireRate;
		}
	}

	private IEnumerator Fire(float fireX, float fireY, bool fireOnce = false)
	{
		if (fireOnce) {
			Destroy(projectileToDelete);
		}

		allowfire = false;

		GameObject projectile = Instantiate(activeSkill, transform.position, Quaternion.identity) as GameObject;
		projectileToDelete = projectile;
		Rigidbody2D projectileRidgidbody2D = projectile.GetComponent<Rigidbody2D>();
		projectileRidgidbody2D.velocity = new Vector3(fireX, fireY, 0);
		projectileRidgidbody2D.velocity = (Vector3.Normalize(projectileRidgidbody2D.velocity) * projectileSpeed);
		yield return new WaitForSeconds(firingRate);
		allowfire = true;
	}
}
