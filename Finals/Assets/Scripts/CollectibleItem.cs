using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // ถ้าคนที่มาชนมี Tag ว่า Player
        if (other.CompareTag("Player"))
        {
            // ค้นหา LevelManager ในฉาก (สำหรับ Unity 6 ใช้ FindFirstObjectByType)
            LevelManager manager = Object.FindFirstObjectByType<LevelManager>();
            
            if (manager != null)
            {
                manager.AddItem(); // สั่งให้บวกคะแนน
            }
            
            // ทำลายตัวเอง (ทำให้ไอเทมหายไปจากจอ)
            Destroy(gameObject);
        }
    }
}