using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage;

    private Rigidbody2D rb;

    void Start()
    {
        // Lấy Rigidbody2D của laser
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stone"))
        {
            // Lấy script Enemy của đối tượng va chạm
            stone Stone = other.gameObject.GetComponent<stone>();

            if (Stone != null)
            {
                HealthStone Health;
                Health = other.gameObject.GetComponent<HealthStone>();
                Health.StoneDmg(damage);
            }
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}