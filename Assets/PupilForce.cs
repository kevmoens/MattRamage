using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilForce : MonoBehaviour
{
    public int Level = 3;
    public GameObject Eye;
    public CircleEdgeCollider2D EyeCollider;
    
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("PupilForce Awake");
        try
        {
            float x = 0, y = 0;
            GetForce(ref x, ref y, EyeCollider.Radius);

            if (Level <= 1)
            {
                gameObject.transform.position = new Vector3(Eye.transform.position.x, Eye.transform.position.y, gameObject.transform.position.z);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(Level == 1 ? 7 : 6, 0));
            } else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(x, Level <= 2 ? 0 : y));
                GetComponent<Rigidbody2D>().gravityScale = 1f;
            }
                
            
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //transform.position = new Vector3(Eye.transform.position.x, Eye.transform.position.y, transform.position.z);

        } catch (System.Exception ex)
        {
            Debug.Log("PupilForce Error: " + ex.Message);
        }
    }
    public static void GetForce(ref float x, ref float y, float radius)
    {
        var seed = System.DateTime.Now.Millisecond;
        var seed2 = seed * System.DateTime.Now.Minute;
        x = GetRandomNumber(seed, 6f, radius * 50f);
        y = GetRandomNumber(seed2, 6f, radius * 50f);
        Debug.Log("X:" + x.ToString());
        Debug.Log("Y:" + y.ToString());
    }
    private static float GetRandomNumber(int seed, double minimum, double maximum)
    {
        var random = new System.Random(seed);
        return (float)(random.NextDouble() * (maximum - minimum) + minimum);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
