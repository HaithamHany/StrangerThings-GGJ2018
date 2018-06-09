using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public GameObject bulletPrefab;
    public GameObject shootingPointDustin;
    public GameObject shootingPointLucas;
    public GameObject Lucas;
    public GameObject Dustin;
    public GameObject SwapObject;
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject[] DemogorgenPrefab;
    private ParticleSystem Particleeffect;
    public GameObject ParticleESysObj;

    public float TimeLimit;
    [SerializeField]
    private float timer=0;
    private float GLobalTIme;

    private Animator LucasAnim;
    private Animator DustinAnim;
    private bool Swap;
    int rand;
    int dir=-1;
    public ObjectPooling poolingscript;

    void Start()
    {
        //Object pooling

        poolingscript = GetComponent<ObjectPooling>();
        LucasAnim = Lucas.GetComponent<Animator>();
        DustinAnim= Dustin.GetComponent<Animator>();
        Particleeffect = ParticleESysObj.GetComponent<ParticleSystem>();
        InvokeRepeating("ActivateMonster", 4, 4);
    }
	// Update is called once per frame
	void Update () {

     
     
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LucasAnim.SetTrigger("Shoot");         
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DustinAnim.SetTrigger("Hit");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Swap = !Swap;
          
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Particleeffect.Play();
        }

        if (Swap)
        {
            dir = 1;
            SwapObject.GetComponent<Animator>().SetTrigger("Swap");
            SwapObject.GetComponent<Animator>().ResetTrigger("SwapBack");
        }
        else
        {
            dir = -1;
            SwapObject.GetComponent<Animator>().SetTrigger("SwapBack");
            SwapObject.GetComponent<Animator>().ResetTrigger("Swap");
        }

      
         rand = Random.Range(0, 2);
        GLobalTIme += Time.deltaTime;
        if (GLobalTIme>= 4)
        {
            DemogorgenPrefab[rand].GetComponent<Demogorgen>().speed += 0.1f;

            GLobalTIme = 0;
        }

    }

    void SpawnMonstersLeft()
    {

        GameObject Monster_left = Instantiate(DemogorgenPrefab[rand], Gate1.transform.position, DemogorgenPrefab[rand].transform.rotation) as GameObject;
        Monster_left.SetActive(false);
      //  Destroy(Demogo, 6);

    }
    void SpawnMonstersRight()
    {
        GameObject Monster_right = Instantiate(DemogorgenPrefab[rand], Gate2.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
        Monster_right.SetActive(false);
       // Destroy(Demogo2, 6);
    }

    public void ShootBullet()
    {     
       // GameObject shootingPoint = character == charactershooting.dusten ? shootingPointDustin : shootingPointLucas;
        GameObject shot = Instantiate(bulletPrefab, shootingPointLucas.transform.position, Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = new Vector2( dir*20, 2);
        Destroy(shot, 1);
    }

    void ActivateMonster()
    {
        GameObject obj = poolingscript.GetPooledObject();
        if (obj == null) return;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (obj.name == "demogorgon@Running(Clone)")
        {
            obj.transform.position = Gate1.transform.position;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else
        {
            obj.transform.position = Gate2.transform.position;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }
        obj.SetActive(true);
    }
}
