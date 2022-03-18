using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText : MonoBehaviour
{
    //public Ghost g;
    KeywordRecognizer kr;
    Dictionary<string, Action> keywords;
    public GameObject tv;
    public Camera playerCam;
    public GameObject gamePlane;
    public GameObject backgroundPlane;




    private void Start()
    {
        keywords = new Dictionary<string, Action>();
        keywords.Add("Where are you", () => { Here(); });
        keywords.Add("How old are you", () => { Old(); });
        keywords.Add("Do something", () => { Do(); });
        keywords.Add("Marco", () => { Polo(); });
        keywords.Add("Lets Play a Game", () => { LetsPlayGame(); });

        kr = new KeywordRecognizer(keywords.Keys.ToArray());
        kr.OnPhraseRecognized += KeywordRecognizerOnPhraseRecongnized;
        kr.Start();
    }

    private void Polo()
    {
        Debug.Log("Polo");
    }

    private void Do()
    {
        Debug.Log("BAOOOOOOOOOOOOOOO");
    }
    private void Here()
    {
        Debug.Log("Behind you");
    }
    private void Old()
    {
        Debug.Log("76 years old");
    }
    private void LetsPlayGame()
    {
        Debug.Log("Game");
        RaycastHit hit;
        Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 10);
        if (tv.GetComponent<TVRemote>().is_enabled && !gameObject.GetComponent<PlayerMovement>().eastereggdone && hit.collider.tag == "EasterEgg" && gameObject.GetComponent<PickUpController>().equipPosition.childCount == 0)
        {

            Debug.Log("GameActive");
            backgroundPlane.SetActive(false);
            gamePlane.SetActive(true);
            gameObject.GetComponent<PlayerMovement>().easteregg = true;
            gameObject.GetComponent<PlayerMovement>().eastereggdone = true;
        }

    }

    private void KeywordRecognizerOnPhraseRecongnized(PhraseRecognizedEventArgs args)
    {

        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

}
