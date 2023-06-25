using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    [SerializeField][Range(0, 5f)] float lerpTime;
    [SerializeField] Color color1, color2;
    int colorIndex = 0;
    float time = 0f;
    MeshRenderer mesh;
    void Start()
    {
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
    }
    void Update()
    {
        mesh.material.SetColor("Color_8e6a14ed1e9a4892b25bd472b0e0a78e", Color.Lerp(mesh.material.GetColor("Color_8e6a14ed1e9a4892b25bd472b0e0a78e"), color1, lerpTime * Time.deltaTime));
        mesh.material.SetColor("Color_d69437e260dc4b4da71b6692822dd46f", Color.Lerp(mesh.material.GetColor("Color_d69437e260dc4b4da71b6692822dd46f"), color2, lerpTime * Time.deltaTime));
        time = Mathf.Lerp(time, 1f, lerpTime * Time.deltaTime);
    }
}
