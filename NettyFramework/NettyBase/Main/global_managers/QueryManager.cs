using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using NettyBase.Logger.types;
using NettyBase.Main.objects;
using NettyBase.Properties;

namespace NettyBase.Main.global_managers
{
    class QueryManager
    {
        public void Load()
        {
            if (!CheckConnection())
            {
                throw new Exception("Couldn't establish MYSQL connection");
            }
            LoadClans();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>If connection is established</returns>
        public bool CheckConnection()
        {
            int tries = 0;
            TRY:
            try
            {
                SqlDatabaseManager.Initialize();
                SqlDatabaseManager.Log.Write("Successfully connected to database");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("MYSQL Connection failed.");
                SqlDatabaseManager.Log.Write("MySQL Connection failed");
                new ExceptionLog("mysql", "MYSQL Connection failed", e);
                if (tries < 6)
                {
                    Console.WriteLine("Trying to reconnect in .. " + tries + " seconds.");
                    Thread.Sleep(tries * 1000);
                    tries++;
                    goto TRY;
                }
            }
            return false;
        }

        public void LoadClans()
        {
            Global.StorageManager.Clans.Add(0, new Clan(0, "", "",0));
            Global.StorageManager.Clans.Add(1, new Clan(1, "Administrators", "ADM",0));
            Global.StorageManager.Clans.Add(2, new Clan(2, "Developers", "DEV",0));
            Global.StorageManager.Clans.Add(3, new Clan(3, "Bulgarian United Legends^", "BUL*", 1000));
            Global.StorageManager.Clans[3].Diplomacy.Add(2, Diplomacy.ALLIED);
            Global.StorageManager.Clans[2].Diplomacy.Add(3, Diplomacy.ALLIED);
            Global.StorageManager.Clans[3].Diplomacy.Add(1, Diplomacy.AT_WAR);
            Global.StorageManager.Clans[1].Diplomacy.Add(3, Diplomacy.AT_WAR);
        }

        public void SaveAll()
        {

        }

        public Clan GetClan(int id)
        {
            return null;
        }

        public List<Cronjob> LoadCrons()
        {
            List<Cronjob> crons = new List<Cronjob>();
            try
            {
                using (var mySqlClient = SqlDatabaseManager.GetGlobalClient())
                {
                    var queryTable = mySqlClient.ExecuteQueryTable("SELECT * FROM server_crons WHERE ACTIVE=1");
                    foreach (DataRow row in queryTable.Rows)
                    {
                        int id = Convert.ToInt32(row["ID"]);
                        string name = row["NAME"].ToString();
                        bool repeat = Convert.ToBoolean(Convert.ToInt16(row["REPEAT"]));
                        DateTime time = Convert.ToDateTime(row["TIME"]);
                        int interval = Convert.ToInt32(row["INTERVAL"]);
                        string exec = row["EXEC"].ToString();
                        crons.Add(new Cronjob(id){ExecuteStr = exec, ExecutionTime = time, Intervals = interval, Name = name, RepeatedTask = repeat});
                    }
                }
            }
            catch (Exception e)
            {
                new ExceptionLog("db_loadcrons", "Error loading crons", e);
            }
            return crons;
        }

        public void UpdateCronjob(Cronjob cronjob)
        {
            try
            {
                using (var mySqlClient = SqlDatabaseManager.GetGlobalClient())
                {
                    mySqlClient.ExecuteNonQuery($"UPDATE server_crons SET REPEAT={Convert.ToInt16(cronjob.RepeatedTask)}, TIME={cronjob.ExecutionTime}, INTERVAL={cronjob.Intervals}, ACTIVE={Convert.ToInt32(Global.CronjobManager.Cronjobs.Contains(cronjob))} WHERE ID={cronjob.Id}");
                }
            }
            catch (Exception e)
            {
                new ExceptionLog("db_updcrons", "Error updating crons", e);
            }
        }
    }
}
