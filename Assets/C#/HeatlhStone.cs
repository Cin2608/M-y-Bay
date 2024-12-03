using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HealthStone : MonoBehaviour
{
    public int BaseHealth;
    public int MaxHealth;
    public GameObject ExplosionPrefab;
    public GameObject[] ItemPrefabs;

    public void StoneDmg(int DmgGiven)
    {
        BaseHealth -= DmgGiven;
        if (BaseHealth <= 0)
        {
            Destroy(gameObject);
            DestroyStoneWithExplosion();
        }
    }
    void DestroyStoneWithExplosion()
    {
        GameObject Explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(Explosion, 1f);
        SpawnRandomItem();
        Destroy(gameObject);
    }

    void SpawnRandomItem()
    {
        // Kiểm tra nếu có vật phẩm để spawn
        if (ItemPrefabs.Length > 0)
        {
            // Chọn ngẫu nhiên một vật phẩm từ mảng
            int randomIndex = Random.Range(0, ItemPrefabs.Length);
            GameObject item = ItemPrefabs[randomIndex];

            // Spawn vật phẩm tại vị trí của đá nổ
            Vector3 spawnPosition = transform.position + new Vector3(0, 2f, 0); // Đặt vật phẩm cách viên đá một chút
            GameObject spawnedItem = Instantiate(item, spawnPosition, Quaternion.identity);

            // Thêm Rigidbody2D vào vật phẩm để nó rơi xuống
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f; // Đảm bảo có trọng lực cho vật phẩm
            }
        }
    }
}
