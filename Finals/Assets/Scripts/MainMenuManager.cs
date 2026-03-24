using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อใช้คำสั่งเปลี่ยน Scene

public class MainMenuManager : MonoBehaviour
{
    // ฟังก์ชันสำหรับกดปุ่มเริ่มเกม
    public void PlayGame()
    {
        // สั่งให้โหลดฉากที่ชื่อว่า "Level1" (ถ้าฉากเกมคุณชื่ออื่น ให้แก้ตรงนี้ให้ตรงเป๊ะนะครับ เช่น "SampleScene")
        SceneManager.LoadScene("Level1"); 
    }

    // ฟังก์ชันสำหรับกดปุ่มออกเกม
    public void QuitGame()
    {
        Debug.Log("ออกจากการเล่นเกมแล้ว!"); // ใช้ดูใน Unity ว่าปุ่มทำงาน
        Application.Quit(); // คำสั่งนี้จะทำงานจริงตอนที่เรา Build เกมเป็น .exe แล้ว
    }
}