using UnityEngine;

public class PlayerProgressTracker : MonoBehaviour
{
    private float lastRoadY = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            float currentRoadY = collision.transform.position.y;
            if (currentRoadY > lastRoadY + 0.5f)
            {
                ScoreManager.Instance.AddScore(1);
                lastRoadY = currentRoadY;
            }
        }
    }
}