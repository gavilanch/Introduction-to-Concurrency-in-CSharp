using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_5
{
    public class TaskWhenAll
    {
        private HttpClient httpClient;

        public TaskWhenAll()
        {
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var currenctDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var destinationSequential = Path.Combine(currenctDirectory, @"Imagenes\resultado-secuencial");
            var destinationParallel = Path.Combine(currenctDirectory, @"Imagenes\resultado-paralelo");
            Utils.PrepareExecution(destinationSequential, destinationParallel);

            Console.WriteLine("begin");

            var images = GetImages();

            // Sequential part

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var image in images)
            {
                await ProcessImage(destinationSequential, image);
            }

            var sequentialTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Sequential - duration in seconds: {0}",
                    sequentialTime);

            stopwatch.Restart();

            // Parallel part

            var tasks = images.Select(async image => await ProcessImage(destinationParallel, image));

            await Task.WhenAll(tasks);

            var timeParallel = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Parallel - duration in seconds: {0}",
                   timeParallel);

            Utils.WriteComparison(sequentialTime, timeParallel);

            Console.WriteLine("end");
        }

        private async Task ProcessImage(string directorio, ImageDTO imagen)
        {
            var response = await httpClient.GetAsync(imagen.URL);
            var content = await response.Content.ReadAsByteArrayAsync();

            Bitmap bitmap;
            using (var ms = new MemoryStream(content))
            {
                bitmap = new Bitmap(ms);
            }

            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            var destination = Path.Combine(directorio, imagen.Name);
            bitmap.Save(destination);
        }

        private static List<ImageDTO> GetImages()
        {
            var images = new List<ImageDTO>();
            for (int i = 0; i < 5; i++)
            {
                {
                    images.Add(
                    new ImageDTO()
                    {
                        Name = $"Spider-Man Spider-Verse {i}.jpg",
                        URL = "https://m.media-amazon.com/images/M/MV5BMjMwNDkxMTgzOF5BMl5BanBnXkFtZTgwNTkwNTQ3NjM@._V1_UY863_.jpg"
                    });
                    images.Add(

                    new ImageDTO()
                    {
                        Name = $"Spider-Man Far From Home {i}.jpg",
                        URL = "https://m.media-amazon.com/images/M/MV5BMGZlNTY1ZWUtYTMzNC00ZjUyLWE0MjQtMTMxN2E3ODYxMWVmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_UY863_.jpg"
                    });
                    images.Add(

                    new ImageDTO()
                    {
                        Name = $"Moana {i}.jpg",
                        URL = "https://lumiere-a.akamaihd.net/v1/images/r_moana_header_poststreet_mobile_bd574a31.jpeg?region=0,0,640,480"
                    });
                    images.Add(

                    new ImageDTO()
                    {
                        Name = $"Avengers Infinity War {i}.jpg",
                        URL = "https://img.redbull.com/images/c_crop,x_143,y_0,h_1080,w_1620/c_fill,w_1500,h_1000/q_auto,f_auto/redbullcom/2018/04/23/e4a3d8a5-2c44-480a-b300-1b2b03e205a5/avengers-infinity-war-poster"
                    });
                    images.Add(

                    new ImageDTO()
                    {
                        Name = $"Avengers Endgame {i}.jpg",
                        URL = "https://hipertextual.com/files/2019/04/hipertextual-nuevo-trailer-avengers-endgame-agradece-fans-universo-marvel-2019351167.jpg"
                    });
                }
            }

            return images;
        }
    }
}
