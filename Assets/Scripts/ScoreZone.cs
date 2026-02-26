using Unity.Netcode;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only the server should handle scoring
        if (!NetworkManager.Singleton.IsServer) return;

        if (!other.CompareTag("Ball")) return;

        if (CompareTag("LeftScoreZone"))
        {
            GameManager.Instance.ScoreRight();
        }
        else if (CompareTag("RightScoreZone"))
        {
            GameManager.Instance.ScoreLeft();
        }
    }
}