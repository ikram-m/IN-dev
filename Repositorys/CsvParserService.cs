using CsvHelper;
using E5irProjet.Mapper;
using E5irProjet.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace E5irProjet.Repositorys
{
    public class CsvParserService
    {
        
        


        public void ParserWS(DataParameters parms)
        {

            try
            {
                FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create("ftp://"+ parms.HostName +parms.path_CSV_file);
                request.Credentials = new NetworkCredential(parms.UserName, parms.Password);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                using (Stream stream = request.GetResponse().GetResponseStream())


                using (var reader = new StreamReader(stream, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<RadioMapp>();
                    var records = csv.GetRecords<Radio>();

                    remove_row_by_header(getDataByFileName(records), parms);

                }
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }
        }


        private List<Radio> getDataByFileName(IEnumerable<Radio> records)
        {
            List<Radio> radios = new List<Radio>();
            foreach (Radio rad in records)
            {
                if (rad.Name_file == "RADIO_LINK_POWER")

                    radios.Add(rad);
            }

            return radios;

        }

        private void remove_row_by_header(IEnumerable<Radio> records, DataParameters parms)
        {


            string name_file = "";
            string target = "";
            List<string> lines = new List<string>();
            FTP_conn ftp = new FTP_conn();
            foreach (Radio rad in records)
            {
                if (rad.Name_file == "RADIO_LINK_POWER")
                {
                    if (rad.Status == "DISABLED")
                    {
                        target = rad.Header;//the name of the column to skip
                        using (StreamReader reader = new StreamReader(ftp.DownloadFileFTP(parms)))
                        {
                            // string target = "";//the name of the column to skip

                            int? targetPosition = null; //this will be the position of the column to remove if it is available in the csv file
                            string line;

                            List<string> collected = new List<string>();
                            while ((line = reader.ReadLine()) != null)
                            {

                                string[] split = line.Split(',');
                                collected.Clear();

                                //to get the position of the column to skip
                                for (int i = 0; i < split.Length; i++)
                                {
                                    if (string.Equals(split[i], target, StringComparison.OrdinalIgnoreCase))
                                    {
                                        targetPosition = i;
                                        break; //we've got what we need. exit loop
                                    }
                                }

                                //iterate and skip the column position if exist



                                for (int i = 0; i < split.Length; i++)
                                {
                                    if (targetPosition != null && i == targetPosition.Value) continue;

                                    collected.Add(split[i]);

                                }

                                lines.Add(string.Join(",", collected));



                            }
                        }

                    }
                }
                name_file = rad.Name_file;

            }



            //
            string filename = String.Format(name_file + DateTime.UtcNow.ToString("yyyyMMdd_HHmmss") + ".csv");

            string loc_file = @"D:\testCSV\" + filename;

            using (StreamWriter writer = new StreamWriter(@"D:\testCSV\" + filename, false))
            {
                foreach (string line in lines)
                    writer.WriteLine(line);

            }

            ftp.AddFile(loc_file,parms);

        }
    }

}
