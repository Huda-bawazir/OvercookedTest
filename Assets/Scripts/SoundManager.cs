using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Function to play simple sound
    private void PlaySound(AudioClip audioClip, Vector3 positon, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, positon, volume);
    }
}
