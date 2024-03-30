using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class ChangePostProcess : MonoBehaviour
{
    public PostProcessProfile normal_profile;
    public PostProcessProfile dream_profile;

    private PostProcessVolume post_process_volume;
    private bool is_normal_state = true;

    // Start is called before the first frame update
    void Start()
    {
        this.post_process_volume = GetComponent<PostProcessVolume>();
        //this.post_process_volume.profile = ScriptableObject.CreateInstance<PostProcessProfile>();

        //foreach (var property in normal_profile.GetType().GetProperties())
        //{
        //    if (property.CanWrite)
        //    {
        //        property.SetValue(this.post_process_volume.profile, property.GetValue(normal_profile));
        //    }
        //}
        //this.post_process_volume.profile = normal_profile;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the specified key is pressed down
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (is_normal_state)
            {
                ChangeToDream();
                is_normal_state = false;
            } else
            {
                ChangeToNormal();
                is_normal_state = true;
            }
        }

    }

    IEnumerator LerpPosition(PostProcessProfile from, PostProcessProfile to, float duration)
    {
        float time = 0;
        float start_temp = post_process_volume.profile.GetSetting<ColorGrading>().temperature;
        List<Tuple<FloatParameter, FloatParameter, FloatParameter>> parameters = new List<Tuple<FloatParameter, FloatParameter, FloatParameter>>();

        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().temperature,
                from.GetSetting<ColorGrading>().temperature,
                to.GetSetting<ColorGrading>().temperature
                ));

        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<Grain>().intensity,
                from.GetSetting<Grain>().intensity,
                to.GetSetting<Grain>().intensity
                ));

        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<Vignette>().intensity,
                from.GetSetting<Vignette>().intensity,
                to.GetSetting<Vignette>().intensity
                ));

        while (time < duration)
        {
            foreach ( var param in parameters )
            {
                float value = Mathf.Lerp(param.Item2.value, param.Item3.value, time / duration);
                param.Item1.value = value;
            }
            // post_process_volume.profile.GetSetting<ColorGrading>().temperature.Interp()
            // float new_temp = Mathf.Lerp(start_temp, target_temp, time / duration);

            //Debug.Log(new_temp);
            // post_process_volume.profile.GetSetting<ColorGrading>().temperature.value = new_temp;
            time += Time.deltaTime;
            yield return null;
        }

        foreach ( var param in parameters )
        {
            param.Item1.value = param.Item3.value;
        }
    }

    public void ChangeToDream()
    {
        StartCoroutine(LerpPosition(normal_profile, dream_profile, 0.1f));
        
    }
    public void ChangeToNormal()
    {
        StartCoroutine(LerpPosition(dream_profile, normal_profile, 0.1f));
    }
}
