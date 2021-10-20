using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 0.16f;
    [SerializeField] private float clampDelta = 42f;
    [SerializeField] private float bounds = 5;
    [SerializeField] private GameObject prodecuralPlayer;
    [SerializeField] private GameObject trailEffect;

    private Rigidbody rb;
    private Vector3 lastMousePos;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool finish;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
        if (canMove)
        {
            transform.position += FindObjectOfType<CameraMovement>().camVelocity;
        }

        if (!canMove && !gameOver && !finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canMove = true;
                FindObjectOfType<GameManager>().RemoveUI();
            }
        }

        if(!canMove && gameOver && !finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Level" + FindObjectOfType<GameManager>().level);
                Time.timeScale = 1f;
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        if (canMove && !finish)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 vector = lastMousePos - Input.mousePosition;
                lastMousePos = Input.mousePosition;
                vector = new Vector3(vector.x, 0, vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
                rb.AddForce((-moveForce * sensitivity) - rb.velocity / 5f, ForceMode.VelocityChange);
            }
        }
        rb.velocity.Normalize();
    }

    private void GameOver()
    {
        GameObject shatterSphere = Instantiate(prodecuralPlayer, transform.position, Quaternion.identity);
        foreach(Transform shatter in shatterSphere.transform)
        {
            shatter.GetComponent<Rigidbody>().AddForce(Vector3.forward * 6, ForceMode.Impulse);
        }

        canMove = false;
        gameOver = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(trailEffect);
        Time.timeScale = 0.3f;
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + FindObjectOfType<GameManager>().GetLevel());
    }

    

    IEnumerator NextLevel()
    {
        Debug.Log("true");
        finish = true;
        canMove = false;
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<GameManager>().LevelUp();
        FindObjectOfType<GameManager>().LevelNoUpdate();
        SceneManager.LoadScene("Level2");
        
    }

    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Enemy")
        {
            if (!gameOver)
            {
                GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.gameObject.name == "Finish")
        {
            StartCoroutine(NextLevel());
        }
    }

}
