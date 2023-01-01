using System.Collections;
using System.Collections.Generic;
using TalesOfTribute;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CardUIButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Card _card;
    public GameObject Cost;
    public GameObject Name;
    public GameObject Type;
    public GameObject Effects;
    public GameObject checkmark;
    public Sprite[] CardSprites;

    public void OnClick()
    {
        checkmark.SetActive(!checkmark.activeSelf);
        if (checkmark.activeSelf)
            FindObjectOfType<CardChoiceUIScript>().SelectCard(_card);
        else
            FindObjectOfType<CardChoiceUIScript>().UnSelectCard(_card);
    }

    public void SetUpCardInfo(Card card)
    {
        _card = card;
        GetComponent<Image>().sprite = CardSprites.First(sprite => sprite.name == CardScript.ParseDeckAndType(_card));
        Cost.GetComponent<TextMeshProUGUI>().SetText(card.Cost.ToString());
        Name.GetComponent<TextMeshProUGUI>().SetText(card.Name);
        Type.GetComponent<TextMeshProUGUI>().SetText(TypeToString(card.Type));

        string effects = "";

        for (int i = 0; i < card.Effects.Length; i++)
        {
            if (i == 0 && card.Effects[0] != null) //Activation
            {
                effects += $"{card.Effects[i].ToString()}\n";
            }
            else if (card.Effects[i] != null)
            {
                effects += $"Combo {i + 1}: {card.Effects[i].ToString()}\n";
            }
        }

        Effects.GetComponent<TextMeshProUGUI>().SetText(effects);
        if (effects.Length > 110)
        {
            Effects.GetComponent<TextMeshProUGUI>().fontSize = 10f;
        }
        if (effects.Length > 130)
        {
            Effects.GetComponent<TextMeshProUGUI>().fontSize = 9f;
        }
        if (effects.Length > 150)
        {
            Effects.GetComponent<TextMeshProUGUI>().fontSize = 8f;
        }
    }

    public Card GetCard()
    {
        return _card;
    }

    private string TypeToString(CardType type)
    {
        return type switch
        {
            CardType.AGENT => "Agent",
            CardType.CURSE => "Curse",
            CardType.CONTRACT_AGENT => "Contract Agent",
            CardType.CONTRACT_ACTION => "Contract Action",
            CardType.STARTER => "Starter",
            CardType.ACTION => "Action",
            _ => ""
        };
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale *= 1.5f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale /= 1.5f;
    }
}