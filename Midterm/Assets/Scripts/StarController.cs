using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Vector3 impulse = new Vector2(0.0f, 16.0f);

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.AddForce(impulse, ForceMode2D.Impulse);
    }
}
