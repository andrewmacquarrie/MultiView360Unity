using UnityEngine;
using System.Collections;


/// <summary>
///		PopMovieSimple is a very simple example of how to use a PopMovie instance to
///		play a movie to a specified texture using games timedelta to control the playback.
/// </summary>
[AddComponentMenu("PopMovie/PopMovieSwitch")]
public class PopMovieSwitch : MonoBehaviour {

    public Texture TargetTexture;
    public string[] Filenames;

	[Header("Current time in the movie in seconds")]
	[Range(0,10)]
	public float			MovieTime = 0;

	[Header("Scalar to make the movie playback faster or slower")]
	[Range(0,3)]
	public float			MovieTimeScalar = 1;

	public PopMovieParams	Parameters;
    public PopMovie[] Movies = new PopMovie[5];

    public int              PlayingMovieIndex = 0;

	public bool				PlayOnAwake = true;
	public bool				Playing = false;

	public UnityEngine.Events.UnityEvent	OnFinished;


	[Tooltip("Which audio stream to use")]
	public int				AudioStream = 0;

	[Tooltip("Enable debug logging when movie starts. Same as the global option, but automatically turns it on")]
	public bool				EnableDebugLog = false;

	void Awake() {
		if (PlayOnAwake ) {
			Play ();
		}
	}

	public void Play()
	{
        for (int i = 0; i < Filenames.Length; i++)
        {
            if (Movies[i] == null)
            {
                Movies[i] = new PopMovie(Filenames[i], Parameters, MovieTime);
            }
        }
        /*
        if (Movies[PlayingMovieIndex] == null)
        {
            Movies[PlayingMovieIndex] = new PopMovie(Filenames[PlayingMovieIndex], Parameters, MovieTime);
			if ( EnableDebugLog )
				PopMovie.EnableDebugLog = true;
		}*/

		Playing = true;
	}

	public void Pause()
	{
		Playing = false;
	}

	public void Stop()
	{
		Playing = false;

        for (int i = 0; i < Filenames.Length; i++)
        {
            if (Movies[i] != null)
            {
                Movies[i].Free();
                Movies[i] = null;
            }
        }
	}

	public void UpdateTextures()
	{
        if (Movies[PlayingMovieIndex] != null && TargetTexture != null)
            Movies[PlayingMovieIndex].UpdateTexture(TargetTexture);
	}

	public void Update () {

		if (Playing) {
			MovieTime += Time.deltaTime * MovieTimeScalar;

            
            if (Movies[PlayingMovieIndex] != null)
                Movies[PlayingMovieIndex].SetTime(MovieTime);
                
            
            UpdateTextures();

			//	detect finish
            if (Movies[PlayingMovieIndex] != null)
			{
                var Duration = Movies[PlayingMovieIndex].GetDuration();
				//	if duration is zero, we don't know it yet (could be streaming, video camera, or may not have loaded meta information yet)
				if ( Duration > 0 && MovieTime >= Duration )
				{
					OnFinished.Invoke();
					Playing = false;
				}
			}
		}

	}

    /*
	//	this gets automatically called if this object has an AudioSource component
	void OnAudioFilterRead(float[] data,int Channels)
	{
        if (Movies[PlayingMovieIndex] == null)
			return;

        uint StartTime = Movies[PlayingMovieIndex].GetTimeMs();

        Movies[PlayingMovieIndex].GetAudioBuffer(data, Channels, StartTime, AudioStream);
	}*/

    public void SwitchToSphere(int sphereIndex)
    {
        //Playing = false;
        PlayingMovieIndex = sphereIndex;
        Movies[PlayingMovieIndex].SetTime(MovieTime);
        //Playing = true;
        
        /*
        if (Movies[PlayingMovieIndex] == null)
        {
            Movies[PlayingMovieIndex] = new PopMovie(Filenames[PlayingMovieIndex], Parameters, MovieTime);
        }*/
    }
}
