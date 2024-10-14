using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OverlayControler
{
    class Server
    {
        private int _port;

        public Server(int port) 
        { 
            _port = port;
        }

        public async void Run()
        {
            try
            {
                string url = @"http://localhost:" + _port + @"/";
                HttpListener listener = new HttpListener();

                listener.Prefixes.Clear();
                listener.Prefixes.Add(url);
                listener.Start();

                Console.WriteLine("server started.");

                while (true)
                {
                    var task = listener.GetContextAsync();
                    var context = await task;
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    // HTMLを表示する
                    if (request != null)
                    {
                        try
                        {
                            response.ContentType = Path.GetExtension(request.Url.LocalPath) switch
                            {
                                ".css" => "text/css",
                                ".html" => "text/html",
                                ".png" => "image/png",
                                ".js" => "text/javascript",
                                ".json" => "application/json",
                                ".txt" => "text/plain",
                                _ => string.Empty
                            };

                            if (response.ContentType == null || response.ContentType == string.Empty)
                            {
                                throw new FileNotFoundException();
                            }

                            string contentsDir = Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/contents/");
                            string path = Path.GetFullPath(contentsDir + request.Url.LocalPath);

                            if (!path.StartsWith(contentsDir, StringComparison.OrdinalIgnoreCase))
                            {
                                throw new Exception("invalid path.");
                            }

                            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                for (; ; )
                                {
                                    byte[] buffer = new byte[4096];
                                    int count = stream.Read(buffer, 0, buffer.Length);
                                    if (count == 0) break;
                                    response.OutputStream.Write(buffer, 0, count);
                                }
                            }
                        }
                        catch (FileNotFoundException e)
                        {
                            Console.WriteLine($"[error] {e.Message}");
                            response.StatusCode = 404;
                        }
                    }
                    else
                    {
                        response.StatusCode = 404;
                    }
                    response.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"[error] {e.Message}");
            }
        }
    }
}
