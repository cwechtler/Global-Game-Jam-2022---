using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Vector2 speed = new Vector2(10, 10);

	[SerializeField] private GameObject projectile;
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float firingRate = 0.2f;

	private Rigidbody2D myrididbody2D;
	private bool allowfire = true;

	// Start is called before the first frame update
	void Start()
	{
		myrididbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		float inputY = Input.GetAxis("Vertical");
		float inputX = Input.GetAxis("Horizontal");

		float fireY = Input.GetAxis("FireVertical");
		float fireX = Input.GetAxis("FireHorizontal");

		myrididbody2D.velocity = new Vector2(speed.x * inputX, speed.y * inputY);

		if ((fireX != 0 || fireY != 0) && allowfire) {
			StartCoroutine(Fire(fireX, fireY));
		}
	}
	private IEnumerator Fire(float fireX, float fireY)
	{
		allowfire = false;

		GameObject projectile = Instantiate(this.projectile, transform.position, Quaternion.identity) as GameObject;
		Rigidbody2D projectileRidgidbody2D = projectile.GetComponent<Rigidbody2D>();
		projectileRidgidbody2D.velocity = new Vector3(fireX, fireY, 0);
		projectileRidgidbody2D.velocity = (Vector3.Normalize(projectileRidgidbody2D.velocity) * projectileSpeed);
		yield return new WaitForSeconds(firingRate);
		allowfire = true;
	}
}
