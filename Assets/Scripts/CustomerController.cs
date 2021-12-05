using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public int DrinkType => requestedDrink;

    [SerializeField] private float moveSpeed, moveSpeedSlow;
    [SerializeField] TextMeshPro text;

    private bool playerIsLooking => Vector3.Angle (cam.transform.forward, transform.position - cam.transform.position) < 60;

    private Camera cam;
    private Rigidbody rb;
    private CustomerState state = CustomerState.Approaching;
    Vector3 moveDirection => (state == CustomerState.Leaving ? -1 : 1) * new Vector3 
        (cam.transform.position.x - transform.position.x, 0, cam.transform.position.z - transform.position.z).normalized;
    
    private int requestedDrink;
    private float patience = 5;

    private void Start ()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody> ();
        transform.forward = moveDirection;
        text.text = RandomDrinksRequestGenerator ();
    }

    private void Update ()
    {
        if (state == CustomerState.Waiting)
        {
            patience = Mathf.Max (patience - Time.deltaTime, 0);
            return;
        }

        rb.velocity = moveDirection * (playerIsLooking ? moveSpeed : moveSpeedSlow);
        transform.forward = moveDirection;
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.CompareTag ("Bar") && state == CustomerState.Approaching)
            state = CustomerState.Waiting;
    }

    public void ReceiveDrink (int drinkType)
    {
        if (state == CustomerState.Leaving) return;

        if (drinkType == requestedDrink)
        {
            text.text = "";
            state = CustomerState.Leaving;
            Destroy (gameObject, 20);
        }
    }

    private string RandomDrinksRequestGenerator ()
    {
        requestedDrink = Drink.RandomDrink;
        string start = phraseStarts[Random.Range (0, phraseStarts.Length)];
        string end = phraseEnds[Random.Range (0, phraseEnds.Length)];

        return start + Drink.DrinkTypes[requestedDrink] + "," + end + ".";
    }

    private string[] phraseStarts = new string[]
    {
        "Can I get a ",
        "Gimme a ",
        "I needs me a ",
        "I'll take one ",
        "Give us a bloody ",
        "One ",
        "Oi, ",
        "uh can I uh... ",
        "*tips fedora* ",
        "Ahoy matey ",
        "To whom it may concern, ",
        "HEYO! ",
        "Howdy doody, ",
        "Sup homie, ",
        "Just a ",
        "A glass of your finest ",
        "Got any... ",
        "I'm cruisin for a ",
        "I gotta hankerin for a ",
        "Got milk? ",
        "I'm feelin a "
    };
    private string[] phraseEnds = new string[]
    {
        " please",
        " thanks",
        " cheers",
        " pal",
        " and make it snappy",
        " ;)",
        " jabroni",
        " bozo",
        "...",
        " please and thank you",
        " good sir",
        " *tips fedora*",
        " poindexter",
        " bro",
        " if you please",
        " m'lady",
        " on the rocks",
        " or whatever",
        " por favor",
        " um thanks...",
        " and one for the road",
        " and buy yourself something nice",
        " keep the change"
    };
}

public enum CustomerState
{
    Approaching,
    Waiting,
    Leaving
}