﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backward_logic_Engine;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Backward_logic_Engine
{
    public class MySql_ConnectionTwo
    {
        private MySqlConnection connection;

        public MySql_ConnectionTwo()
        {
            try
            {
                initialize();
            }
            catch(MySqlException)
            {
                //handle
            }
        }

        private void initialize()
        {
            string server;
            string database;
            string uid;
            string password;
            server = "localhost";
            database = "milestone2";
            uid = "root";
            password = "qwe33162yuiF+++!!!123mysql";
            string connectionString = "SERVER = " + server + ";" + "DATABASE = " + database + ";" + "UID = " + uid + ";" + "PASSWORD = " + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                if(ex.Number == 0)
                {
                    return false;//cannot connect to server
                }
                else if(ex.Number == 1045)
                {
                    return false;//base usr name | password
                }

                //handle else
            }

            return false;
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch(MySqlException)
            {
                //handle
            }

            return false;
        }

        public ObservableCollection<string> SQLSELECTExec(string querySTR, string column_name = null)
        {
            ObservableCollection<string> qResult = new ObservableCollection<string>();
            //int i = 0;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(querySTR, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (column_name != null)
                    {
                        qResult.Add(dataReader.GetString(column_name));
                    }
                    else
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            qResult.Add(dataReader.GetString(dataReader.GetName(i)));
                        }
                        //qResult.Add(dataReader.GetFieldValue(i));
                        //i++;
                    }
                }

                dataReader.Close();
                this.CloseConnection();
            }

            return qResult;
        }
    }
}
