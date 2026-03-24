using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("ตั้งค่าด่าน")]
    public int score = 0;
    public int maxScore = 5;
    public string nextLevelName = "Level2"; // ชื่อ Scene ด่าน 2

    [Header("อ้างอิง UI")]
    public TextMeshProUGUI scoreText;
    public GameObject winPanel;
    // เอา countdownText ออกไปแล้ว

    void Start()
    {
        UpdateScoreUI();
        winPanel.SetActive(false); // ซ่อนหน้าชนะไว้ตอนเริ่ม
    }

    // ฟังก์ชันนี้ถูกเรียกเมื่อผู้เล่นเดินชนไอเทม
    public void AddItem()
    {
        score++;
        UpdateScoreUI();

        // ถ้าเก็บครบตามกำหนด ให้เรียกหน้าจอชนะ
        if (score >= maxScore)
        {
            ShowWinScreen();
        }
    }

    void UpdateScoreUI()
    {
    // โชว์แค่ตัวเลขเพียวๆ เลย
    scoreText.text = score + " / " + maxScore; 
    
    // หรือถ้าอยากให้มีคำภาษาอังกฤษด้วย ก็ใช้แบบนี้แทนได้ครับ:
    // scoreText.text = "Score: " + score + " / " + maxScore;
    }

    // ฟังก์ชันแสดงหน้าจอชนะ
    void ShowWinScreen()
    {
        winPanel.SetActive(true); // โชว์หน้าจอ UI
        
        // **สำคัญมาก:** ปลดล็อคเมาส์และแสดงเมาส์ เพื่อให้ผู้เล่นคลิกปุ่มได้
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ฟังก์ชันนี้จะเอาไว้ผูกกับปุ่ม "ไปด่านต่อไป"
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}