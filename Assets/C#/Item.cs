using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu vật phẩm va chạm với tàu
        if (other.CompareTag("AirPlane"))
        {
            // Nếu va chạm với tàu, gọi phương thức EatItem từ script tàu
            AirPlane Air = other.GetComponent<AirPlane>();
            if (Air != null)
            {
                Air.EatItem(gameObject);  // Gọi phương thức EatItem từ tàu
            }
        }
    }
}
