using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

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
        sceneController.SetScene(0);
        SceneInfo.OnSceneComplete += SceneComplete;
    }

    private void SceneComplete() 
    {
        sceneController.NextScene();
    }
}
