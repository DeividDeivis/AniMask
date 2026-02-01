using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInfo : MonoBehaviour
{
    [SerializeField] private GameObject sceneUIContainer;
    [SerializeField] private List<DialogueInfo> sceneDialogues = new List<DialogueInfo>();

    [SerializeField] private List<SceneObjectInfo> objectsInScene = new List<SceneObjectInfo>();
}

[Serializable]
public class SceneObjectInfo 
{
    public string objectID;
    public InteractableObject interactableObject;
    public bool alreadyInteracted = false;
}

[Serializable]
public class DialogueInfo 
{
    [Header("Speaker Info")]
    public Sprite avatarSprite;
    public string avatarName;
    public string dialogue;

    [Header("Object Info")]
    public Sprite objectInFront;

    [Header("Dialogue Event")]
    public UnityEvent dialogueEvent;
}
