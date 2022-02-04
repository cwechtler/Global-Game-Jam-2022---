using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	#if UNITY_WEBGL
	[SerializeField] private bool setFeaturedGameQuitURL = false;
	[ConditionalHide("setFeaturedGameQuitURL", true, true)]
	[SerializeField] private string webglQuitURL = "about:blank";
	#endif

	public string currentScene { get; private set; }

	private void Awake(){
		if (instance == null){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else if (instance != this){
			Destroy(gameObject);
		}
	}

	void Start(){
		currentScene = SceneManager.GetActiveScene().name;
	}

	private void Update(){
		if (SceneManager.GetActiveScene().name != currentScene) {
			currentScene = SceneManager.GetActiveScene().name;
		}

		if (Input.GetButtonDown("Enter")) {
			if (currentScene != "Main Menu") {
				LoadLevel("Main Menu");
			}
			else {
				LoadLevel("MikeTest");
			}
		}

		if (Input.GetButtonDown("Cancel")) {
			QuitRequest();
		}
	}

	public void LoadLevel (string name, bool restart = false){
		Debug.Log("Level load requested for: " + name);
		if (restart) {
			GameController.instance.resetGame();
		}
		StartCoroutine(LoadLevel(name, .9f));
	}

	public IEnumerator LoadLevel(string name, float waitTime)
	{
		GameController.instance.FadePanel();
		yield return new WaitForSeconds(waitTime);
		SoundManager.instance.PlayMusicForScene(ReferanceIndex(name));
		SceneManager.LoadScene(name);
	}

	public void LoadLevel(int levelIndex, float waitTime) {
		StartCoroutine(LoadScene(levelIndex, waitTime));
	}

	public void LoadLevelAdditive(string name) {
		SoundManager.instance.PlayMusicForScene(ReferanceIndex(name));
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}
	
	public void LoadNextLevel() {
		StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1, .9f));
		currentScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name;
	}

	private IEnumerator LoadScene(string name, float waitTime)
	{
		GameController.instance.FadePanel();
		Debug.Log("start Coroutine");
		yield return new WaitForSeconds(waitTime);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
		yield return new WaitUntil(() => asyncOperation.isDone);
		print("Scene " + currentScene + " Loaded");
		SoundManager.instance.PlayMusicForScene(ReferanceIndex(name));
	}

	private IEnumerator LoadScene(int sceneToLoad, float waitTime)
	{
		print("Load " + sceneToLoad);
		yield return new WaitForSeconds(waitTime);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		yield return new WaitUntil(() => asyncOperation.isDone);
		print("Scene " + currentScene + " Loaded");
		//if (currentScene == "Level 1") {
		//	GameController.instance.LoadSceneObjects();
		//}	
	}

	public IEnumerator UnloadScene(float waitTime, string name)
	{
		float counter = 0f;

		while (counter < waitTime) {
			counter += GameController.instance.timeDeltaTime;
		}
		if (counter >= waitTime) {
			print("Unload");
			SceneManager.UnloadSceneAsync(name);
		}
		yield return null;
	}

	public void QuitRequest()
	{
		Debug.Log("Level Quit Request");

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;


		#elif UNITY_WEBGL
			Application.Quit();
			if (setFeaturedGameQuitURL) {
				WebGLPluginJS.SessionRedirect();
			}
			else {
				WebGLPluginJS.Redirect(webglQuitURL);
			}
		
		#else
			Application.Quit();
		#endif
	}

	private int ReferanceIndex(string scene)
	{
		int randomIndex = Random.Range(2, SoundManager.instance.MusicArrayLength);
		int clipIndex;
		switch (scene) {
			case "Main Menu":
				clipIndex = 0;
				break;
			case "Options":
				clipIndex = 0;
				break;
			case "Lose Level":
				clipIndex = 1;
				break;
			case "Test Level":
			case "MikeTest":
			case "Level 1":
				clipIndex = randomIndex;
				break;
			case "Level 2":
				clipIndex = randomIndex;
				break;
			default:
				clipIndex = 0;
				break;
		}
		return clipIndex;
	}
}
