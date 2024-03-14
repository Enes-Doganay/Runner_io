using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Data/SkinDataBase")]
public class SkinDataBase: ScriptableObject
{
    public List<CharacterSkin> characterSkin;

    public CharacterSkin GetRandomCharacterSkin()
    {
        int randomIndex = Random.Range(0, characterSkin.Count);
        return characterSkin[randomIndex];
    }
}

[System.Serializable]
public struct CharacterSkin
{
    public int SkinID;
    public List<Color> SkinColors;
}
