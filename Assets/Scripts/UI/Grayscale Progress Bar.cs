using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GrayscaleProgressBar : MonoBehaviour
{
    RectMask2D _mask;

    int _maxSteps;
    int _currSteps;
    float _startProgressOffset;
    void Awake()
    {
        this._mask = GetComponent<RectMask2D>();
    }
    

    public void Init(int maxsteps, float startOffsetScale = 0.75f)
    {
        _currSteps = 0;
        _maxSteps = maxsteps;
        _startProgressOffset = startOffsetScale;
    }
    public void IncrementProgress()
    {
        /*float val = Screen.width * _startProgressOffset;

        _currSteps++;

        val -= val * _currSteps / _maxSteps;

        this._mask.padding.Set(0, 0, val, 0);*/

        AlternativeProgressMarker();
    }

    void AlternativeProgressMarker()
    {
        _currSteps++;
        float currProgress = (float)_currSteps / _maxSteps;

        

        if (!this.IsDestroyed())
        {
            int chidlrenToRecolor = (int)(currProgress * this.transform.childCount);

            for (int i = 0; i < chidlrenToRecolor; i++)
            {
                if (this.transform.GetChild(i).TryGetComponent<Image>(out Image image))
                {
                    image.color = Color.white;
                }
            }
        }
    }
}
