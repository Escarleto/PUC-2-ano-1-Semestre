using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsMenu : MonoBehaviour
{
    public Volume PostProcessing;

    public void Windowed(bool isWindowed) 
    {
        Screen.fullScreenMode = isWindowed ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
    }

    public void Vignette(bool VignetteOn)
    {
        if (PostProcessing.profile.TryGet<Vignette>(out var vignette)) vignette.active = VignetteOn;
    }

    public void FilmGrain(bool FilmGrainOn)
    {
        if (PostProcessing.profile.TryGet<FilmGrain>(out var filmGrain)) filmGrain.active = FilmGrainOn;
    }

    public void Bloom(bool BloomOn)
    {
        if (PostProcessing.profile.TryGet<Bloom>(out var bloom)) bloom.active = BloomOn;
    }
}
