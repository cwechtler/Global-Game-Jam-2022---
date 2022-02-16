using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	public GameObject playerGO { get; private set; }
	public bool isPaused { get; private set; }
	public float timeDeltaTime { get; private set; }
	public int EnemiesKilled { get; set; }
	public int ActiveSkillIndex { get; set; }
	public int Shadow { get => shadow; }
	public int Air { get => air; }
	public int Fire { get => fire; }
	public int Water { get => water; }
	public string PlayerName { get; set; }
	public List<Tuple<string, int>> HighScores { get => highScores; set => highScores = value; }

	private List<Tuple<string, int>> highScores = new List<Tuple<string, int>>();
	private GameObject fadePanel;
	private Animator animator;
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
		//PlayerPrefsManager.DeleteAllPlayerPrefs();

		if (PlayerPrefsManager.DoesKeyExist(PlayerPrefsManager.HighScoresKey)) {
			string highScoresString = PlayerPrefsManager.GetHighScores();
			highScores = JsonConvert.DeserializeObject<List<Tuple<string, int>>>(highScoresString);

			highScores.Sort((x, y) => y.Item2.CompareTo(x.Item2));
		}

		if (PlayerPrefsManager.DoesKeyExist(PlayerPrefsManager.PlayerNameKey)) {
			PlayerName = PlayerPrefsManager.GetPlayerName();
		}
		else {
			PlayerName = System.Environment.MachineName;
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Pause")) {
			if (!isPaused) {
				PauseGame();
			}
			else {
				ResumeGame();
			}
		}
	}

	public bool SaveHighScore(string name)
	{
		highScores.Add(Tuple.Create(name, EnemiesKilled));
		highScores.Sort((x, y) => y.Item2.CompareTo(x.Item2));

		string json = JsonConvert.SerializeObject(highScores, Formatting.Indented);
		PlayerPrefsManager.SetHighScores(json);

		return true;
	}

	public void AddEnemyType(skillElementType skillElementType) {
		switch (skillElementType) {
			case skillElementType.Fire:
				water++;
				break;
			case skillElementType.Water:
				fire++;
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
	public void resetGame() {
		EnemiesKilled = 0;
		shadow = 0;
		air = 0;
		fire = 0;
		water = 0;
	}

	public void FadePanel()
	{	
		fadePanel = GameObject.FindGameObjectWithTag("Fade Panel");
		fadePanel.GetComponent<Animator>().SetBool("FadeIn", true);
	}

	private void PauseGame()
	{
		timeDeltaTime = Time.deltaTime;
		isPaused = true;
		Time.timeScale = 0;
	}

	private void ResumeGame()
	{
		Time.timeScale = 1;
		isPaused = false;
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
