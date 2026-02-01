using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SceneInfo : MonoBehaviour
{
    [SerializeField] private List<SceneObjectInfo> objectsInScene = new List<SceneObjectInfo>();

    [SerializeField] private List<DialogueInfo> sceneDialogues = new List<DialogueInfo>();
    private int dialogueIndex = 0;

    [SerializeField] private bool sceneComplete = false;
    public static Action OnSceneComplete;

    public void SetUpScene() 
    {
        dialogueIndex = 0;
        sceneComplete = false;

        DialogueSystem.OnNextDialogueClick += () => TryShowNextDialogue(string.Empty);

        foreach (var obj in objectsInScene)
        {
            obj.alreadyInteracted = false;
            obj.interactableObject.OnInteraction += obj.AlreadyInteracted;
            obj.interactableObject.OnInteraction += TryShowNextDialogue;
            obj.objectID = obj.interactableObject.ObjectID;
        }

        ShowDialogue();
    }

    public void EndUpScene() 
    {
        DialogueSystem.OnNextDialogueClick -= () => TryShowNextDialogue(string.Empty);

        foreach (var obj in objectsInScene) 
        {
            obj.interactableObject.OnInteraction -= obj.AlreadyInteracted;
            obj.interactableObject.OnInteraction += TryShowNextDialogue;
        }                  
    }

    private void ShowDialogue() 
    {
        DialogueInfo currentDialogue = sceneDialogues[dialogueIndex];
        DialogueSystem.Instance.ShowDialogue(currentDialogue);

        dialogueIndex++;

        if(dialogueIndex >= sceneDialogues.Count)
            sceneComplete = true;
    }

    public void TryShowNextDialogue(string DialogueTrigger) 
    {
        if (sceneComplete) 
        {
            DialogueSystem.Instance.CloseDialogue();
            OnSceneComplete?.Invoke();
            return;
        }

        DialogueInfo nextDialogue = sceneDialogues[dialogueIndex];

        //Conditions check
        bool triggerCondition = true;
        bool interactionsCondition = true;

        // Si no hay trigger condition o si son iguales
        if (nextDialogue.NecessaryTrigger != string.Empty && nextDialogue.NecessaryTrigger != DialogueTrigger)         
            triggerCondition = false;

        if (nextDialogue.NecessaryInteractions.Count > 0) 
        {
            foreach (var interaction in nextDialogue.NecessaryInteractions)
            {
                SceneObjectInfo sceneObject = objectsInScene.First(obj => obj.objectID == interaction);
                if (!sceneObject.alreadyInteracted)                 
                    interactionsCondition = false;                   
            }
        }

        if (triggerCondition && interactionsCondition)
            ShowDialogue();
        else
            DialogueSystem.Instance.CloseDialogue();
    }
}

[Serializable]
public class SceneObjectInfo 
{
    public string objectID = string.Empty; // ID del objeto interactuable
    public InteractableObject interactableObject; //
    public bool alreadyInteracted = false;

    public void AlreadyInteracted(string objID) { alreadyInteracted = true; }
}

[Serializable]
public class DialogueInfo 
{
    [Header("Speaker Info")]
    public Sprite avatarSprite; // Imagen del avatar que habla si esta vacia se obvia.
    public string avatarName = string.Empty;   // Nombre del avatar que habla si esta vacio se obvia.
    [TextArea(3, 5)]public string dialogue = string.Empty; // No dejen vacio este XD

    [Header("Dialogue Sfx")]
    public AudioClip dialogueSfx; // Sirve para voces pero tambien para cualquier sfx.

    [Header("Object Showed in screen")]
    public Sprite objectInFront; // Imagen del objeto interactuado si esta vacia se obvia.

    [Header("Dialogue Conditions")]
    public string NecessaryTrigger = string.Empty; // Objeto con el que hay que interactuar para que este dialogo se muestre.
    public List<string> NecessaryInteractions = new List<string>(); // Interacciones necesarias para que este dialogo se muestre.

    [Header("Dialogue Event")]
    public UnityEvent dialogueEvent; // Eventos que suceden cuando este dialogo se muestre.
}
