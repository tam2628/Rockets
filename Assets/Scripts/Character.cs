using UnityEngine;
[CreateAssetMenu(fileName = "New Character", menuName = "New Character")]
public class Character: GenericScriptableObject
{
    public string characterType;
    public GameObject characterPrefab;
    public Material charcterBackgroundMaterial;
}