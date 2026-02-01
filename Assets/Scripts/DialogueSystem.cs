using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueContainer;

    [Header("Simple Dialogues Settings")]
    [SerializeField] private GameObject textContainer;

    [SerializeField] private GameObject nameContainer;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image avatarImg;

    [SerializeField] private Button nextDialogue;

    [SerializeField] private AudioSource SfxSource;

    [SerializeField] private Image ObjectInFrontImg;

    [Header("Fork Dialogues Settings")]
    [SerializeField] private GameObject forkContainer;

    [SerializeField] private GameObject forkNameContainer;
    [SerializeField] private TextMeshProUGUI forkNameText;
    [SerializeField] private Image forkAvatarImg;

    [SerializeField] private Button forkDialogueBtn1;
    [SerializeField] private TextMeshProUGUI forkDialogueText1;
    [SerializeField] private Button forkDialogueBtn2;
    [SerializeField] private TextMeshProUGUI forkDialogueText2;

    [SerializeField] private Button forkNextDialogue;

    public static Action<int> OnChoiceClick;

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

        forkNextDialogue.onClick.AddListener(() => OnNextDialogueClick?.Invoke());

        forkDialogueBtn1.onClick.AddListener(() => OnChoiceClick?.Invoke(0));
        forkDialogueBtn2.onClick.AddListener(() => OnChoiceClick?.Invoke(1));
    }

    #region Normal Dialogues
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

        if (dialogueInfo.dialogueSfx != null)
        {
            SfxSource.clip = dialogueInfo.dialogueSfx;
            SfxSource.Play();
        }

        dialogueContainer.SetActive(true);
        textContainer.SetActive(true);
        // Animacion de caja de dialogo

        dialogueInfo.dialogueEvent?.Invoke();
    }
    #endregion

    public void CloseDialogue() 
    {
        dialogueContainer.SetActive(false);
        textContainer.gameObject.SetActive(false);
        forkContainer.gameObject.SetActive(false);
    }

    public void CloseForkDialogue()
    {
        forkNextDialogue.gameObject.SetActive(false); // boton siguiente del fork dialogue.
        nextDialogue.gameObject.SetActive(true);

        forkContainer.gameObject.SetActive(false);
        //dialogueContainer.SetActive(false);
    }

    #region Fork Dialogues

    public void ShowForkChoices(ForkInfo forkInfo, List<ChoiceInfo> forkChoices) 
    {
        textContainer.gameObject.SetActive(false);  // Apago la opcion comun de dialogos.
        nextDialogue.gameObject.SetActive(false); // Se desactiva el boton comun de siguiente.

        forkNextDialogue.gameObject.SetActive(false); // boton siguiente del fork dialogue.

        SfxSource.Stop(); // Para un Sfx previo si habia uno.

        forkNameText.text = forkInfo.avatarName;
        bool showName = forkInfo.avatarName == string.Empty ? false : true;
        forkNameContainer.SetActive(showName);

        forkDialogueText1.text = forkChoices[0].choiceText;
        forkDialogueText2.text = forkChoices[1].choiceText;

        forkAvatarImg.sprite = forkInfo.avatarSprite;
        forkAvatarImg.enabled = forkInfo.avatarSprite != null ? true : false;

        ObjectInFrontImg.sprite = forkInfo.objectInFront;
        ObjectInFrontImg.enabled = forkInfo.objectInFront != null ? true : false;

        if (forkInfo.dialogueSfx != null)
        {
            SfxSource.clip = forkInfo.dialogueSfx;
            SfxSource.Play();
        }

        forkContainer.gameObject.SetActive(true);   // Enciendo la opcion de fork de dialogos
        // Animacion de cajas de dialogos
    }


    public void ShowChoiceDialogue(ForkDialogueInfo choiceInfo) 
    {
        forkContainer.gameObject.SetActive(false);
        forkNextDialogue.gameObject.SetActive(true);

        SfxSource.Stop(); // Para un Sfx previo si habia uno.

        nameText.text = choiceInfo.avatarName;
        bool showName = choiceInfo.avatarName == string.Empty ? false : true;
        nameContainer.SetActive(showName);

        dialogueText.text = choiceInfo.dialogue;

        avatarImg.sprite = choiceInfo.avatarSprite;
        avatarImg.enabled = choiceInfo.avatarSprite != null ? true : false;

        ObjectInFrontImg.sprite = choiceInfo.objectInFront;
        ObjectInFrontImg.enabled = choiceInfo.objectInFront != null ? true : false;

        if (choiceInfo.dialogueSfx != null)
        {
            SfxSource.clip = choiceInfo.dialogueSfx;
            SfxSource.Play();
        }

        dialogueContainer.SetActive(true);
        textContainer.gameObject.SetActive(true);   // Enciendo la opcion de fork de dialogos
        // Animacion de cajas de dialogos
    }
    #endregion
}
