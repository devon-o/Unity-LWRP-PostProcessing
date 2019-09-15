using UnityEngine;

public class AnimatedLight : MonoBehaviour
{
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float maxY = 10f;

    private float speedX, speedY, angleX, angleY;

    private void Start()
    {
        speedX = Random.Range(-5, 5);
        speedY = Random.Range(-5, 5);
        angleX = 0f;
        angleY = 0f;
    }

    private void Update()
    {
        var pos = transform.position;

        pos.x = Mathf.Sin(angleX) * maxX;
        pos.y = Mathf.Cos(angleY) * maxY;

        transform.position = pos;

        angleX += speedX * Mathf.Deg2Rad;
        angleY += speedY * Mathf.Deg2Rad;
    }
}
