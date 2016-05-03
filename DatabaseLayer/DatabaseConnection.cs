using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using CommonResources;
using ControlPipeline;

namespace DatabaseLayer
{
    public class DatabaseConnection
    {
        private const int MaxCodeLength = 3;

        public void Input(int input)
        {
            ProgressPipe.Instance.ProgressMessage("Odczytywanie z bazy...");
            Process(input);
        }

        private void Process(int code)
        {
            try
            {
                var connection = new SQLiteConnection(@"DataSource=Data\CodeDatabase.db");
                connection.Open();
                var ecodes = ReadDatabase(code, connection);
                SendData(ecodes);
                connection.Close();
            }
            catch (Exception e)
            {
                ErrorPipe.Instance.ErrorMessage(this, "Nie udało się połączyć z bazą danych.\n" + e.Message);
            }
   
        }

        private List<ECode> ReadDatabase(int code, SQLiteConnection connection)
        {
            var ecodes = new List<ECode>();

            var sqlQuery = @"select * from ECodes where Code like '%" + code.ToString() + "%' order by Id";
            var command = new SQLiteCommand(sqlQuery, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var ecode = new ECode
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Code = Convert.ToString(reader["Code"]),
                    Name = Convert.ToString(reader["Name"]),
                    Description = Convert.ToString(reader["Description"])
                };
                if (ecode.Code.Count(char.IsDigit) <= MaxCodeLength)
                {
                    ecodes.Add(ecode);
                }
            }
            return ecodes;
        }

        private void SendData(List<ECode> ecodes)
        {
            DataPipe.Instance.DataMessage(ecodes);
        }
    }
}
