using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AirPlane : MonoBehaviour
{
    public float Speed;
    private float MvInput;
    private bool IsFacingRight = true;
    public float LaserSpeed;

    public Transform FirePoint;
    public GameObject LaserPrefab;

    public float timeBetweenShots;  // Thời gian giữa các lần bắn (1.5 giây)
    private float lastShotTime = 0f;  // Lưu trữ thời gian của lần bắn cuối

    private bool Shoot2 = false;
    public Rigidbody2D rg;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MvInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector2.right * MvInput * Speed * Time.deltaTime);

        if (MvInput > 0 && !IsFacingRight)
        {
            Flip();
        }
        else if (MvInput < 0 && IsFacingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.Z) && Time.time - lastShotTime >= timeBetweenShots)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }
    void Shoot()
    {
        if (Shoot2)
        {
            ShootTwo();
        }
        else
        {
            ShootOne();
        }
    }
    void ShootOne()
    {
        Vector2 laserDirection = Vector2.up;

        GameObject laser = Instantiate(LaserPrefab, FirePoint.position, Quaternion.identity);

        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        if (laserRb != null)
        {
            laserRb.velocity = laserDirection * LaserSpeed; // Điều chỉnh LaserTime cho tốc độ laser
        }

    }
    void ShootTwo()
    {
        Debug.Log("Shooting two lasers...");
        Vector2 laserLeftDirection = Vector2.up;
        GameObject laserLeft = Instantiate(LaserPrefab, FirePoint.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
        Rigidbody2D laserLeftRb = laserLeft.GetComponent<Rigidbody2D>();
        if (laserLeftRb != null)
        {
            laserLeftRb.velocity = laserLeftDirection * LaserSpeed; // Điều chỉnh LaserTime cho tốc độ laser
        }

        Vector2 laserRightDirection = Vector2.up;
        GameObject laserRight = Instantiate(LaserPrefab, FirePoint.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
        Rigidbody2D laserRightRb = laserRight.GetComponent<Rigidbody2D>();
        if (laserRightRb != null)
        {
            laserRightRb.velocity = laserRightDirection * LaserSpeed; // Điều chỉnh LaserTime cho tốc độ laser
        }
    }
    //void ShootThree()
    //{
    //    Debug.Log("Shooting 3 lasers...");
    //    Vector2 laserLeftDirection = new Vector2(-0.8f, 1f).normalized;
    //    GameObject laserLeft = Instantiate(LaserPrefab, FirePoint.position + new Vector3(-2f, 0, 0), Quaternion.identity);
    //    Rigidbody2D laserLeftRb = laserLeft.GetComponent<Rigidbody2D>();
    //    if (laserLeftRb != null)
    //    {
    //        laserLeftRb.velocity = laserLeftDirection * LaserSpeed; // Điều chỉnh LaserTime cho tốc độ laser
    //    }


    //    Vector2 laserMidDirection = Vector2.up;
    //    GameObject laserMid = Instantiate(LaserPrefab, FirePoint.position, Quaternion.identity);
    //    Rigidbody2D laserMidRb = laserMid.GetComponent<Rigidbody2D>();
    //    if (laserMidRb != null)
    //    {
    //        laserMidRb.velocity = laserMidDirection * LaserSpeed; // Điều chỉnh LaserTime cho tốc độ laser
    //    }


    //    Vector2 laserRightDirection = new Vector2(0.8f, 1f).normalized;
    //    GameObject laserRight = Instantiate(LaserPrefab, FirePoint.position + new Vector3(2f, 0, 0), Quaternion.identity);
    //    Rigidbody2D laserRightRb = laserRight.GetComponent<Rigidbody2D>();
    //    if (laserRightRb != null)
    //    {
    //        laserRightRb.velocity = laserRightDirection * LaserSpeed; // Điều chỉnh LaserTime cho tốc độ laser
    //    }
    //}
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 Scale = transform.localScale; // Lấy scale hiện tại
        Scale.x *= -1; // Đảo chiều x
        transform.localScale = Scale; // Áp dụng scale mới
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            EatItem(other.gameObject);
        }
    }
    public void EatItem(GameObject laser)
    {
        // Bạn có thể xử lý vật phẩm ở đây (ví dụ: tăng điểm, sức khỏe, v.v.)
        Debug.Log("Item eaten!");
        Shoot2 = true;
        Destroy(laser);
    }
}
