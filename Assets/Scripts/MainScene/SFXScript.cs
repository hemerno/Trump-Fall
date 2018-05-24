using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour {

    bool SFXstate = false;
    AudioSource[] AudioSlots;
    public AudioClip[] OrangeAudio, RedAudio, GreenAuido, WhiteAudio, PepeAudio, IceKingAudio, IceAudio;
    int ComponentsCount;
    int nextFreeSlot;

    // Use this for initialization
    void Start () {

        //for (int i = 0; i < ComponentsCount-1; i++)
        AudioSlots = GetComponents<AudioSource>();
        ComponentsCount = AudioSlots.Length;
        nextFreeSlot = 0;
        if (PlayerPrefs.GetInt("SFX") == 1)
            SFXstate = true;
	}
	
	public void TrumpDeathrattle (int trumpState) {

        if (SFXstate)
        {
            switch(trumpState)
            {
                case 0:
                    {
                        PlayingNewTrack(IceAudio);
                        break;
                    }
                case 1:
                    {
                        PlayingNewTrack(OrangeAudio);
                        break;
                    }
                case 2:
                    {
                        PlayingNewTrack(RedAudio);
                        break;
                    }
                case 3:
                    {
                        PlayingNewTrack(GreenAuido);
                        break;
                    }
                case 4:
                    {
                        PlayingNewTrack(WhiteAudio);
                        break;
                    }
                case 5:
                    {
                        PlayingNewTrack(PepeAudio);
                        break;
                    }
                case 6:
                    {
                        PlayingNewTrack(IceKingAudio);
                        break;
                    }
                default:
                    {
                        print("Playing nonexistent muisc");
                        break;
                    }
            }

           // TrumpVoice(trumpState);
        }


		
	}

    void PlayingNewTrack(AudioClip[] requiredClips)
    {
        int curFreeSlot = nextFreeSlot;
        nextFreeSlot += 1;
        if (nextFreeSlot > ComponentsCount-1)
        {
            nextFreeSlot = 0;
        }
        AudioSlots[curFreeSlot].clip = requiredClips[Random.Range(0, requiredClips.Length)];
        AudioSlots[curFreeSlot].Play();
    }

    void TrumpVoice(int trmpVoiceType)
    {
        if (Random.Range(1, 101) <= 10)
        {
            switch(trmpVoiceType)
            {
                default:
                    {
                        break;
                    }
                case 1:
                    {
                        break;
                    }
            }
        }
    }
}
