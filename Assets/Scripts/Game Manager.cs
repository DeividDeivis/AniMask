using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private DialogueSystem dialogueSystem;

    [Header("Menu Settings")]
    [SerializeField] private Button StartGame;

    [Header("End Settings")]
    [SerializeField] private GameObject EndSceneContainer;
    [SerializeField] private Button PlayAgain;

    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame.onClick.AddListener(PlayGame);

        SceneInfo.OnSceneComplete += SceneComplete;

        sceneController.GameOver += ShowEndScene;
        PlayAgain.onClick.AddListener(RestartGame);
    }

    private void PlayGame() 
    {
        sceneController.SetScene(0);
    }

    private void SceneComplete() 
    {
        sceneController.NextScene();
    }

    private void ShowEndScene()
    {
        DialogueSystem.Instance.CloseDialogue();
        DialogueSystem.Instance.CloseForkDialogue();

        dialogueSystem.gameObject.SetActive(false);

        EndSceneContainer.SetActive(true);
    }

    private void RestartGame() 
    {
        EndSceneContainer.SetActive(false);
        DialogueSystem.OnNextDialogueClick = null;
    }
}
