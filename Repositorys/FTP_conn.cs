using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace E5irProjet.Repositorys
{
    public class FTP_conn
    {

        public string AddFile(string loc_name)
        {

          
               try
                  {
                      // Setup session options
                      SessionOptions sessionOptions = new SessionOptions
                      {
                          Protocol = Protocol.Ftp,
                          HostName = "ftp.vastserve.com",
                          UserName = "vasts_30905831",
                          Password = "RIHABBEJI"
                      };


                      using (Session session = new Session())
                      {
                          // Connect
                          session.Open(sessionOptions);

                          // Upload files
                          TransferOptions transferOptions = new TransferOptions();
                          transferOptions.TransferMode = TransferMode.Binary;

                          TransferOperationResult transferResult;
                          transferResult =
                                          session.PutFiles(loc_name, "/htdocs/", false, transferOptions);

                    File.Delete(loc_name);
                    // Throw on any error
                    transferResult.Check();

                          // Print results
                          foreach (TransferEventArgs transfer in transferResult.Transfers)
                          {
                              Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                          }
                      }

                      return "sa7it";
                  }
                  catch (Exception e)
                  {
                      Console.WriteLine("Error: {0}", e);
                      return "noooo";
                  }
            }

        public Stream DownloadFileFTP()
        {

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.vastserve.com/htdocs/SOEM1_TN_RADIO_LINK_POWER_20200312_001500.txt");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("vasts_30905831", "RIHABBEJI");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            return responseStream;

            /* StreamReader reader = new StreamReader(responseStream);
             Console.WriteLine(reader.ReadToEnd());






             Console.WriteLine($"Download Complete, status {response.StatusDescription}");

             reader.Close();
             response.Close();*/
        }
    }
    } 
