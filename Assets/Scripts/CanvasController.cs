using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private GameObject[] skills;
	[SerializeField] private GameObject fadePanel;

	[SerializeField] private TextMeshProUGUI ScoreText;

	private Button button;
	private TextMeshProUGUI buttonText;
	private Animator animator;

	private void Start()
	{
		animator = fadePanel.GetComponent<Animator>();
	}

	private void Update()
	{
		ScoreText.text = GameController.instance.EnemiesKilled.ToString();
	}

	public void MainMenu()
	{
		animator.SetBool("FadeOut", true);
		LevelManager.instance.LoadLevel(0, .9f);
	}

	//public void StartNewGame() {
	//	animator.SetBool("FadeOut", true);
	//	LevelManager.instance.StartNewGame();
	//}

	//public void ContinueGame() {
	//	animator.SetBool("FadeOut", true);
	//	LevelManager.instance.Continue();
	//}

	public void Options()
	{
		animator.SetBool("FadeOut", true);
		LevelManager.instance.LoadLevel(1, .9f);
	}

	public void QuitGame()
	{
		LevelManager.instance.QuitRequest();
	}
}
