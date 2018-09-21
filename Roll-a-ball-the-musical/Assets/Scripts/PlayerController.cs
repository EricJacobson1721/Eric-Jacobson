using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public GUIText countText;
	public GUIText winText;
    public GameObject beat;
    public GameObject percussion;
    public GameObject guitar;
    public GameObject melody;
    public AudioMixer masterMixer;
    public AudioMixer percussionMixer;

	private int count;
    AudioSource audioSource;
    private bool isGameOver;
    

    void Start()
	{
		count = 0;
		SetCountText();
		winText.text = "";
        isGameOver = false;




    }
	
	void FixedUpdate ()
	{
        if (isGameOver == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "PickUp")
		{
            other.gameObject.GetComponent <Collider>().enabled = false;
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<AudioSource>().Play();
 

			count = count + 1;
			SetCountText();


            if (count == 1)
            {
                audioSource = beat.GetComponent<AudioSource>();
                audioSource.mute = !audioSource.mute;
            }
            else if (count == 2)
            {
                audioSource = percusion.GetComponent<AudioSource>();
                audioSource.mute = !audioSource.mute;
            }
            else if (count == 3)
            {
                audioSource = guitar.GetComponent<AudioSource>();
                audioSource.mute = !audioSource.mute;
            }
            else if (count == 4)
            {
                audioSource = melody.GetComponent<AudioSource>();
                audioSource.mute = !audioSource.mute;
            }
            else if (count == 5)
            {
                audioSource = melody.GetComponent<AudioSource>();
                audioSource.volume = 0.5f;
            }
            else if (count == 6)
            {
                masterMixer.SetFloat("mVol", 0f);
                percussionMixer.SetFloat("pVol", -10f);
                audioSource = melody.GetComponent<AudioSource>();
                audioSource.spatialBlend = 0.70f;


            }
            else if (count == 7)
            {
                masterMixer.SetFloat("mVol", 5f);
                percussionMixer.SetFloat("pVol", -0f);
                audioSource = melody.GetComponent<AudioSource>();
                audioSource.spatialBlend = 0.40f;
                masterMixer.SetFloat("mPitch", 1.1f);


            }
            else if (count == 8)
            {
                masterMixer.SetFloat("mPitch", 1.2f);

            }
            else if (count == 9)
            {

                masterMixer.SetFloat("mPitch", 1.40f);

            }
            else
            {
                audioSource = beat.GetComponent<AudioSource>();
                audioSource.loop = !audioSource.loop;
                audioSource = melody.GetComponent<AudioSource>();
                audioSource.loop = !audioSource.loop;
                audioSource = percussion.GetComponent<AudioSource>();
                audioSource.loop = !audioSource.loop;
                audioSource = guitar.GetComponent<AudioSource>();
                audioSource.loop = !audioSource.loop;

                isGameOver = true;
            }

        }
	}
	
	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();
		if(count >= 10)
		{
			winText.text = "YOU WIN!";
            
        }
	}
}
