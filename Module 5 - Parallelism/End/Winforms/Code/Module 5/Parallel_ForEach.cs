using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_5
{
    public class Parallel_ForEach
    {
        public void btnStart_Click()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var originFolder = Path.Combine(currentDirectory, @"Imagenes\resultado-secuencial");
            var destinationSequential = Path.Combine(currentDirectory, @"Imagenes\foreach-sequential");
            var destinationParallel = Path.Combine(currentDirectory, @"Imagenes\foreach-parallel");
            Utils.PrepareExecution(destinationSequential, destinationParallel);

            var files = Directory.EnumerateFiles(originFolder);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Sequential algorithm
            foreach (var file in files)
            {
                FlipImage(file, destinationSequential);
            }

            var sequentialTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Sequential - duration in seconds: {0}",
                    sequentialTime);

            stopwatch.Restart();

            // Parallel algorithm
            Parallel.ForEach(files, file =>
            {
                FlipImage(file, destinationParallel);
            });

            var parallelTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Parallel - duration in seconds: {0}",
                   parallelTime);

            Utils.WriteComparison(sequentialTime, parallelTime);

            Console.WriteLine("end");
        }

        private void FlipImage(string file, string destinationDirectory)
        {
            using (var image = new Bitmap(file))
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                var fileName = Path.GetFileName(file);
                var destination = Path.Combine(destinationDirectory, fileName);
                image.Save(destination);
            }
        }
    }
}
