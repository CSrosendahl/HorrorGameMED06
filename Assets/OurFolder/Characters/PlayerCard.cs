using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private GameObject visuals;
    [SerializeField] private Image characterIconImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text CharacterNameText;

    public void UpdateDisplay(CharacterSelectState state)
    {
        if (state.CharacterID != -1)
        {
            var character = characterDatabase.GetCharacterById(state.CharacterID);
            characterIconImage.sprite = character.Icon;
            characterIconImage.enabled = true;
            CharacterNameText.text = character.DisplayName;
        }

        else
        {
            characterIconImage.enabled = false;
        }

        playerNameText.text = $"Player {state.ClientID}";

        visuals.SetActive(true);
    }

    public void DisableDisplay()
    {
        visuals.SetActive(false);
    }
}
