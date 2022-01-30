using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private int health = 100;
	[SerializeField] private Vector2 speed = new Vector2(10, 10);
	[SerializeField] private float projectileSpeed;
	[Space]
	[Tooltip("Skill Prefabs")]
	[SerializeField] private GameObject[] skills;
	[SerializeField] private Transform skillSpawner;
	[SerializeField] private Transform skillSpawnPoint;
	[SerializeField] private GameObject lightningEndPoint;
	[SerializeField] private Transform notch;
	[SerializeField] private GameObject rigFront;
	[SerializeField] private GameObject rigBack;
	[SerializeField] private CanvasController canvasController;

	private Rigidbody2D myRigidbody2D;
	private Animator[] animators;

	private bool moveHorizontaly;
	private bool moveVertically;
	private bool isDead = false;

	private GameObject activeSkill;
	private int activeSkillIndex;
	private float firingRate;
	private float[] coolDownTimes;
	private float[] timerTimes;
	private bool[] skillWasCast;

	public GameObject LightningEndPoint { get => lightningEndPoint; }

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		animators = GetComponentsInChildren<Animator>(true);

		SetActiveSkill(0);

		coolDownTimes = new float[skills.Length];
		timerTimes = new float[skills.Length];
		skillWasCast = new bool[skills.Length];

		for (int i = 0; i < skills.Length; i++) {
			Projectile skill = skills[i].GetComponent<Projectile>();
			coolDownTimes[i] = skill.CoolDownTime;
			canvasController.SetCoolDownTime(i, coolDownTimes[i]);
			canvasController.SetSkillImages(i, skill.SkillImage);
		}
	}

	void Update()
	{
		if (!isDead) {
			float inputY = Input.GetAxis("Vertical");
			float inputX = Input.GetAxis("Horizontal");

			float fireY = Input.GetAxis("ArrowsVertical");
			float fireX = Input.GetAxis("ArrowsHorizontal");

			myRigidbody2D.velocity = new Vector2(speed.x * inputX, speed.y * inputY);
			moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
			moveVertically = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

			SetAnimations();
			FlipDirection();

			SelectSkill();
			Fire(fireX, fireY);
		}
	}

	private void SetAnimations()
	{
		if (moveHorizontaly || moveVertically) {
			foreach (var animator in animators) {
				if (animator.isActiveAndEnabled)
					animator.SetBool("Move", true);
			}
		}
		else {
			foreach (var animator in animators) {
				if (animator.isActiveAndEnabled)
					animator.SetBool("Move", false);
			}
		}
	}

	private void SetActiveSkill(int index) {
		GameController.instance.ActiveSkillIndex = index;
		activeSkillIndex = index;
		activeSkill = skills[index];
		Projectile activeSkillProjectile = activeSkill.GetComponent<Projectile>();
		firingRate = activeSkillProjectile.FireRate;
		canvasController.UpdateTextColor();
	}

	private void SelectSkill()
	{
		if (Input.GetButtonDown("Fire1")) {
			SetActiveSkill(0);
		}
		else if (Input.GetButtonDown("Fire2")) {
			SetActiveSkill(1);
		}
		else if (Input.GetButtonDown("Fire3")) {
			SetActiveSkill(2);
		}
		else if (Input.GetButtonDown("Jump")) {
			SetActiveSkill(3);
		}
	}

	private void Fire(float fireX, float fireY) {
		if ((fireX != 0 || fireY != 0)) {
			skillSpawner.eulerAngles = new Vector3(0, 0, Mathf.Atan2(fireY, fireX) * 180 / Mathf.PI);
			if (skillWasCast[activeSkillIndex] == false) {
				skillWasCast[activeSkillIndex] = true;
				string skillType = activeSkill.GetComponent<Projectile>().SkillElementType.ToString();
				foreach (var animator in animators) {
					animator.SetTrigger("Attack");
				}
				switch (skillType) {
					case "Water":
					case "Lightning":
						CastSkill();
						break;
					case "Suction":
						PlaceSkill();
						break;
					default:
						StartCoroutine(ThrowSkill(fireX, fireY));
						break;
				}
			}
		}

		for (int i = 0; i < skills.Length; i++) {
			if (skillWasCast[i]) {
				if (timerTimes[i] < coolDownTimes[i]) {
					timerTimes[i] += Time.deltaTime;
					canvasController.CoolDownTimer(timerTimes[i], coolDownTimes[i], i);
				}
				else if (timerTimes[i] >= coolDownTimes[i]) {
					timerTimes[i] = 0;
					skillWasCast[i] = false;
				}
			}
		}
	}

	private void CastSkill() {
		GameObject spell = Instantiate(activeSkill, transform.position, Quaternion.identity) as GameObject;
	}

	private void PlaceSkill() {
		GameObject spell = Instantiate(activeSkill, skillSpawnPoint.position, Quaternion.identity) as GameObject;
	}

	private IEnumerator ThrowSkill(float fireX, float fireY)
	{
		GameObject projectile = Instantiate(activeSkill, transform.position, Quaternion.identity) as GameObject;
		Rigidbody2D projectileRidgidbody2D = projectile.GetComponent<Rigidbody2D>();
		projectileRidgidbody2D.velocity = new Vector3(-fireX, -fireY, 0);
		projectileRidgidbody2D.velocity = (Vector3.Normalize(projectileRidgidbody2D.velocity) * projectileSpeed);
		yield return new WaitForSeconds(firingRate);
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

	public void ReduceHealth(int damage)
	{
		SoundManager.instance.PlayHurtClip();
		health -= damage;
		canvasController.ReduceHealthBar(health);

		if (health <= 0) {
			StartCoroutine(PlayerDeath());
		}
	}

	private IEnumerator PlayerDeath()
	{
		print("Player Died Do something");
		isDead = true;
		foreach (var animator in animators) {
			animator.SetBool("IsDead", true);
		}
		SoundManager.instance.PlayDeathClip();
		yield return new WaitForSeconds(2f);
		LevelManager.instance.LoadLevel("Lose Level");
	}
}
