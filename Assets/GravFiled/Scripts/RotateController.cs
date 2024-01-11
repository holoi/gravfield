using UnityEngine;

public class RotateController : MonoBehaviour
{
    public Transform objectToMove;
    public Transform player1;
    public Transform player2;
    public float speed = 1f;
    public float radius = 1f;

    private float _time;

    void Update()
    {
        _time += Time.deltaTime * speed;

        Vector3 center1 = player1.position;
        Vector3 center2 = player2.position;
        Vector3 center = (center1 + center2) / 2;
        Vector3 direction = (center2 - center1).normalized;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up);

        float x = Mathf.Sin(_time) * radius;
        float y = Mathf.Sin(_time) > 0 ? Mathf.Cos(_time) * radius : -Mathf.Cos(_time) * radius;

        objectToMove.position = center + x * direction + y * perpendicular;
    }
}