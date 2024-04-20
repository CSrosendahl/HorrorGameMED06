using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private int id = -1; //character ID
    [SerializeField] private string displayName = "New Display Name"; //display name of the character
    [SerializeField] private Sprite icon; //Prety icon for the character. bonus point if it looks like a mugshot
    [SerializeField] private GameObject introPrefab; //Prefab to display in the character select screen
    [SerializeField] private NetworkObject gameplayPrefab; //Prefab to spawn in the game
    [SerializeField] private bool isHuman; // are we human?
    [SerializeField] private bool isMonster; // or are we dancer aka monster?

    //Make sure to add getters for all the fields. 
    public int Id => id;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
    public GameObject IntroPrefab => introPrefab;
    public NetworkObject GameplayPrefab => gameplayPrefab;
    public bool IsHuman => isHuman;
    public bool IsMonster => isMonster;
}
