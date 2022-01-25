using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages a fade throug black with the UI system in Unity.
/// 
/// </summary>
public class FadeOut : MonoBehaviour
{

    /// <summary>
    /// This is the image that processes the fade. Is must take the whole screen to work correctly.
    /// </summary>
    public Image fadeElement;

    /// <summary>
    /// Static element makes the class accessible from the whole game. I recommend to put the script
    /// and particularly the image in parallel with a DontDestroyOnLoad script or object. 
    /// </summary>
    public static FadeOut FADE_OUT_ELEMENT;


    /// <summary>
    /// The awake function assign this script to be a singleton
    /// </summary>
    private void Awake()
    {
        if (!FADE_OUT_ELEMENT)
        {
            FADE_OUT_ELEMENT = this;
        }
    }

    /// <summary>
    /// Makes the screen black. To go from black to transparent, invoke the FadeFromBlack function
    /// </summary>
    /// <param name="fadeTime">Time it take to turn black</param>
    public void FadeToBlack(float fadeTime)
    {

        fadeElement.color = Color.black;
        fadeElement.canvasRenderer.SetAlpha(0.0f);
        fadeElement.CrossFadeAlpha(1.0f, fadeTime, true);
    }

    /// <summary>
    /// Makes the screen transparent. To go from transparent to black, invoke the FadeToBlack function
    /// </summary>
    /// <param name="fadeTime">Time it take to turn black</param>
    public void FadeFromBlack(float fadeTime)
    {
        fadeElement.color = Color.black;
        fadeElement.canvasRenderer.SetAlpha(1.0f);
        fadeElement.CrossFadeAlpha(0.0f, fadeTime, true);
    }

    /// <summary>
    /// On starting the scene, invokes Fading from black
    /// </summary>
    private void OnEnable()
    {
        FadeFromBlack(1f);
    }


}