using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "OurFolder/Characters/Character")]

public class Character : ScriptableObject
{
    [SerializeField] private int id = -1;
    [SerializeField] private string displayName = "New Display Name";
    [SerializeField] private Sprite icon; 

    public int ID => id;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
}
