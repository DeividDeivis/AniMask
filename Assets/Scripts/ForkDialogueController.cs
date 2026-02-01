using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForkDialogueController : MonoBehaviour
{
    [SerializeField] private ForkInfo forkInfo;
    [SerializeField] private List<ChoiceInfo> choiceDialogues = new List<ChoiceInfo>();
    private int choiceWinner = 0;
    private int choiceDialoguesIndex = 0;
    private bool completeFork = false;

    public void ShowForkEvent() // Call this from Unity Events
    {
        choiceWinner = 0;
        choiceDialoguesIndex = 0;
        completeFork = false;

        DialogueSystem.Instance.ShowForkChoices(forkInfo, choiceDialogues);
        DialogueSystem.OnChoiceClick += ShowWinnerChoice;
        DialogueSystem.OnNextDialogueClick += ShowChoiceDialogue;
    }

    public void ShowWinnerChoice(int choiceIndex) 
    {
        choiceWinner = choiceIndex;
        ShowChoiceDialogue();
    }

    private void ShowChoiceDialogue()
    {
        if (completeFork)
        {
            CloseForkEvent();
            return;
        }

        ForkDialogueInfo currentDialogue = choiceDialogues[choiceWinner].sceneDialogues[choiceDialoguesIndex];
        DialogueSystem.Instance.ShowChoiceDialogue(currentDialogue);

        choiceDialoguesIndex++;

        if (choiceDialoguesIndex >= choiceDialogues[choiceWinner].sceneDialogues.Count)
            completeFork = true;
    }

    private void CloseForkEvent() 
    {
        DialogueSystem.OnNextDialogueClick -= ShowChoiceDialogue;
        DialogueSystem.Instance.CloseForkDialogue();
    }
}

[Serializable]
public class ForkInfo 
{
    [Header("Speaker Info")]
    public Sprite avatarSprite; // Imagen del avatar que habla si esta vacia se obvia.
    public string avatarName = string.Empty;   // Nombre del avatar que habla si esta vacio se obvia.

    [Header("Dialogue Sfx")]
    public AudioClip dialogueSfx; // Sirve para voces pero tambien para cualquier sfx.

    [Header("Object Showed in screen")]
    public Sprite objectInFront; // Imagen del objeto interactuado si esta vacia se obvia.

    [Header("Dialogue Event")]
    public UnityEvent dialogueEvent; // Eventos que suceden cuando este dialogo se muestre.
}

[Serializable]
public class ChoiceInfo 
{
    public string choiceText; // Eleccion para mostrar.
    public List<ForkDialogueInfo> sceneDialogues = new List<ForkDialogueInfo>(); // Diaogos que se muestran si se elige esta eleccion.
}

[Serializable]
public class ForkDialogueInfo
{
    [Header("Speaker Info")]
    public Sprite avatarSprite; // Imagen del avatar que habla si esta vacia se obvia.
    public string avatarName = string.Empty;   // Nombre del avatar que habla si esta vacio se obvia.
    [TextArea(3, 5)] public string dialogue = string.Empty; // No dejen vacio este XD

    [Header("Dialogue Sfx")]
    public AudioClip dialogueSfx; // Sirve para voces pero tambien para cualquier sfx.

    [Header("Object Showed in screen")]
    public Sprite objectInFront; // Imagen del objeto interactuado si esta vacia se obvia.

    [Header("Dialogue Event")]
    public UnityEvent dialogueEvent; // Eventos que suceden cuando este dialogo se muestre.
}
