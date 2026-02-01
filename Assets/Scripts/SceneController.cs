using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<SceneInfo> sceneInfos;
    private int sceneIndex = 0;

    public Action GameOver;

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

        if (sceneIndex < sceneInfos.Count)
        {
            sceneInfos[sceneIndex].gameObject.SetActive(true);
            sceneInfos[sceneIndex].SetUpScene();
        }
        else
        {
            Debug.Log("<color=green>COMPLETASTE EL JUEGO, NO HAY MAS ESCENAS PARA MOSTRAR</color>");
            GameOver?.Invoke();

            DialogueSystem.OnNextDialogueClick += DialogueSystem.Instance.CloseDialogue;
            DialogueSystem.OnNextDialogueClick += DialogueSystem.Instance.CloseForkDialogue;
        }
    }
}
