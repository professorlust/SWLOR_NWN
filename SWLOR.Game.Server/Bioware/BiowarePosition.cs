﻿using SWLOR.Game.Server.Bioware.Contracts;
using SWLOR.Game.Server.GameObject;

using NWN;

namespace SWLOR.Game.Server.Bioware
{
    public class BiowarePosition : IBiowarePosition
    {
        private readonly INWScript _;

        public BiowarePosition(INWScript script)
        {
            _ = script;
        }

        public void TurnToFaceObject(NWObject objectToFace, NWObject facer)
        {
            facer.AssignCommand(() =>
            {
                _.SetFacingPoint(objectToFace.Position);
            });
        }

        public void TurnToFaceLocation(Location locationToFace, NWObject facer)
        {
            facer.AssignCommand(() =>
            {
                _.SetFacingPoint(_.GetPositionFromLocation(locationToFace));
            });
        }

        public float GetChangeInX(float fDistance, float fAngle)
        {
            return fDistance * _.cos(fAngle);
        }

        public float GetChangeInY(float fDistance, float fAngle)
        {
            return fDistance * _.sin(fAngle);
        }

        public Vector GetChangedPosition(Vector vOriginal, float fDistance, float fAngle)
        {
            float changedZ = vOriginal.m_Z;

            var changedX = vOriginal.m_X + GetChangeInX(fDistance, fAngle);
            if (changedX < 0.0)
                changedX = -changedX;
            var changedY = vOriginal.m_Y + GetChangeInY(fDistance, fAngle);
            if (changedY < 0.0)
                changedY = -changedY;

            return _.Vector(changedX, changedY, changedZ);
        }

    }
}
