using UnityEngine;

public class CharacterSkinManager : MonoBehaviour
{
    [SerializeField] private SkinDataBase skinData;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    void Awake()
    {
        CharacterSkin skin = skinData.GetRandomCharacterSkin();

        for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++)
        {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            mpb.SetColor("_Color", skin.SkinColors[i]);
            skinnedMeshRenderer.SetPropertyBlock(mpb, i);
        }
    }
}
