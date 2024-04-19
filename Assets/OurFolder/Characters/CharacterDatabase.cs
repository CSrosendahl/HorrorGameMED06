using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "OurFolder/Characters/CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] private Character[] characters = new Character[0];

    public Character[] GetAllCharacters() => characters;
    public Character GetCharacterById(int id)
    {
        foreach (Character character in characters)
        {
            if (character.ID == id)
            {
                return character;
            }
        }
        return null;
    }
}
