using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //stores dna and current hub
    public bool[] dna;
    public int hub;

    //stores position in current hub
    public float[] hh_pos;
    public float[] gh_pos;
    public float[] mh_pos;
    public float[] rh_pos;
    public float[] eh_pos;

    //stores level completed bools
    public bool[] hl_completed;
    public bool[] gl_completed;
    public bool[] ml_completed;
    public bool[] rl_completed;
    public bool[] el_completed;

    public SaveData()
    {
        dna = new bool[5];

        dna[0] = HubStorage.humanDNA;
        dna[1] = HubStorage.gorillaDNA;
        dna[2] = HubStorage.mouseDNA;
        dna[3] = HubStorage.rhinoDNA;
        dna[4] = HubStorage.eagleDNA;

        hub = HubStorage.currentHub;

        hh_pos = new float[3] { HubStorage.playerPositionHumanHub.x, HubStorage.playerPositionHumanHub.y, HubStorage.playerPositionHumanHub.z };
        gh_pos = new float[3] { HubStorage.playerPositionGorillaHub.x, HubStorage.playerPositionGorillaHub.y, HubStorage.playerPositionGorillaHub.z };
        mh_pos = new float[3] { HubStorage.playerPositionMouseHub.x, HubStorage.playerPositionMouseHub.y, HubStorage.playerPositionMouseHub.z };
        rh_pos = new float[3] { HubStorage.playerPositionRhinoHub.x, HubStorage.playerPositionRhinoHub.y, HubStorage.playerPositionRhinoHub.z };
        eh_pos = new float[3] { HubStorage.playerPositionEagleHub.x, HubStorage.playerPositionEagleHub.y, HubStorage.playerPositionEagleHub.z };

        hl_completed = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            hl_completed[i] = HubStorage.humanlevelCompleted[i];
        }

        gl_completed = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            gl_completed[i] = HubStorage.gorillaLevelCompleted[i];
        }

        ml_completed = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            ml_completed[i] = HubStorage.mouseLevelCompleted[i];
        }

        rl_completed = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            rl_completed[i] = HubStorage.rhinoLevelCompleted[i];
        }

        el_completed = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            el_completed[i] = HubStorage.eagleLevelCompleted[i];
        }
    }

    public void loadGame()
    {
        HubStorage.humanDNA = dna[0];
        HubStorage.gorillaDNA = dna[1];
        HubStorage.mouseDNA = dna[2];
        HubStorage.rhinoDNA = dna[3];
        HubStorage.eagleDNA = dna[4];

        HubStorage.currentHub = hub;

        HubStorage.playerPositionHumanHub = new Vector3(hh_pos[0], hh_pos[1], hh_pos[2]);
        HubStorage.playerPositionGorillaHub = new Vector3(gh_pos[0], gh_pos[1], gh_pos[2]);
        HubStorage.playerPositionMouseHub = new Vector3(mh_pos[0], mh_pos[1], mh_pos[2]);
        HubStorage.playerPositionRhinoHub = new Vector3(rh_pos[0], rh_pos[1], rh_pos[2]);
        HubStorage.playerPositionEagleHub = new Vector3(eh_pos[0], eh_pos[1], eh_pos[2]);

        for (int i = 0; i < 5; i++)
        {
            HubStorage.humanlevelCompleted[i] = hl_completed[i];
        }

        for (int i = 0; i < 4; i++)
        {
            HubStorage.gorillaLevelCompleted[i] = gl_completed[i];
        }

        for (int i = 0; i < 4; i++)
        {
            HubStorage.mouseLevelCompleted[i] = ml_completed[i];
        }

        for (int i = 0; i < 4; i++)
        {
            HubStorage.rhinoLevelCompleted[i] = rl_completed[i];
        }

        for (int i = 0; i < 4; i++)
        {
            HubStorage.eagleLevelCompleted[i] = el_completed[i];
        }
    }
}
