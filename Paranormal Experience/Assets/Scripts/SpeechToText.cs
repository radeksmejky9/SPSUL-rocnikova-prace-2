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
    public AudioSource audioSource;
    System.Random r = new System.Random();
    string location;
    string age;
    bool close;
    public List<AudioClip> audioClips;


    private void Start()
    {
        age = r.Next(0, 101) > 50 ? "Old" : "Young";

        keywords = new Dictionary<string, Action>();
        //easteregg

        keywords.Add("Lets Play a Game", () => { LetsPlayGame(); });
        //KILL/HATE/DIE/ATTACK
        keywords.Add("Should we leave", () => { PlayAudioClip("Kill"); });
        keywords.Add("Are you angry", () => { PlayAudioClip("Kill"); });
        keywords.Add("What do you want", () => { PlayAudioClip("Kill"); });
        keywords.Add("Why are you here", () => { PlayAudioClip("Hate"); });
        keywords.Add("Do you want to hurt us", () => { PlayAudioClip("Die"); });
        keywords.Add("Shall we leave", () => { PlayAudioClip("Die"); });
        keywords.Add("Can you talk", () => { PlayAudioClip("Die"); });
        keywords.Add("Is anything wrong", () => { PlayAudioClip("Death"); });
        keywords.Add("Do you want us here", () => { PlayAudioClip("Death"); });
        keywords.Add("What should we do", () => { PlayAudioClip("Death"); });
        keywords.Add("Anybody in the room", () => { PlayAudioClip("Death"); });
        keywords.Add("Anybody with us", () => { PlayAudioClip("Death"); });
        keywords.Add("Speak to us", () => { PlayAudioClip("Death"); });
        keywords.Add("Anybody here", () => { PlayAudioClip("Death"); });
        keywords.Add("Are you with us", () => { PlayAudioClip("Attack"); });
        keywords.Add("Do you want us to leave", () => { PlayAudioClip("Attack"); });
        keywords.Add("Can we help", () => { PlayAudioClip("Attack"); });
        keywords.Add("Are you here", () => { PlayAudioClip("Hate"); });
        keywords.Add("Is anyone here", () => { PlayAudioClip("Hate"); });
        //OLD/YOUNG
        keywords.Add("How old are you", () => { PlayAudioClip(age); });
        keywords.Add("How young are you", () => { PlayAudioClip(age); });
        keywords.Add("What is your age", () => { PlayAudioClip(age); });
        keywords.Add("When were you born", () => { PlayAudioClip(age); });
        keywords.Add("Are you a child", () => { PlayAudioClip(age); });
        keywords.Add("Are you old", () => { PlayAudioClip(age); });
        keywords.Add("Are you young", () => { PlayAudioClip(age); });
        //CLOSE/FAR/BEHINDYOU/IMHERE
        keywords.Add("Where are you", () => { PlayAudioClip("BehindYou"); });
        keywords.Add("Are you close", () => { PlayAudioClip("ImClose"); });
        keywords.Add("Can you show yourself", () => { PlayAudioClip("ImHere"); });
        keywords.Add("Give us a sign", () => { PlayAudioClip("ImClose"); });
        keywords.Add("Let us know you are here", () => { PlayAudioClip("NextToYou"); });
        keywords.Add("Show yourself", () => { PlayAudioClip("ImHere"); });
        keywords.Add("Is there a spirit here", () => { PlayAudioClip("ImHere"); });
        keywords.Add("Is there a Ghost here", () => { PlayAudioClip("BehindYou"); });
        keywords.Add("What is your location", () => { PlayAudioClip("NextToYou"); });
        //POLO
        keywords.Add("Marco", () => { PlayAudioClip("Polo"); });


        kr = new KeywordRecognizer(keywords.Keys.ToArray());
        kr.OnPhraseRecognized += KeywordRecognizerOnPhraseRecongnized;
        kr.Start();
    }


    private void PlayAudioClip(string s)
    {
        kr.Stop();
        foreach (var audioClip in audioClips)
        {
            if (audioClip.name == s)
            {
                audioSource.clip = audioClip;
                break;
            }
        }
        audioSource.Play();
        kr.Start();
    }
    private void LetsPlayGame()
    {
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
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            Debug.Log(builder.ToString());
            keywordAction.Invoke();
        }
    }

}
