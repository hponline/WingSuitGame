using System.Collections;
using UnityEngine;

public enum HitX { Left, Mid, Right }
public class CollisionHandler : MonoBehaviour
{
    PlatformSpawner platformSpawner;
    public Collider playerCollider;
    public GameObject snowBallPrefab;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public HitX GetHitX(Collider col)
    {
        Bounds playerBounds = playerCollider.bounds;
        Bounds hitBounds = col.bounds;

        float minX = Mathf.Max(playerBounds.min.x, hitBounds.min.x);
        float maxX = Mathf.Min(playerBounds.max.x, hitBounds.max.x);

        float avarage = ((minX + maxX) / 2f - hitBounds.min.x) / hitBounds.size.x;        
        if (avarage > 0.66f)
            return HitX.Right;
        else if (avarage < 0.33f)
            return HitX.Left;
        else
            return HitX.Mid;        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Collider col = collision;

            HitX hitSide = GetHitX(col);
            Debug.Log("Çarpýþma yönü: " + hitSide.ToString());

            StartCoroutine(HandleHit(hitSide));
        }
    }

    IEnumerator HandleHit(HitX hitSide)
    {
        yield return null;

        switch (hitSide) 
        {
            case HitX.Left:                
                animator.CrossFade("HitLeft", 0.1f);
                break;
                
            case HitX.Mid:                
                animator.CrossFade("FrontDeath", 0.1f);
                //platformSpawner.speed = 0;               
                break; 
            
            case HitX.Right:               
                animator.CrossFade("HitRight", 0.1f);
                break;
        }
    }

    //public void ShowObstacle()
    //{
    //    Vector3 pos = new (playerCollider.transform.position,)
    //    Instantiate(snowBallPrefab, playerCollider.transform.position + new Vector3 ())
    //}

}
