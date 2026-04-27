using UnityEngine;

public class ScreenDetector : MonoBehaviour
{
    void OnBecameInvisible() {
        Destroy(transform.parent.gameObject);
    }
}
