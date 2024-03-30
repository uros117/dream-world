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
    //private bool is_normal_state = true;

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
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    if (is_normal_state)
        //    {
        //        ChangeToDream();
        //        is_normal_state = false;
        //    } else
        //    {
        //        ChangeToNormal();
        //        is_normal_state = true;
        //    }
        //}

    }

    IEnumerator LerpPosition(PostProcessProfile from, PostProcessProfile to, float duration)
    {
        float time = 0;
        float start_temp = post_process_volume.profile.GetSetting<ColorGrading>().temperature;
        List<Tuple<FloatParameter, FloatParameter, FloatParameter>> parameters = new List<Tuple<FloatParameter, FloatParameter, FloatParameter>>();

        List<Tuple<Vector4Parameter, Vector4Parameter, Vector4Parameter>> vec_4_parameters = new List<Tuple<Vector4Parameter, Vector4Parameter, Vector4Parameter>>();

        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().temperature,
                from.GetSetting<ColorGrading>().temperature,
                to.GetSetting<ColorGrading>().temperature
                ));

        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().mixerRedOutBlueIn,
                from.GetSetting<ColorGrading>().mixerRedOutBlueIn,
                to.GetSetting<ColorGrading>().mixerRedOutBlueIn
                ));
        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().mixerRedOutGreenIn,
                from.GetSetting<ColorGrading>().mixerRedOutGreenIn,
                to.GetSetting<ColorGrading>().mixerRedOutGreenIn
                ));
        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().mixerRedOutRedIn,
                from.GetSetting<ColorGrading>().mixerRedOutRedIn,
                to.GetSetting<ColorGrading>().mixerRedOutRedIn
                ));

        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().mixerGreenOutRedIn,
                from.GetSetting<ColorGrading>().mixerGreenOutRedIn,
                to.GetSetting<ColorGrading>().mixerGreenOutRedIn
                ));
        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().mixerGreenOutGreenIn,
                from.GetSetting<ColorGrading>().mixerGreenOutGreenIn,
                to.GetSetting<ColorGrading>().mixerGreenOutGreenIn
                ));
        parameters.Add(
            new Tuple<FloatParameter, FloatParameter, FloatParameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().mixerGreenOutBlueIn,
                from.GetSetting<ColorGrading>().mixerGreenOutBlueIn,
                to.GetSetting<ColorGrading>().mixerGreenOutBlueIn
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


        vec_4_parameters.Add(
            new Tuple<Vector4Parameter, Vector4Parameter, Vector4Parameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().gamma,
                from.GetSetting<ColorGrading>().gamma,
                to.GetSetting<ColorGrading>().gamma
                ));

        vec_4_parameters.Add(
            new Tuple<Vector4Parameter, Vector4Parameter, Vector4Parameter>(
                post_process_volume.profile.GetSetting<ColorGrading>().lift,
                from.GetSetting<ColorGrading>().lift,
                to.GetSetting<ColorGrading>().lift
                ));


        while (time < duration)
        {
            foreach ( var param in parameters )
            {
                float value = Mathf.Lerp(param.Item2.value, param.Item3.value, time / duration);
                param.Item1.value = value;
            }

            foreach ( var param in vec_4_parameters )
            {
                Vector4 value = Vector4.Lerp(param.Item2.value, param.Item3.value, time / duration);
                param.Item1.value = value;
            }

            time += Time.deltaTime;
            yield return null;
        }

        foreach ( var param in parameters )
        {
            param.Item1.value = param.Item3.value;
        }

        foreach ( var param in vec_4_parameters )
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
