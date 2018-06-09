using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demogorgen : MonoBehaviour {

    public Transform target;
    public float speed;
    public Image damageImage;
    private bool damaged=false;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);
    public float flashSpeed = 5f;
    private GameObject imageObj;
    void Start()
    {
        imageObj = GameObject.FindGameObjectWithTag("BloodCam");
        damageImage = imageObj.GetComponent<Image>();
    }
    void Update()
    {
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        float step = speed * Time.deltaTime;    
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        damaged = false;
    }
    void DeactivateMonster()
    {
        gameObject.SetActive(false);
      
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("bullet"))
        {
            Invoke("DeactivateMonster", 0.5f);
            Destroy(coll.gameObject, 0.3f);
        }

        if (coll.gameObject.CompareTag("Bat"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(20, 10), ForceMode.Impulse);
            Invoke("DeactivateMonster", 2f);

        }
        if (coll.gameObject.CompareTag("Character"))
        {
            gameObject.SetActive(false);
            damaged = true;

        }
    }   
}
