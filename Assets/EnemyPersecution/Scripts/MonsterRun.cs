using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MonsterRun : MonoBehaviour
{

    [SerializeField] SplineContainer spline;
    [SerializeField] float speed;
    [SerializeField] bool running; 

    [SerializeField] Spline splines;
    [SerializeField] GameObject monsterPersecution;

    private SplineAnimate splineAnimate;

    private void Awake()
    {
        splineAnimate = monsterPersecution.GetComponent<SplineAnimate>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCorsue()
    {
        splineAnimate.Play();
        Sound_Manager._SOUND_MANAGER.PlayMusicSound(Sound_Manager.TypeOfSound.music, Sound_Manager.Music.level3Chase);
    }

    public void StopCouerse()
    {
        splineAnimate.Pause();
    }

    public void ResetCourse()
    {
        splineAnimate.Restart(false);
    }
}
