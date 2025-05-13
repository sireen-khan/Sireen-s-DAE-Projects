using UnityEngine;
using UnityEngine.UI; // For UI Text

public class BowlingGameManager : MonoBehaviour
{
    // References to the ball and pin prefabs, spawn point, and pin reset position
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public GameObject pinsParent;
    public GameObject pinPrefab;
    public Transform pinResetPosition;

    // UI Elements
    public Text messageText;
    public GameHUD gameHUD;

    private GameObject currentBall;
    private bool isDragging = false;
    private bool isRolling = false;
    private Vector3 offset;
    private float rollTime = 5f;
    private bool gameIsOver = false;
    private int rollsThisTurn = 0;
    private int totalScore = 0;
    private int currentFrame = 1;

    // ✅ Awake(): Setup before Start
    void Awake()
    {
        if (gameHUD == null)
        {
            gameHUD = FindObjectOfType<GameHUD>();
            Debug.Log("GameHUD was not assigned. Found and assigned at runtime.");
        }

        if (messageText == null)
        {
            Debug.LogWarning("MessageText is not assigned in the Inspector.");
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint not assigned!");
        }

        Debug.Log("Awake: All critical components validated.");
    }

    // ✅ Start(): Initial setup
    void Start()
    {
        messageText.gameObject.SetActive(false);
        StartNewTurn();
    }

    // ✅ Update(): Handle input
    void Update()
    {
        if (gameIsOver) return;

        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == currentBall)
                {
                    isDragging = true;
                    offset = currentBall.transform.position - hit.point;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                currentBall.transform.position = hit.point + offset;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            StartBallRoll();
        }
    }

    // ✅ FixedUpdate(): Optional physics handling
    void FixedUpdate()
    {
        // Optional: Add rolling friction, or check motion
        if (isRolling && currentBall != null)
        {
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null && rb.linearVelocity.magnitude < 0.1f)
            {
                StopRolling();
            }
        }
    }

    void StartNewTurn()
    {
        rollsThisTurn = 0;
        gameIsOver = false;
        SpawnBall();
        ResetPins();
    }

    void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball Prefab is not assigned!");
            return;
        }

        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        currentBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.linearVelocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }

        Debug.Log("Spawning new ball...");
    }

    void ResetPins()
    {
        foreach (Transform pin in pinsParent.transform)
        {
            Destroy(pin.gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            Vector3 pinPosition = pinResetPosition.position + new Vector3(i * 0.5f, 0, 0);
            GameObject pin = Instantiate(pinPrefab, pinPosition, Quaternion.identity);
            pin.transform.SetParent(pinsParent.transform);
        }
    }

    public void RollBall()
    {
        rollsThisTurn++;
        isRolling = true;

        Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();

        if (ballRigidbody != null)
        {
            ballRigidbody.AddForce(spawnPoint.forward * 10f, ForceMode.VelocityChange);
        }

        Invoke("StopRolling", rollTime);
    }

    void StopRolling()
    {
        if (currentBall != null)
        {
            Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.linearVelocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
            }
        }

        ShowMessage("Well Done!");
        gameIsOver = true;
        Invoke("StartNewTurn", 2f);
    }

    void ShowMessage(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
    }

    private void StartBallRoll()
    {
        if (currentBall != null)
        {
            Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 direction = currentBall.transform.position - spawnPoint.position;
                ballRigidbody.AddForce(direction.normalized * 10f, ForceMode.VelocityChange);
                isRolling = true;
            }
        }
    }

    public void Bowl(int pins)
    {
        totalScore += pins;
        gameHUD.UpdateScore(totalScore);
        HandleFrameUpdate();
    }

    private void HandleFrameUpdate()
    {
        if (currentFrame <= 10)
        {
            if (rollsThisTurn >= 2)
            {
                currentFrame++;
                gameHUD.UpdateFrame(currentFrame);
                rollsThisTurn = 0;
            }
        }
    }
}
