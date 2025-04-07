using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainCard : MonoBehaviour
{
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject cardBack;

    private Sprite _image;
    private int _id;
    private SpriteRenderer spriteRenderer;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public Sprite Image
    {
        get => _image;
        set
        {
            _image = value;
            spriteRenderer.sprite = _image;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;

        if (cardBack != null)
        {
            cardBack.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    public void SetCard(int id, Sprite image)
    {
        Id = id;
        Image = image;
    }

    private void OnMouseDown()
    {
        if (cardBack != null && cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        if (cardBack != null)
        {
            cardBack.SetActive(true);
        }
    }
}
