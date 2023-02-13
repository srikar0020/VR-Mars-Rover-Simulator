using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class fly : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myRightHand;
    public GameObject myCamera;

    public GameObject masterAsteroid1;
    public GameObject masterAsteroid2;
    public GameObject masterAsteroid3;
    public VideoPlayer myvideoPlayer;
    public GameObject myTV;
    MeshRenderer tvMeshrenderer;
    public GameObject CanvasIntro;
    public GameObject CanvasPicture;
    public RawImage myRawimage;



    int numberofasteroids = 500;
    List<GameObject> allMyAsteroids = new List<GameObject>();
    List<Vector3> myAxis = new List<Vector3>();



    void Start()
    {
        tvMeshrenderer = myTV.GetComponent<MeshRenderer>();
        for (int i = 0; i < numberofasteroids; i++)
        {
            if (i % 3 == 0)
            {
                allMyAsteroids.Add(Instantiate(masterAsteroid1));
            }
            if (i % 3 == 1)
            {
                allMyAsteroids.Add(Instantiate(masterAsteroid2));
            }
            if (i % 3 == 2)
            {
                allMyAsteroids.Add(Instantiate(masterAsteroid3));
            }

            allMyAsteroids[i].transform.position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20));
            allMyAsteroids[i].transform.eulerAngles = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            //allMyAsteroids[i].transform.Rotate(Random.Range(1, 1000), Random.Range(1, 1000), Random.Range(1, 1000));
            //allMyAsteroids[i].transform.rotation = Quaternion.Slerp(allMyAsteroids[i].transform.rotation, , Random.Range(0, 180) * Time.deltaTime);
            //allMyAsteroids[i].transform.Rotate(Vector3.left,100);
            //allMyAsteroids[i].transform.Rotate(10 * Time.deltaTime, 5 * Time.deltaTime, 2 * Time.deltaTime, Space.World);
        }
    }

        // Update is called once per frame
        void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Debug.Log("Button one was hit");
            }

            float flyspeedy = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
            float flyspeedx = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

            myCamera.transform.position += myRightHand.transform.forward * flyspeedy;
            myCamera.transform.position += myRightHand.transform.right * flyspeedx;

            if(OVRInput.GetDown(OVRInput.RawButton.A))
            {
                myvideoPlayer.url = "https://alkekone.library.txstate.edu/textures/videos/3Dprinter.mp4";
                myvideoPlayer.Play();
            /*
            for (int i = 0; i < numberofasteroids; i++)
            {
                

                allMyAsteroids[i].transform.localScale = new Vector3(ScaleMode.);
            }
            */
            }
            if(myvideoPlayer.isPlaying)
            {
                tvMeshrenderer.material.mainTexture = myvideoPlayer.texture;
            myRawimage.texture = myvideoPlayer.texture;

            }
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
               
                myvideoPlayer.Pause();

            }
          if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
          {
            Ray myRay = new Ray(myRightHand.transform.position, myRightHand.transform.forward);
            RaycastHit myHits;

            if(Physics.Raycast(myRay,out myHits, 1000f))
            {
                GameObject whatIhit = myHits.transform.gameObject;
                if (whatIhit.name == "ButtonNext")
                {
                    CanvasIntro.SetActive(false);
                    CanvasPicture.SetActive(true);
                }
                if (whatIhit.name == "ButtonOk")
                {
                    CanvasPicture.SetActive(false);
                }


                Debug.Log(myHits.transform.gameObject.name);
                Destroy(myHits.transform.gameObject);
            }
          }
          for (int i = 0; i < numberofasteroids; i++)
          {
            allMyAsteroids[i].transform.Rotate(Random.Range(0.1f, 2f), Random.Range(0.1f, 2f), Random.Range(0.1f, 2f));
          }
    }


    
}