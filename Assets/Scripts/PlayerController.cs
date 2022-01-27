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
	[SerializeField] private Transform skillSpawner;
	[SerializeField] private Transform skillSpawnPoint;
	[SerializeField] private GameObject lightningEndPoint;
	[SerializeField] private Transform notch;
	[SerializeField] private GameObject rigFront;
	[SerializeField] private GameObject rigBack;

	private Rigidbody2D myRigidbody2D;
	public bool allowfire = true;
	private float firingRate;
	public GameObject activeSkill;
	private Projectile activeSkillProjectile;
	private GameObject projectileToDelete;
	private int skillIndex;

	private bool moveHorizontaly;
	private bool moveVertically;

	public GameObject LightningEndPoint { get => lightningEndPoint; }

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
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

		myRigidbody2D.velocity = new Vector2(speed.x * inputX, speed.y * inputY);
		moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
		moveVertically = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

		if ((fireX != 0 || fireY != 0)) {
			skillSpawner.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-fireY, -fireX) * 180 / Mathf.PI);
			if (allowfire) {
				string skillType = activeSkill.GetComponent<Projectile>().SkillElementType.ToString();
				switch (skillType) {
					case "Water":
					case "Lightning":
						Cast();
						break;
					case "Suction":
						PlaceSkill();
						break;
					default:
						StartCoroutine(Fire(fireX, fireY, activeSkillProjectile.FireOnce));
						break;
				}
			}
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
		FlipDirection();

		//if (projectileToDelete == null) {
		//	allowfire = true;
		//}
	}

	private void Cast() {
		allowfire = false;
		GameObject spell = Instantiate(activeSkill, transform.position, Quaternion.identity) as GameObject;
		projectileToDelete = spell;
	}

	private void PlaceSkill() {
		allowfire = false;
		GameObject spell = Instantiate(activeSkill, skillSpawnPoint.position, Quaternion.identity) as GameObject;
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

	private void FlipDirection()
	{
		if (moveHorizontaly && !moveVertically) {
			rigFront.SetActive(true);
			rigBack.SetActive(false);
			float DirectionX = Mathf.Sign(myRigidbody2D.velocity.x);

			if (DirectionX == -1) {
				notch.localScale = new Vector2(1f, 1f);
			}
			if (DirectionX == 1) {
				notch.localScale = new Vector2(-1f, 1f);
			}
		}

		if (moveVertically) {
			float DirectionY = Mathf.Sign(myRigidbody2D.velocity.y);

			if (DirectionY == 1) {
				rigFront.SetActive(false);
				rigBack.SetActive(true);
			}
			if (DirectionY == -1) {
				rigFront.SetActive(true);
				rigBack.SetActive(false);
			}
		}
	}

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	print("Water");
	//}

	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//	print("stay");
	//}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	print("Water collide");
	//}
}
