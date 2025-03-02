using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D player;
    
    public float moveSpeed = 5f; // Adjusted to a more reasonable default value
    private int currentNumber = 0;
    

    void Start()
    {
        // Initialization code
    }

    void Update()
    {
        // Update logic if needed
    }

    public void MoveToPosition(Vector3 targetPosition, System.Action onComplete)
    {
        StartCoroutine(MoveToPositionCoroutine(targetPosition, onComplete));
    }

    private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition, System.Action onComplete)
{
    while ((targetPosition - transform.position).sqrMagnitude > 0.01f)
    {
        float step = moveSpeed * Time.deltaTime;
        Debug.Log($"Moving... Speed: {moveSpeed}, Step: {step}, DeltaTime: {Time.deltaTime}");
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        yield return null;
    }
    transform.position = targetPosition;
    Debug.Log("Player reached the target position: " + targetPosition);
    onComplete?.Invoke();
}

    public int GetCurrentPosition()
    {
        return currentNumber;
    }

    public void SetCurrentPosition(int position)
    {
        currentNumber = position;
    }
}