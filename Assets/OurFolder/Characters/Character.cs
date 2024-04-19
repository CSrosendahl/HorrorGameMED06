using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private int ID = -1;
    [SerializeField] private string displayName = "New Display Name";
    [SerializeField] private Sprite icon;
  
    [SerializeField] private NetworkObject gameplayPrefab;

    public int Id => ID;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
   
    public NetworkObject GameplayPrefab => gameplayPrefab;
}
