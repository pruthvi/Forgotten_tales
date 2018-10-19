/*  Copyright (c) Pruthvi  |  http://pruthv.com  */

using UnityEngine;

public class bullet : MonoBehaviour {

    #region Variables
    private GameObject player;
    public float speed;
    Vector3 direction;
    #endregion

    void Start ()
	{
        player = GameObject.FindGameObjectWithTag("MainCamera");
        direction = player.transform.position - this.transform.position;

    }

    void Update ()
	{

        //set direction
        // this.GetComponent<Rigidbody>().velocity = transform.forward + direction.normalized * 5;
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform);
        this.GetComponent<Rigidbody>().AddForce(direction * speed * Time.deltaTime);

    }

}
