using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    private Material mat;
    private Vector2 offset;

    void Start()
    {
        // Lấy component Renderer để can thiệp vào Material của Quad
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Tính toán độ lệch trục X dựa trên thời gian và tốc độ
        offset.x += scrollSpeed * Time.deltaTime;
        
        // Cập nhật offset cho Material để tạo hiệu ứng cuộn
        mat.mainTextureOffset = offset;
    }
}