using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("ตั้งค่าเป้าหมาย")]
    public Transform target; // ลากตัว Player มาใส่ช่องนี้

    [Header("ตั้งค่าระยะห่าง")]
    public float distance = 4.0f; // ระยะห่างจากตัวละคร (ซูมเข้า-ออก)
    public Vector3 offset = new Vector3(0, 1.5f, 0); // จุดศูนย์กลางที่กล้องจะมอง (ตั้งไว้ที่ความสูงระดับหัวไหล่/หัว)

    [Header("ความไวของเมาส์")]
    public float sensitivity = 2.0f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    
    // ล็อคมุมก้มเงย เพื่อไม่ให้กล้องมุดดินหรือตีลังกา
    private float minY = -15f; 
    private float maxY = 60f;

    void Start()
    {
        // ซ่อนเมาส์และล็อคไว้กลางจอตอนเริ่มเกม
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ใช้ LateUpdate สำหรับกล้อง เพื่อให้กล้องขยับตาม "หลังจาก" ที่ตัวละครเดินไปแล้ว ภาพจะได้ไม่กระตุก
    void LateUpdate()
    {
        // ถ้ายังไม่ได้ใส่เป้าหมาย ให้หยุดทำงาน
        if (target == null) return;

        // รับค่าการขยับเมาส์
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // ล็อคมุมกล้องแกน Y (ก้ม/เงย)
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // คำนวณทิศทางและการหมุนของกล้อง
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        
        // อัปเดตตำแหน่งกล้อง ให้อยู่ห่างจากตัวละครตามระยะที่กำหนด
        transform.position = target.position + offset + rotation * direction;
        
        // สั่งให้กล้องหันหน้ามองไปที่ตัวละครเสมอ
        transform.LookAt(target.position + offset);
    }
}