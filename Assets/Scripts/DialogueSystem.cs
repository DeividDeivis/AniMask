using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image avatarImg;
    [SerializeField] private Button nextDialogue;

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
        nextDialogue.onClick.AddListener(CloseDialogue);
    }

    public void ShowDialogue(Sprite avatar, string dialogue) 
    {
        dialogueText.text = dialogue;
        avatarImg.sprite = avatar;
        dialogueContainer.SetActive(true);
    }

    private void CloseDialogue() 
    {
        dialogueContainer.SetActive(false);
    }
}
