using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animationSpeed;
    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // background moves downwards
    private void Update(){
        meshRenderer.material.mainTextureOffset += new Vector2(0, animationSpeed * Time.deltaTime);
    }
}
