using UnityEngine;

public class StartButton : MonoBehaviour
{
    private void Start()
    {
        // Make sure it's visible at scene start
        gameObject.SetActive(true);
    }

    public void OnStartClicked()
    {
        // Hide button when game starts
        gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }
}