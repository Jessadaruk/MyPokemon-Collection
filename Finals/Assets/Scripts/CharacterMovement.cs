using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("ตั้งค่าการเดิน")]
    public float speed = 5f; 
    public float turnSpeed = 10f; // เพิ่มความเร็วในการหมุนตัว (หันหน้า)

    [Header("อ้างอิงกล้อง")]
    public Transform cam; // ช่องสำหรับดึงกล้องมาอ้างอิงทิศทาง

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        
        // ถ้ายืมลืมลากกล้องมาใส่ ระบบจะหา Main Camera ให้อัตโนมัติ
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical");     

        // 1. หาว่าตอนนี้กล้องหันไปทางไหน (หน้า, ขวา)
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // ล็อคแกน Y ไว้ เพื่อไม่ให้ตัวละครพยายามเดินมุดดินหรือลอยขึ้นฟ้าตามมุมกล้อง
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // 2. คำนวณทิศทางการเดิน โดยอิงจากมุมกล้อง
        Vector3 movementDirection = (camForward * moveVertical + camRight * moveHorizontal).normalized;

        // 3. ตรวจสอบว่ามีการกดปุ่มให้เดินหรือไม่
        if (movementDirection.magnitude >= 0.1f)
        {
            // --- ส่วนที่ทำให้ตัวละครหันหน้า ---
            // คำนวณองศาว่าต้องหันหน้าไปทางไหน
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            // หมุนตัวละครอย่างนุ่มนวล (Slerp) ตามความเร็ว turnSpeed
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);

            // สั่งให้เดินไปในทิศทางนั้น
            rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);

            // เล่นอนิเมชันเดิน
            if (anim != null) anim.SetBool("isWalking", true);
        }
        else
        {
            // ถ้าปล่อยปุ่ม สั่งให้หยุดเล่นอนิเมชัน
            if (anim != null) anim.SetBool("isWalking", false);
        }
    }
}