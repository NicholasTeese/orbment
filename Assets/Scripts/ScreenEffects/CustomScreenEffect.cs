using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomScreenEffect : MonoBehaviour
{
    public Material m_EffectMaterial;
    // Use this for initialization

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_EffectMaterial);
    }
}
