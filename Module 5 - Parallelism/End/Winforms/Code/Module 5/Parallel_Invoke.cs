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
    public class Parallel_Invoke
    {
        public void btnStart_Click()
        {
            // Preparing code for transforming images
            var currenctDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var carpetaOrigen = Path.Combine(currenctDirectory, @"Imagenes\resultado-secuencial");
            var carpetaDestinoSecuencial = Path.Combine(currenctDirectory, @"Imagenes\foreach-secuencial");
            var carpetaDestinoParalelo = Path.Combine(currenctDirectory, @"Imagenes\foreach-paralelo");
            Utils.PrepareExecution(carpetaDestinoSecuencial, carpetaDestinoParalelo);
            var archivos = Directory.EnumerateFiles(carpetaOrigen);

            // Preparing code for matrices
            int colsMatrixA = 208;
            int rows = 1240;
            int colsMatrixB = 750;
            var matrixA = Matrices.InitializeMatrix(rows, colsMatrixA);
            var matrixB = Matrices.InitializeMatrix(colsMatrixA, colsMatrixB);
            var resultado = new double[rows, colsMatrixB];

            Action multiplyMatrices = () => Matrices.MultiplyMatricesSequential(matrixA, matrixB, resultado);
            Action transformImages = () =>
            {
                foreach (var archivo in archivos)
                {
                    TransformImage(archivo, carpetaDestinoSecuencial);
                }
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Sequential part
            multiplyMatrices();
            transformImages();

            var sequentialTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Sequential - duration in seconds: {0}",
                    sequentialTime);

            Utils.PrepareExecution(carpetaDestinoSecuencial, carpetaDestinoParalelo);

            stopwatch.Restart();

            // Parallel part
            Parallel.Invoke(transformImages, multiplyMatrices);

            var parallelTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Parallel - duration in seconds: {0}",
                   parallelTime);

           Utils.WriteComparison(sequentialTime, parallelTime);

            Console.WriteLine("end");
        }

        private void TransformImage(string file, string destinationDirectory)
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
