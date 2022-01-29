using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	public GameObject playerGO { get; private set; }
	public bool isPaused { get; private set; }
	public float timeDeltaTime { get; private set; }
	public bool OptionsOverlayOpen { get; set; } = false;
	public int EnemiesKilled { get; set; }
	public int ActiveSkillIndex { get; set; }
	public int Shadow { get => shadow; }
	public int Air { get => air; }
	public int Fire { get => fire; }
	public int Water { get => water; }

	private int enemiesKilled;
	private GameObject fadePanel;
	private Vector3 spawnPointLocation;
	private Animator animator;
	private bool continueGame = false;
	private int shadow, air, fire, water;

	private void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		//StartCoroutine(LateStart(.1f));
	}

	private void Update()
	{
		if (Input.GetButtonDown("Submit")) {
			if (!GameController.instance.isPaused) {
				GameController.instance.PauseGame();
			}
			else {
				GameController.instance.ResumeGame();
			}
		}
	}

	IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		AstarPath.active.Scan();
	}


	private void FindSceneObjects() {
		playerGO = GameObject.FindGameObjectWithTag("Player");
		fadePanel = GameObject.FindGameObjectWithTag("Fade Panel");
		if (fadePanel != null) {
			animator = fadePanel.GetComponent<Animator>();
		}
	}

	public void AddEnemyType(skillElementType skillElementType) {
		switch (skillElementType) {
			case skillElementType.Fire:
				fire++;
				break;
			case skillElementType.Water:
				water++;
				break;
			case skillElementType.Lightning:
				shadow++;
				break;
			case skillElementType.Suction:
				air++;
				break;
			default:
				break;
		}
	}

	public void StartGame()
	{
		StartCoroutine(LateStart(.1f));
	}

	public void PauseGame()
	{
		timeDeltaTime = Time.deltaTime;
		isPaused = true;
		Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
		isPaused = false;
	}

	public void CloseOverlayOptions() {
		isPaused = false;
	}

	public void LoadSceneObjects() {
		FindSceneObjects();
		if (continueGame) {
			playerGO.transform.position = spawnPointLocation;
			continueGame = false;
		}
	}

	private IEnumerator RespawnPlayer(int waitToSpawn)
	{
		yield return new WaitForSeconds(waitToSpawn);
		//playerGO.transform.position = spawnPoint.transform.position;
		playerGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		playerGO.gameObject.SetActive(true);
		playerGO.GetComponent<Rigidbody2D>().isKinematic = false;
		yield return new WaitForSeconds(1);
	}


	public IEnumerator FadeCanvasGroup_TimeScale_0(CanvasGroup canvasGroup, bool isPanelOpen, float fadeTime)
	{
		float counter = 0f;

		if (isPanelOpen) {
			while (counter < fadeTime) {
				counter += timeDeltaTime;
				canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTime / timeDeltaTime);
			}
		}
		else {
			while (counter < fadeTime) {
				counter += timeDeltaTime;
				canvasGroup.alpha = Mathf.Lerp(0, 1, fadeTime / timeDeltaTime);
			}
		}
		yield return null;
	}
}
