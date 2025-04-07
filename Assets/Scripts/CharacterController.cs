using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float laneChangeSpeed; // Þerit deðiþtirme hýzý
    public int desiredLane = 0; // Þerit 0 ortada demektir.
    public int laneDistance = 12; // Yatay Þerit uzunlugu


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
        }
        desiredLane = Mathf.Clamp(desiredLane, -1, 1);

        Vector3 targetPosition = transform.position; // Hedef yol
        targetPosition.x = desiredLane * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
        //transform.position += moveSpeed * Time.deltaTime * -transform.forward;
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
