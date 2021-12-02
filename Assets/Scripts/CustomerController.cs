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
    private int requestedDrink;
    private bool leaving = false;
    Vector3 moveDirection;

    private void Start ()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody> ();
        moveDirection = cam.transform.position - transform.position;
        moveDirection.y = 0;
        moveDirection.Normalize ();
        transform.forward = moveDirection;
        text.text = RandomDrinksRequestGenerator ();
    }

    private void Update ()
    {
        rb.velocity = moveDirection * (playerIsLooking ? moveSpeed : moveSpeedSlow);
        //transform.position += moveDirection * (playerIsLooking ? moveSpeed : moveSpeedSlow) * Time.deltaTime;
    }

    public void ReceiveDrink (int drinkType)
    {
        if (leaving) return;
        if (drinkType == requestedDrink)
        {
            moveDirection *= -1;
            transform.forward = moveDirection;
            text.text = "";
            leaving = true;
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
