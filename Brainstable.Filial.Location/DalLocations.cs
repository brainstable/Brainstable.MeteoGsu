using System.Collections.Generic;
using System.Data.SQLite;

namespace Brainstable.Filial.Location
{
    public class DalLocations
    {
        private readonly string stringConnection = "Data Source='data\\filial.db';Password='19011982'";
        private SQLiteConnection connection;

        public DalLocations()
        {
            connection = new SQLiteConnection(stringConnection);
        }
        public DalLocations(string stringConnection)
        {
            this.stringConnection = stringConnection;
            connection = new SQLiteConnection(stringConnection);
        }



        public List<Plot> GetPlots()
        {
            List<Plot> plots = null;
            string sql = @"SELECT 
	                        P.Id, 
	                        P.GoodName, 
	                        P.DistrictId,
	                        E.Name,
	                        E.GenetiveName,
	                        E.DativeName,
	                        E.AblativeName
                        FROM Plots AS P
                        JOIN ExtPlots AS E
	                        ON P.Id = E.PlotId
                        WHERE P.IsExists = 1";
            using (SQLiteConnection connection = new SQLiteConnection(stringConnection))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        plots = new List<Plot>();
                        while (dr.Read())
                        {

                            plots.Add(new Plot
                            {
                                Id = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                DistrictId = dr.GetInt32(2),
                                UserName = dr.GetString(3),
                                GenetiveName = dr.IsDBNull(4) ? "" : dr.GetString(4),
                                DativeName = dr.IsDBNull(5) ? "" : dr.GetString(5),
                                AblativeName = dr.IsDBNull(6) ? "" : dr.GetString(6)
                            });
                        }
                    }
                }
            }
            return plots;
        }

        
    }
}