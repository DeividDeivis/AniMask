using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string objID = "Test Object";
    [SerializeField] private Button objButton;
    [SerializeField] private Image objImage;
    public string ObjectID => objID;

    public Action<string> OnInteraction;

    private void OnEnable()
    {
        objButton.onClick.AddListener(ObjectPressed);
    }

    private void OnDisable()
    {
        objButton.onClick.RemoveListener(ObjectPressed);
    }

    private void ObjectPressed() 
    {
        OnInteraction?.Invoke(objID);
    }

    public void ChangeImg(Sprite newSprite) 
    {
        objImage.sprite = newSprite;
    }
}
