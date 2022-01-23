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

	public List<string> collectedItems;

	private GameObject fadePanel;
	private Vector3 spawnPointLocation;
	private Animator animator;
	private bool continueGame = false;

	private void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		StartCoroutine(LateStart(.1f));
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

	public List<string> GetCollectedItems() {
		return collectedItems;
	}

	public void CollectItems(string itemName) {
		collectedItems.Add(itemName);
		//GameCanvasController gameCanvasController = FindObjectOfType<GameCanvasController>();

		//gameCanvasController.AddCollectedItem(itemName);

		if (collectedItems.Count >= 4) {
			animator.SetBool("FadeOut", true);
			LevelManager.instance.LoadNextLevel();
			collectedItems.Clear();
		}
	}

	public void StartGame()
	{
		AstarPath.active.Scan();
		PlayerPrefsManager.DeletePlayerPrefsPlayerInfo();
		collectedItems.Clear();
	}

	public void Continue()
	{
		continueGame = true;

		collectedItems = PlayerPrefsManager.GetItems();
		spawnPointLocation = new Vector3(PlayerPrefsManager.GetPlayerSpawnpointX(), PlayerPrefsManager.GetPlayerSpawnpointY(), 0);

		LevelManager.instance.LoadLevel(3, .9f);
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


	public void SavePlayerInfo() {
		PlayerPrefsManager.SetItems(collectedItems);
		PlayerPrefsManager.SetPlayerSpawnpointX(playerGO.transform.position.x);
		PlayerPrefsManager.SetPlayerSpawnpointY(playerGO.transform.position.y);

		collectedItems.Clear();
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
