using UnityEngine;
using UnityEngine.UI;

public class DelayedButton : MonoBehaviour
{
    [Header("ตั้งค่าปุ่มและเวลา")]
    [Tooltip("ลากปุ่ม (Button) ที่ต้องการให้โผล่มาใส่ในช่องนี้")]
    public GameObject nextButton;

    [Tooltip("จำนวนวินาทีที่ต้องการให้หน่วงก่อนปุ่มจะโผล่")]
    public float delayTime = 3f;

    private float appearTime;
    private bool isButtonReady = false;

    void Start()
    {
        // บังคับคืนค่าเวลาเกมให้กลับมาเดินปกติ เผื่อว่าโดนสั่งหยุดมาจากด่านที่แล้ว
        Time.timeScale = 1f;

        // เริ่มต้นด้วยการซ่อนปุ่มเอาไว้ก่อน
        if (nextButton != null)
        {
            nextButton.SetActive(false);
            isButtonReady = false;
        }
        else
        {
            Debug.LogWarning("DelayedButton: คุณยังไม่ได้ลากปุ่มมาใส่ในช่อง Next Button!");
        }

        // คำนวณเวลาที่ปุ่มจะโผล่ โดยใช้ Time.unscaledTime
        // (unscaledTime จะเดินหน้าเสมอ ไม่สนใจว่าเกมจะถูก Pause หรือ Time.timeScale เป็น 0 อยู่)
        appearTime = Time.unscaledTime + delayTime;
    }

    void Update()
    {
        // ถ้าปุ่มยังไม่พร้อมแสดง และเวลาจริงเลยกำหนดที่ตั้งไว้แล้ว
        if (!isButtonReady && Time.unscaledTime >= appearTime)
        {
            ShowButton();
        }
    }

    void ShowButton()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(true);
            isButtonReady = true; // มาร์คไว้ว่าปุ่มแสดงแล้ว จะได้ไม่ทำซ้ำใน Update
        }
    }

    // ฟังก์ชันเสริม: เผื่อต้องการสั่งเริ่มนับเวลาใหม่จากสคริปต์อื่น
    public void ResetTimer()
    {
        isButtonReady = false;
        if (nextButton != null) nextButton.SetActive(false);
        appearTime = Time.unscaledTime + delayTime;
    }
}