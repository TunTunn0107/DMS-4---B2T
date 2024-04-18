using System.Collections;
using UnityEngine;

public class autodoor : MonoBehaviour
{
    public Animator doorAnim;
    public float delay = 5f; // Thời gian chờ trước khi đóng cửa, có thể điều chỉnh trong Unity Editor

    private bool isDoorOpen = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            isDoorOpen = true;
            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isDoorOpen)
        {
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    IEnumerator CloseDoorAfterDelay()
    {
        // Chờ đợi theo thời gian delay đã thiết lập
        yield return new WaitForSeconds(delay);
        if (isDoorOpen) // Kiểm tra lại để chắc chắn cửa vẫn cần đóng
        {
            doorAnim.ResetTrigger("open");
            doorAnim.SetTrigger("close");
            isDoorOpen = false; // Cập nhật trạng thái cửa
        }
    }
}
