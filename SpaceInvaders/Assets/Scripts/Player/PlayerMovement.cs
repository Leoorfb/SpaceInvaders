using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1.2f;
    private float speed = 1.2f;
    [SerializeField] InputReader inputReader;
    Vector3 direction = Vector3.zero;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        UpdateSpeed(GameManager.Instance.gameSpeed);
        GameManager.OnGameSpeedValueChangeEvent += UpdateSpeed;
    }

    private void OnDisable()
    {
        GameManager.OnGameSpeedValueChangeEvent -= UpdateSpeed;
    }

    private void FixedUpdate()
    {
        direction.x = (speed * inputReader.MovementAxis);
        _rb.velocity = direction;
    }

    private void UpdateSpeed(float gameSpeed)
    {
        speed = baseSpeed * gameSpeed;
    }
}
