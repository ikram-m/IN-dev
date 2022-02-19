using E5irProjet.Models;
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

        public string AddFile(string loc_name, DataParameters parms)
        {

          
               try
                  {
                      // Setup session options
                      SessionOptions sessionOptions = new SessionOptions
                      {
                          Protocol = Protocol.Ftp,
                          HostName = parms.HostName,
                          UserName = parms.UserName,
                          Password = parms.Password,

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
                                          session.PutFiles(loc_name, "/ik/", false, transferOptions);

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

        public Stream DownloadFileFTP(DataParameters parms)
        {

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://"+parms.HostName+parms.path_txt_file);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(parms.UserName, parms.Password);

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
