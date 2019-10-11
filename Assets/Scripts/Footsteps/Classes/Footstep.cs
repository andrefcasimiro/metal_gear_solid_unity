using UnityEngine;

public class Footstep {

  private FootstepManager m_footstepManager;
  private AudioSource m_audioSource;

  private int soundIndex = 0;

  public Footstep (FootstepManager footstepManager, AudioSource audioSource)
  {
    this.m_footstepManager = footstepManager;
    this.m_audioSource = audioSource;
  }

  public void Dispatch (int layer)
  {
    FootstepSound footstepSound = m_footstepManager.GetByLayer(layer);

    if (footstepSound != null)
    {
      if (soundIndex >= footstepSound.clips.Length)
      {
        soundIndex = 0;
      }

      AudioClip footstepClip = footstepSound.clips[soundIndex];
      m_audioSource.clip = footstepClip;
      m_audioSource.Play();

      soundIndex++;
    }
  }

}
