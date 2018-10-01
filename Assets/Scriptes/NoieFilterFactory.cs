using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoieFilterFactory 
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings noiseSettings)
    {
        switch(noiseSettings.filterType)
        {
            case NoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(noiseSettings.simpleNoiseSettings);
            case NoiseSettings.FilterType.Ridged:
                return new RidgedNoiseFilter(noiseSettings.ridgedNoiseSettings);
        }
        return null; //if we faced error in selection
    }
}
