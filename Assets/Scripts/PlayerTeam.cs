using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum Team
{
    Blue,
    Red,
    Undefiend,
}

public class PlayerTeam : NetworkBehaviour
{
    Team m_team = Team.Undefiend;
    public Transform startPos;
    public Team GetTeam()
    {
        return m_team;
    }

    public void SetTeam(Team team)
    {
        m_team = team;
    }
}
