using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class myFieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    Vector3 origin;
    private float fov;
    private float startingAngle;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 90f;

    }

    private void Update()
    {
        
        int raycount = 100;
        float angle = startingAngle;
        float angleIncrease = fov / raycount;
        float viewDistance = 5f;



        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= raycount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }



            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;


                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }


  

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void setOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDir(Vector3 aimDir)
    {
        startingAngle = GetAngleFromVectorFloat(aimDir) + fov / 2f;
    }



    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
