using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    public NetworkVariable<int> LeftScore = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<int> RightScore = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<bool> GameOver = new NetworkVariable<bool>(
        true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] private int pointsToWin = 5;

    [Header("Prefabs")]
    [SerializeField] private GameObject ballPrefab;
    public GameObject paddlePrefab;

    private GameObject ballInstance;

    void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        SpawnBall();

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        // Spawn already connected clients
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            OnClientConnected(client.ClientId);
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (!IsServer) return;

        if (NetworkManager.Singleton.ConnectedClientsList.Count > 2)
            return;

        bool isLeftSide = NetworkManager.Singleton.ConnectedClientsList.Count == 1;

        SpawnPlayerPaddleForClient(clientId, isLeftSide);
    }

    private void SpawnPlayerPaddleForClient(ulong clientId, bool isLeftSide)
    {
        GameObject paddle = Instantiate(paddlePrefab);

        paddle.transform.position = isLeftSide
            ? new Vector2(-7, 0)
            : new Vector2(7, 0);

        NetworkObject netObj = paddle.GetComponent<NetworkObject>();

        // Give ownership to that specific client
        netObj.SpawnAsPlayerObject(clientId, true);
    }

    private void SpawnBall()
    {
        if (ballPrefab == null) return;

        ballInstance = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        ballInstance.GetComponent<NetworkObject>().Spawn();
        ballInstance.GetComponent<BallMovement>().StopBall();
    }

    public void StartGame()
    {
        if (IsServer)
            StartGameInternal();
        else
            StartGameServerRpc();
    }

    [ServerRpc]
    private void StartGameServerRpc()
    {
        StartGameInternal();
    }

    private void StartGameInternal()
    {
        LeftScore.Value = 0;
        RightScore.Value = 0;
        GameOver.Value = false;

        if (ballInstance == null)
            SpawnBall();
        else
            ResetBall(true);
    }

    public void ResetBall(bool leftScored)
    {
        if (ballInstance == null) return;

        ballInstance.transform.position = Vector2.zero;
        BallMovement ball = ballInstance.GetComponent<BallMovement>();

        if (ball != null)
        {
            Vector2 startDir = new Vector2(leftScored ? 1 : -1, Random.Range(-0.5f, 0.5f));
            ball.Direction = startDir;
        }
    }

    public void ScoreLeft()
    {
        if (!IsServer || GameOver.Value) return;

        LeftScore.Value++;
        CheckWinCondition();
        if (!GameOver.Value) ResetBall(true);
    }

    public void ScoreRight()
    {
        if (!IsServer || GameOver.Value) return;

        RightScore.Value++;
        CheckWinCondition();
        if (!GameOver.Value) ResetBall(false);
    }

    private void CheckWinCondition()
    {
        if (LeftScore.Value >= pointsToWin || RightScore.Value >= pointsToWin)
        {
            GameOver.Value = true;

            if (ballInstance != null)
                ballInstance.GetComponent<BallMovement>()?.StopBall();
        }
    }
}