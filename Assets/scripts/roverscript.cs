using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;


public class roverscript : MonoBehaviour
{
    public GameObject myRightHand;
    public GameObject rover;
    public GameObject canvasKeyboard;
    public Text OutputText;
    public GameObject masterSphere;
    public AudioSource myAudio;
    public GameObject myCamera;
    List<GameObject> allSpheres = new List<GameObject>();

    KeywordRecognizer MyVoiceCommands;
    Dictionary<string, System.Action> commoncommands = new Dictionary<string, System.Action>();

    bool cap = false;
    void rotateRover()
    {
        Debug.Log("Rotate");
        rover.transform.Rotate(new Vector3(0, 1, 0), 90f);

    }
    void MyVoiceCommands_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordaction;
        if (commoncommands.TryGetValue(args.text, out keywordaction))
        {
            keywordaction.Invoke();

        }
    }
    void PlayAudio()
    {
        myAudio.Play();
    }

   // void MyVoiceCommands.TryGetValue(args.text, out keywordaction)){

    // Start is called before the first frame update
    void Start()
    {
        //myAudio.volume = 1 / (myrearCamera.transform.position - masterSphere.transform.position).magnitude;
        for (int i=0; i<100; i++)
        {
            allSpheres.Add(Instantiate(masterSphere));
            allSpheres[i].transform.position = new Vector3(Random.Range(-20, 20), Random.Range(10, 20), Random.Range(2, 6));

        }

        commoncommands.Add("rotate", () =>
        {
            rotateRover();
        });
        commoncommands.Add("Play", () =>
        {
            PlayAudio();
        });
        MyVoiceCommands = new KeywordRecognizer(commoncommands.Keys.ToArray());
        MyVoiceCommands.OnPhraseRecognized += MyVoiceCommands_OnPhraseRecognized;
        MyVoiceCommands.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if((myCamera.transform.position - myRightHand.transform.position).magnitude > 1)
        {
            myAudio.volume = 0;
        }
        else
        {
            myAudio.volume = 1 - (myCamera.transform.position - myRightHand.transform.position).magnitude;

        }

        float movespeedy = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
        movespeedy = movespeedy * 0.1f;
        rover.transform.position += myRightHand.transform.forward * movespeedy;
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Ray myRay = new Ray(myRightHand.transform.position, myRightHand.transform.forward);
            RaycastHit myHits;
            if(Physics.Raycast(myRay, out myHits, 1000f))
            {
                GameObject whatIhit = myHits.transform.gameObject;
                if(whatIhit.name == "Keyboard_Cap")
                {

                    cap = !cap;
                    Component[] myKeys;
                    myKeys = canvasKeyboard.GetComponentsInChildren<Button>();
                    Debug.Log("Number of Keys" + myKeys.Length.ToString());
                    //whatIhit.GetComponentInChildren<Text>().text = myKeys.Length.ToString();
                    for(int i=0; i<myKeys.Length; i++)
                    {
                        if (myKeys[i].name.Split('_')[1].Length == 1)
                        {
                            string myText = myKeys[i].GetComponentInChildren<Text>().text;
                            if (cap == true)
                            {
                                string upperCase = myText.ToUpper();
                                myKeys[i].GetComponentInChildren<Text>().text = upperCase;
                            }
                            else
                            {
                                string lowerCase = myText.ToLower();
                                myKeys[i].GetComponentInChildren<Text>().text = lowerCase;
                            }
                                                     
                        }

                    }

                    
                    

                }
                if(whatIhit.name.Split('_')[1].Length == 1)
                {
                    if(cap == true)
                    { 
                        OutputText.text += whatIhit.name.Split('_')[1].ToUpper();
                    }
                    
                    else
                    {
                        OutputText.text += whatIhit.name.Split('_')[1];
                    }
                    

                }
                if (whatIhit.name == "Keyboard_Delete")
                {

                    OutputText.text = OutputText.text.Remove(OutputText.text.Length -1, 1);              

                }
                if (whatIhit.name == "Keyboard_Space")
                {

                    OutputText.text += " ";

                }
                if (whatIhit.name.Split('_')[1].Length == 2)
                {
                    
                        OutputText.text += whatIhit.name.Split('_')[1];              

                }
            }
        }
    }
}


