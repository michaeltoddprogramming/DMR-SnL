using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D player;
    
    public float moveSpeed = 1000f;
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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