using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E5irProjet.Models
{
    public class DataParameters
    {

        public string Protocol { get; set; }
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string path_CSV_file { get; set; }
        public string path_txt_file { get; set; }

        public DataParameters() {
            
                Protocol = "Protocol.Ftp";
                HostName = "ftp.dlptest.com";
                UserName = "dlpuser";
                Password = "rNrKYTX9g7z3RgJRmxWuGHbeu";
                path_CSV_file = "/ik/ISD_TRANS_MW_ERC_PM_V17.csv";
                path_txt_file = "/ik/SOEM1_TN_RADIO_LINK_POWER_20200312_001500.txt";
            }
      public  DataParameters (DataParameters parms)
        {
            Protocol = parms.Protocol;
            HostName = parms.HostName;
            UserName = parms.UserName;
            Password = parms.Password;
            path_CSV_file = parms.path_CSV_file;
            path_txt_file = parms.path_txt_file;
        }

    }
}
