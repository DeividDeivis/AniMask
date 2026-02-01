using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<SceneInfo> sceneInfos;

    #region Singleton
    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    public void ChangeScene(int sceneIndex) 
    {
        foreach (var scene in sceneInfos)
        {
            scene.gameObject.SetActive(false);
        }
        sceneInfos[sceneIndex].gameObject.SetActive(true);
    }
}
