using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    [Header("Menu Settings")]
    [SerializeField] private Button StartGame;

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
    }

    private void PlayGame() 
    {
        sceneController.SetScene(0);
    }

    private void SceneComplete() 
    {
        sceneController.NextScene();
    }
}
