using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eight : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float speed = 1f;
    public float lenth = 1f;
    public float width = 1f;
    private float t = 0f;
    private float dis;
    void Update()
    {
        dis = Vector3.Distance(target1.position, target2.position) / 10;
        t += Time.deltaTime * speed;

        float x = Mathf.Sin(t);
        float y = Mathf.Sin(t) * Mathf.Cos(t);

        Vector3 midpoint = (target1.position + target2.position) / 2;
        Vector3 direction = (target2.position - target1.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rotation *= Quaternion.Euler(0, 90, 0); 
        Vector3 localPosition = new Vector3(x * lenth, 0, y * width) * dis;
        Vector3 worldPosition = midpoint + rotation * localPosition;

        transform.position = worldPosition;
    }
}