﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace ChartTestFramwork
{
    internal class ModelSQL : IModelLocalData
    {

       private static String myConnectionString = "server=127.0.0.1;uid=EKG;pwd=1234?;database=ekgdatenbank;";

       private static MySqlConnection conn = new MySqlConnection(myConnectionString);

       private static MySqlCommand mycommand = conn.CreateCommand();
        IViewEKG IModelLocalData.ViewEKG { set => viewEKG = value; }
        IModelECGDevice IModelLocalData.ModelEKGDevice { set => modelECGDevice = value; }
        IControllerEKG IModelLocalData.ControllerEKG { set => controllerEKG = value; }
        private IViewEKG viewEKG;
        private IModelECGDevice modelECGDevice;
        private IControllerEKG controllerEKG;
        List<double> IModelLocalData.getData24h()
        {
            throw new NotImplementedException();
        }

        void IModelLocalData.saveLiveData(ECGValue aktuellerWert)
        {
            mycommand.CommandText = "Insert into ekgtabelle  values(NULL" +
                ",'"+ aktuellerWert.Value + "'" +
                ",'"+aktuellerWert.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:f:ff") + " ')";

            try
            {
                conn.Open();
                mycommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(mycommand.CommandText);
                MessageBox.Show(ex.Message);
            }
            finally 
            { 
                conn.Close();
            }
        }


    }
}