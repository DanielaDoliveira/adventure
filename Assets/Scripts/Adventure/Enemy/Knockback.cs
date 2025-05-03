using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

   
   public  void GetKnockedBack(Transform damageSource, float knockbackThrust)
    {
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * _rigidbody.mass;
        _rigidbody.AddForce(difference, ForceMode2D.Impulse);
    }
}
