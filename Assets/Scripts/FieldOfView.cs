using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Mesh mesh;
    [SerializeField] private LayerMask layerMask;
    private Vector3 origin;
    private float startingAngle;
    [SerializeField ]private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private int rayCount = 50;


    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }



    private void FixedUpdate()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;


        Vector3[] vertecies = new Vector3[rayCount +1 + 1];
        Vector2[] uv = new Vector2[vertecies.Length];
        int[] triangles = new int[rayCount * 3];


        vertecies[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i ++)
        {
            Vector3 vertex;

            RaycastHit hitSomething;
              var hit = Physics.Raycast(origin, GetVectorFromAngle(angle), out hitSomething, viewDistance, layerMask);
            if(!hit)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else
            {
                vertex = hitSomething.point;
            }

            vertecies[vertexIndex] = vertex;

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




        mesh.vertices = vertecies;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        mesh.RecalculateNormals();
        



    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void SetDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }
  

}
