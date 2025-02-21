using System.Collections;
using UnityEngine;
using Assets.Master.Scripts.Turret;

namespace Assets.Master.Scripts.Core
{
    public class ShopManager : MonoBehaviour
    {
        public TurretBlueprint standardTurret;
        public TurretBlueprint missileLauncher;
        public TurretBlueprint laserBeamer;

        BuildManager buildManager;

        void Start()
        {
            buildManager = BuildManager.instance;
        }

        public void SelectStandardTurret()
        {
            //Debug.Log("Standard Turret Selected");
            buildManager.SelectTurretToBuild(standardTurret);
        }

        public void SelectMissileLauncher()
        {
            //Debug.Log("Missile Launcher Selected");
            buildManager.SelectTurretToBuild(missileLauncher);
        }

        public void SelectLaserBeamer()
        {
            //Debug.Log("Laser Beamer Selected");
            buildManager.SelectTurretToBuild(laserBeamer);
        }
    }
}