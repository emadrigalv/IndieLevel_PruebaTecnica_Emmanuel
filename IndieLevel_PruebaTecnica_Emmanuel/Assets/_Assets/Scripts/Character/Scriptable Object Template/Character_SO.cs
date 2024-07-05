using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character Data")]
public class Character_SO: ScriptableObject
{
    public int maxHealth;
    public int attackDamage;
    public float movementSpeed;
}
