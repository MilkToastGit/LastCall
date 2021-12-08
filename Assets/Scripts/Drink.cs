using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    [SerializeField] private int type;

    [SerializeField] private GameObject GlassParticles;

    private Rigidbody rb;
    private Vector3 origin;
    private Quaternion originRot;

    private bool returnedToPosition;

    private void Awake ()
    {
        rb = GetComponent<Rigidbody> ();
        rb.isKinematic = true;
        origin = transform.position;
        originRot = transform.rotation;
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (rb.isKinematic) return;

        if (collision.collider.TryGetComponent (out CustomerController customer))
            customer.ReceiveDrink (type);

        //Instantiate (GlassParticles, transform.position, Quaternion.identity);
        rb.isKinematic = true;
        transform.position = origin;
        transform.rotation = originRot;
    }

    public static string[] DrinkTypes = new string[]
    {
        "Shiraz",
        "Cabernet Sauvignon",
        "Pinot Noir",
        "Sauvignon Blanc",
        "Chardonnay",
        "Vodka Lime Soda",
        "Bourbon And Coke",
        "Espresso Martini",
        "Gin And Tonic",
        "Bloody Mary",
        "Shot Of Tequila",
        "Long Island Iced Tea",
        "Pale Ale",
        "Lager",
        "Imperial Stout",
        "Pilsner",
    };
    public static int RandomDrink => Random.Range (0, DrinkTypes.Length);

    public IEnumerator ReturnToPosition()
    {
        yield return new WaitForSeconds(3f);
        if (!returnedToPosition)
        {
            transform.position = origin;
            transform.rotation = originRot;
        }
    }
}