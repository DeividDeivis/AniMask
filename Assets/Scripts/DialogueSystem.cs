using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private GameObject nameContainer;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image avatarImg;
    [SerializeField] private Button nextDialogue;

    [SerializeField] private AudioSource SfxSource;

    [SerializeField] private Image ObjectInFrontImg;

    public static Action OnNextDialogueClick;

    #region Singleton
    private static DialogueSystem _instance;
    public static DialogueSystem Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueContainer.SetActive(false);
        nextDialogue.onClick.AddListener(()=> OnNextDialogueClick?.Invoke());
    }

    public void ShowDialogue(DialogueInfo dialogueInfo) 
    {
        SfxSource.Stop(); // Para un Sfx previo si habia uno.

        nameText.text = dialogueInfo.avatarName;
        bool showName = dialogueInfo.avatarName == string.Empty ? false : true;
        nameContainer.SetActive(showName);

        dialogueText.text = dialogueInfo.dialogue;

        avatarImg.sprite = dialogueInfo.avatarSprite;
        avatarImg.enabled = dialogueInfo.avatarSprite != null ? true : false;

        ObjectInFrontImg.sprite = dialogueInfo.objectInFront;
        ObjectInFrontImg.enabled = dialogueInfo.objectInFront != null ? true : false;

        SfxSource.clip = dialogueInfo.dialogueSfx;
        if (dialogueInfo.dialogueSfx != null)
        {
            SfxSource.clip = dialogueInfo.dialogueSfx;
            SfxSource.Play();
        }

        dialogueContainer.SetActive(true);
        // Animacion de caja de dialogo

        dialogueInfo.dialogueEvent?.Invoke();
    }

    public void CloseDialogue() 
    {
        dialogueContainer.SetActive(false);
    }
}
