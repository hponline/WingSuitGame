using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float laneChangeSpeed; // Þerit deðiþtirme hýzý
    public int desiredLane = 0; // Þerit 0 ortada demektir.
    public int laneDistance = 12; // Yatay Þerit uzunlugu

    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {            
            animator.CrossFade("MoveRight", 0.1f);
            if (desiredLane < 1)
            {
                desiredLane++;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {            
            animator.CrossFade("MoveLeft", 0.1f);
            if (desiredLane > -1)
            {
                desiredLane--;
            }
        }

        desiredLane = Mathf.Clamp(desiredLane, -1, 1);

        Vector3 targetPosition = transform.position; // Hedef yol
        targetPosition.x = desiredLane * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.gameManagerInstance.playerCoin++;
            Destroy(other.gameObject);           
        }
    }

}