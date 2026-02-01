using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<SceneInfo> sceneInfos;
    private int sceneIndex = 0;

    #region Singleton
    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    public void SetScene(int newSceneIndex) 
    {
        sceneIndex = newSceneIndex;

        foreach (var scene in sceneInfos)
        {
            scene.gameObject.SetActive(false);
        }

        sceneInfos[sceneIndex].gameObject.SetActive(true);
        sceneInfos[sceneIndex].SetUpScene();
    }

    public void NextScene() 
    {
        sceneInfos[sceneIndex].gameObject.SetActive(false);
        sceneInfos[sceneIndex].EndUpScene();

        sceneIndex++;

        sceneInfos[sceneIndex].gameObject.SetActive(true);
        sceneInfos[sceneIndex].SetUpScene();
    }
}
