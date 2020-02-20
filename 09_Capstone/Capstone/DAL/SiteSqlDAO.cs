﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";

        public IList<Site> GetTopFiveSites()
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 5 * FROM site", conn);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Site site = new Site()
                        {
                            SiteId = Convert.ToInt32(rdr["site_id"]),
                            CampgroundId = Convert.ToInt32(rdr["campground_id"]),
                            SiteNumber = Convert.ToInt32(rdr["site_number"]),
                            MaxOccupancy = Convert.ToInt32(rdr["max_occupancy"]),
                            Accessible = Convert.ToBoolean(rdr["accessible"]),
                            MaxRVLength = Convert.ToInt32(rdr["max_rv_length"]),
                            Utilities = Convert.ToBoolean(rdr["utilities"])
                        };
                        sites.Add(site);
                    };
                }
                return sites;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}
