using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public GameObject bulletPrefab;

    public override void OnStartLocalPlayer()
    {
        //GetComponent<MeshRenderer>().material.color = Color.red;
    }

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var bullet = (GameObject)Instantiate(
             bulletPrefab,
             transform.position + transform.forward,
             Quaternion.identity);

        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        bullet.GetComponent<Rigidbody>().transform.LookAt(mousePos);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 1000);

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 2.0f);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        /*var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(-x, 0, -z);*/

        if (Input.GetMouseButtonDown(0))
        {
            // Command function is called from the client, but invoked on the server
            CmdFire();
        }
    }
}