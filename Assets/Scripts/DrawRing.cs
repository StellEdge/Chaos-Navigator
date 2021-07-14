using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRing : MonoBehaviour
{
    public Material material;
    public FloatReference radius;

    // Start is called before the first frame update
    void Start()
    {
        Draw(gameObject, transform.position + new Vector3(0.0f, 0.01f, 0.0f), 40, radius, 0.1f, material);
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 画圆环
    /// </summary>
    /// <param name="gameObject">依附对象</param>
    /// <param name="center">中心点</param>
    /// <param name="segments">分割段数</param>
    /// <param name="innerRadius">内径</param>
    /// <param name="thickness">厚度，向外扩的宽度</param>
    /// <param name="material">材质</param>
    public static void Draw(GameObject gameObject, Vector3 center, int segments,
        float innerRadius, float thickness, Material material)

    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        //gameObject.GetComponent<MeshRenderer>().material;
        gameObject.GetComponent<MeshRenderer>().material = material;
        
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //分割16块，共有32个点，96 三角形
        Vector3[] innerVertices = GetVerticesFromCenter(center, segments, innerRadius);
        Vector3[] outerVertices = GetVerticesFromCenter(center, segments, innerRadius + thickness);
        List<Vector3> list = new List<Vector3>(outerVertices);
        list.AddRange(innerVertices);
        var bounds = list.ToArray();

        //设置顶点
        mesh.vertices = bounds;

        int[] array = new int[segments * 2 * 3];

        //        Debug.Log("bounds.Length-1="+(bounds.Length-1));
        for (int i = 0, j = 0; j < segments; i += 6, j++)
        {
            if (j != segments - 1)
            {
                array[i] = j;
                array[i + 1] = j + 1;
                array[i + 2] = segments + 1 + j;

                array[i + 3] = j;
                array[i + 4] = segments + 1 + j;
                array[i + 5] = segments + j;
            }
            //最后一对三角形时，需要单独处理
            else
            {
                array[i] = j;
                array[i + 1] = 0;
                array[i + 2] = segments;

                array[i + 3] = j;
                array[i + 4] = segments;
                array[i + 5] = segments + j;
            }

            //            Debug.Log("i="+i+","+"j="+j+"|"+array[i]+","+array[i+1]+","+array[i+2]+","+array[i+3]+","+array[i+4]+","+array[i+5]);
        }

        mesh.triangles = array;
    }


    /// <summary>
    /// 根据圆心获取获取所有点的数组
    /// </summary>
    /// <param name="center"></param>
    /// <param name="segments"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private static Vector3[] GetVerticesFromCenter(Vector3 center, int segments, float radius)
    {
        //内
        Vector3[] points = new Vector3[segments];

        //每一份的角度
        float angle = Mathf.Deg2Rad * 360f / segments;

        Debug.Log("angle=" + angle);

        for (int i = 0; i < segments; i++)
        {
            //计算x点和z点，内圈
            float inX = center.x + radius * Mathf.Sin(angle * i);
            float inZ = center.z + radius * Mathf.Cos(angle * i);
            points[i] = new Vector3(inX, center.y, inZ);

            //            DrawCircle(new GameObject("circle " + i), 0.01f, 20, points[i],
            //                Resources.Load<Material>("Materials/GreenLine"));
        }

        return points;
    }

}
