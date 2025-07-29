using UnityEngine;
// 此腳本需要 GameObject 上必須掛有 MeshFilter 與 MeshRenderer 元件
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FanShapeDisplay : MonoBehaviour
{
    // 攻擊範圍的半徑，扇形的長度
    public float radius = 3f;

    // 扇形的角度，例如 60 度就是左右各 30 度
    public float angle = 60f;

    // 用幾個三角形切割扇形，越多越平滑
    public int segments = 30;

    // 遊戲一開始時自動產生扇形
    void Start()
    {
        DrawFanShape(); // 呼叫方法畫出扇形
    }

    // 這個方法會動態建立一個扇形的 Mesh
    public void DrawFanShape()
    {
        // 建立新的 Mesh 物件
        Mesh mesh = new Mesh();

        // 扇形會有 (segments + 2) 個頂點：
        // 中心點 + 每個邊緣點
        Vector3[] vertices = new Vector3[segments + 2];

        // 每個小三角形會用 3 個頂點索引組成，總共 segments 個三角形
        int[] triangles = new int[segments * 3];

        // 中心點（原點），是所有三角形共用的其中一點
        vertices[0] = Vector3.zero;

        // 每個切片的角度大小（總角度 ÷ 段數）
        float angleStep = angle / segments;

        // 扇形的一半角度，用來從 -halfAngle 到 +halfAngle 畫線
        float halfAngle = angle / 2;

        // 產生圓弧上的點（從左到右）
        for (int i = 0; i <= segments; i++)
        {
            // 從負角度到正角度掃描整個扇形
            float currentAngle = -halfAngle + angleStep * i;

            // 角度轉換為弧度（C# 的三角函數用的是弧度制）
            float rad = Mathf.Deg2Rad * currentAngle;

            // 計算當前點在圓上的位置（x, y），z 固定為 0
            vertices[i + 1] = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
        }

        // 建立三角形索引資料，每個三角形都由中心點與兩個圓邊點組成
        for (int i = 0; i < segments; i++)
        {
            // 第 i 個三角形使用中心點（索引 0）+ 第 i+1 個邊緣點 + 第 i+2 個邊緣點
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        // 將頂點與三角形資料賦值給 Mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // 把產生好的 Mesh 設定到物件的 MeshFilter 上，這樣才會顯示出來
        GetComponent<MeshFilter>().mesh = mesh;
    }
}