using System.Collections;
using UnityEngine;

public class stone : MonoBehaviour
{
    public GameObject StonePrefab;
    //public GameObject ExplosionPrefab;

    public float MinHeight;
    public float MaxHeight;
    public float TimeDestroy;
    private bool isStoneCreated = false;

    private void Start()
    {
        Invoke("Stone", 2f); // Gọi phương thức Stone sau 2 giây khi bắt đầu
    }

    void Stone()
    {
        if (!isStoneCreated)
        {
            Vector3 Spawn = new Vector3(Random.Range(MinHeight, MaxHeight), 11f);
            GameObject Da = Instantiate(StonePrefab, Spawn, Quaternion.identity);
            Destroy(Da, TimeDestroy);

            isStoneCreated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            isStoneCreated = false;  // Đặt lại giá trị của isStoneCreated để có thể tạo lại viên đá
            Invoke("Stone", 2f);  // Gọi lại phương thức Stone sau khi viên đá bị hủy
        }
    }
}
