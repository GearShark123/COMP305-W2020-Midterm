using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private GameObject star;
    [SerializeField] private Transform spawn;

    // Private variables
    private Animator anim;
    private bool isOpening = false;
    private bool isPlayer = false;
    private float num = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayer == true && Input.GetKeyUp(KeyCode.E))
        {
            isOpening = true;
            anim.SetBool("isOpening", isOpening);
        }
    }

    // Physics
    void FixedUpdate()
    {
        if (isOpening == true)
        {
            Invoke("Spawn", num);
            num += 0.1f;
        }
    }

    void Spawn()
    {
        Destroy(Instantiate(star, spawn.position, spawn.rotation), 3.0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayer = false;
        }
    }
}


