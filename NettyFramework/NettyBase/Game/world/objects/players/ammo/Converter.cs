using System;
using NettyBase.Game.world.objects.players.settings;

namespace NettyBase.Game.world.objects.players.ammo
{
    static class Converter
    {
        public static string AmmoToDbString(string lootId)
        {
            switch (lootId)
            {
                case "ammunition_laser_lcb-10":
                    return "LCB_10";
                case "ammunition_laser_mcb-25":
                    return "MCB_25";
                case "ammunition_laser_mcb-50":
                    return "MCB_50";
                case "ammunition_laser_ucb-100":
                    return "UCB_100";
                case "ammunition_laser_sab-50":
                    return "SAB_50";
                case "ammunition_laser_cbo-100":
                    return "CBO_100";
                case "ammunition_laser_rsb-75":
                    return "RSB_75";
                case "ammunition_laser_job-100":
                    return "JOB_100";
                case "ammunition_rocket_r-310":
                    return "R_310";
                case "ammunition_rocket_plt-2026":
                    return "PLT_2026";
                case "ammunition_rocket_plt-2021":
                    return "PLT_2021";
                case "ammunition_rocket_plt-3030":
                    return "PLT_3030";
                case "ammunition_specialammo_pld-8":
                    return "PLD_8";
                case "ammunition_specialammo_dcr-250":
                    return "DCR_250";
                case "ammunition_specialammo_wiz-x":
                    return "WIZ_X";
                case "ammunition_rocket_bdr-1211":
                    return "BDR_1211";
                case "ammunition_rocketlauncher_hstrm-01":
                    return "HSTRM_01";
                case "ammunition_rocketlauncher_ubr-100":
                    return "UBR_100";
                case "ammunition_rocketlauncher_eco-10":
                    return "ECO_10";
                case "ammunition_rocketlauncher_sar-01":
                    return "SAR_01";
                case "ammunition_rocketlauncher_sar-02":
                    return "SAR_02";
                case "ammunition_mine_acm-01":
                    return "ACM_01";
                case "ammunition_mine_smb-01":
                    return "SMB_01";
                case "equipment_extra_cpu_ish-01":
                    return "ISH_01";
                case "ammunition_specialammo_emp-01":
                    return "EMP_01";
                default:
                    throw new NotImplementedException();
            }
        }
        
        public static int GetLootAmmoId(string lootId)
        {
            switch (lootId)
            {
                case "ammunition_laser_lcb-10":
                case "ammunition_rocket_r-310":
                    return 1;
                case "ammunition_laser_mcb-25":
                case "ammunition_rocket_plt-2026":
                    return 2;
                case "ammunition_laser_mcb-50":
                case "ammunition_rocket_plt-2021":
                    return 3;
                case "ammunition_laser_ucb-100":
                case "ammunition_rocket_plt-3030":
                    return 4;
                case "ammunition_laser_sab-50":
                case "ammunition_rocket_bdr-1211":
                    return 5;
                case "ammunition_laser_rsb-75":
                    return 6;
                case "ammunition_laser_cbo-100":
                case "ammunition_specialammo_pld-8":
                    return 7;
                case "ammunition_specialammo_dcr-250":
                    return 8;
                case "ammunition_laser_job-100":
                case "ammunition_specialammo_wiz-x":
                    return 9;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
