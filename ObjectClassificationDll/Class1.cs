using Emgu.CV;
//using MecWise.Blazor.Api.DataAccess;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Object_Classification
{
    public class Class1
    {
        private static string publishedModelName = "Iteration1";

        public static void TestIteration(CustomVisionPredictionClient predictionApi, Project project)
        {
            VideoCapture capture = new VideoCapture(1); //create a camera captue
            Bitmap image = capture.QueryFrame().ToBitmap(); //take a picture

            //Saving photos into folder
            string filename = "file";
            image.Save(filename);
            string FileName = System.IO.Path.Combine(@"C:\Users\Admin\Downloads\JC", DateTime.Now.ToString("yyy-MM-dd-hh-mm-ss"));
            image.Save(FileName + ".jpg");
            string imageFilePath = filename;

            //MemoryStream testImage = new MemoryStream(File.ReadAllBytes(Path.Combine(@"C:\Users\User\Downloads", "2021-10-13-02-47-14.jpg"))); ;
            MemoryStream testImage = new MemoryStream(File.ReadAllBytes(imageFilePath));
            // Make a prediction against the new project
            Console.WriteLine("Making a prediction.");
            var result = predictionApi.ClassifyImage(project.Id, publishedModelName, testImage);

            // Loop over each prediction and write out the results
            //foreach (var c in result.Predictions)
            //{
            //    Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
            //}
            string predicted = "";
            //Only get the first item (Item with the highest probability)
            for (int i = 0; i < 1; i++)
            {
                //Display the item with the highest probability
                predicted = result.Predictions[i].TagName;
                Console.WriteLine(predicted);

                //Display the item AND the probability in %
                //Console.WriteLine($"\t{result.Predictions[i].TagName}: {result.Predictions[i].Probability:P1}");
            }

            //Console.WriteLine("Getting database tag");
            //string y = "select RUN_NO from MF_ITEM_TAG where TAG= '" + predicted + "'"; 

            //SqlCommand command = new SqlCommand(y, conn);
            //command.CommandTimeout = 0;

            //var dataSet = new DataSet();
            //var dataAdapter = new SqlDataAdapter { SelectCommand = command };
            //dataAdapter.Fill(dataSet);
            //string output = dataSet.Tables[0].Rows[0][0].ToString();
            //Console.WriteLine(output);



        }
    }
}
