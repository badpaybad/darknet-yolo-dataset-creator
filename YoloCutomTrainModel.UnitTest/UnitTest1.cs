
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace YoloCutomTrainModel.UnitTest
{
    [TestClass]
    public class YoloLoadModelByCustomTrain_UnitTest
    {
        [TestMethod]
        public void TestCustom_Model_Train()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var dirTest = "D:/robot/yolo-dataset-creator/YoloCutomTrainModel.UnitTest/test";
            string[] labels = File.ReadAllLines(Path.Combine(dirTest, "obj.names")).ToArray();

            string _pathFileImgOrigin = Path.Combine(dirTest, "hs2.jpg").Replace("\\","/");
            var fileInfo = new FileInfo(_pathFileImgOrigin);
            var org = new Mat(_pathFileImgOrigin);

            var blob = CvDnn.BlobFromImage(org, 1.0 / 255, new Size(416, 416), new Scalar(), true, false);
            
            var net = CvDnn.ReadNetFromDarknet(Path.Combine(dirTest, "yolo-obj.cfg").Replace("\\", "/")
                , Path.Combine(dirTest, "yolo-obj_last.weights").Replace("\\", "/"));

            net.SetPreferableBackend(Net.Backend.OPENCV);
            net.SetPreferableTarget(Net.Target.CPU);
            net.SetInput(blob);

            var outNames = net.GetUnconnectedOutLayersNames();
            var outs = outNames.Select(_ => new Mat()).ToArray();

            net.Forward(outs, outNames);

            var detected = ParseResult(labels, outs, org,0.5f,0.3f);
            detected = detected.OrderByDescending(i => i.Probability).Take(10).ToList();
            
            using(var sw1 =new StreamWriter(_pathFileImgOrigin+".txt",false))
            {
                sw1.Write(JsonConvert.SerializeObject(detected));
                sw1.Flush();
            }

            foreach(var d in detected)
            {
                org.Rectangle(new Point(d.X, d.Y), new Point(d.CenterX + d.Width / 2, d.CenterY + d.Width / 2), Scalar.Red , 2);
                var textSize = Cv2.GetTextSize(d.Lable, HersheyFonts.HersheyPlain, 1.5, 1, out var baseline);

                Cv2.PutText(org, d.Lable +$" - c:{d.Confidence} p:{d.Probability}", new Point(d.X, d.Y), HersheyFonts.HersheyPlain, 1.5, Scalar.Red);

                org.Rectangle(new Point(d.CenterX, d.CenterY), new Point(d.CenterX + 5, d.CenterY + 5), Scalar.Blue, 5);
            }

            sw.Stop();

            string fileName = _pathFileImgOrigin + ".drawed" + fileInfo.Extension;
            org.SaveImage(fileName);
            
            
        }

        public List<PredictYoloResult> ParseResult(string[] labels, IEnumerable<Mat> output, Mat imgToPredict, float threshold = 0.5f, float nmsThreshold = 0.3f, bool nms = false)
        {
            //for nms
            var classIds = new List<int>();
           
            /*
             YOLO3 COCO trainval output
             0 1 : center                    2 3 : w/h
             4 : confidence                  5 ~ 84 : class probability 
            */
            const int prefix_idx5 = 5;   //skip 0~4
            List<PredictYoloResult> results = new List<PredictYoloResult>();
          
            foreach (var prob in output)
            {

                for (var j = 0; j < prob.Rows; j++)
                {
                    var confidence = prob.At<float>(j, 4);
                    if (confidence < threshold)
                    {
                        continue;
                    }
                    //get classes probability
                    Cv2.MinMaxLoc(prob.Row(j).ColRange(prefix_idx5, prob.Cols),out _, out double maxVal, out _, out Point max);
                    //var classes = max.X;

                    var probability = prob.At<float>(j, max.X + prefix_idx5);

                    //if (probability < threshold) //more accuracy, you can cancel it
                    //{
                    //    continue;
                    //}

                    //get center and width/height
                    var centerX = prob.At<float>(j, 0) * imgToPredict.Width;
                    var centerY = prob.At<float>(j, 1) * imgToPredict.Height;
                    var width = prob.At<float>(j, 2) * imgToPredict.Width;
                    var height = prob.At<float>(j, 3) * imgToPredict.Height;

                    //put data to list for NMSBoxes
                    classIds.Add(max.X);

                    results.Add(new PredictYoloResult
                    {
                        CenterX = centerX,
                        CenterY = centerX,
                        Width = width,
                        Height = height,
                        ClassesId = max.X,
                        Confidence = confidence,
                        Probability = probability,
                        Lable = labels[max.X],
                        X = (centerX - width / 2) < 0 ? 0 : centerX - width / 2, //avoid left side over edge
                        Y = centerY - height / 2
                    });
                }
            }
            if (!nms) return results;

            Dictionary<Rect2d, PredictYoloResult> maped = new Dictionary<Rect2d, PredictYoloResult>();
            List<float> confidences = new List<float>();

            foreach (var r in results)
            {
                maped.Add(new Rect2d { Height = r.Height, Width = r.Width, X = r.CenterX, Y = r.CenterY }, r);
            }

            //using non-maximum suppression to reduce overlapping low confidence box
            //CvDnn.NMSBoxes(boxes, confidences, threshold, nmsThreshold, out int[] indices);

            List<Rect2d> boxies = maped.Keys.ToList();

            CvDnn.NMSBoxes(maped.Keys, confidences, threshold, nmsThreshold, out int[] indices);

            List<PredictYoloResult> filtered = new List<PredictYoloResult>();

            foreach (var i in indices)
            {
                var box = boxies[i];
                if (maped.TryGetValue(box, out PredictYoloResult r))
                {
                    filtered.Add(r);
                }
            }

            return filtered;

        }

        // [TestMethod]
        // public void TestMethod1()
        // {
        //     var dirTest = "C:/robot/yolo-dataset-creator/YoloCutomTrainModel.UnitTest/test";
        //     string[] Labels = File.ReadAllLines(Path.Combine(dirTest, "obj.names")).ToArray();

        //     string _pathFileImgOrigin = Path.Combine(dirTest, "1 (10).jpg");

        //     var fileInfo = new FileInfo(_pathFileImgOrigin);
        //     var _imgOriginFileExt = fileInfo.Extension;

        //     Image<Bgr, byte> _imgOrigin = new Image<Bgr, byte>(_pathFileImgOrigin);


        //     var net = Emgu.CV.Dnn.DnnInvoke.ReadNetFromDarknet(Path.Combine(dirTest, "yolo-obj.cfg"),
        //       Path.Combine(dirTest, "yolo-obj_final.weights"));

        //     var l = net.LayerNames;

        //     net.SetPreferableBackend(Emgu.CV.Dnn.Backend.OpenCV);
        //     /*
        //0:DNN_BACKEND_DEFAULT 
        //1:DNN_BACKEND_HALIDE 
        //2:DNN_BACKEND_INFERENCE_ENGINE
        //3:DNN_BACKEND_OPENCV 
        // */
        //     net.SetPreferableTarget(0);
        //     /*
        //     0:DNN_TARGET_CPU 
        //     1:DNN_TARGET_OPENCL
        //     2:DNN_TARGET_OPENCL_FP16
        //     3:DNN_TARGET_MYRIAD 
        //     4:DNN_TARGET_FPGA 
        //      */
        //     Mat blob = Emgu.CV.Dnn.DnnInvoke.BlobFromImage(_imgOrigin.Mat, 1 / 255, new Size(416, 416), new MCvScalar(), true, false);

        //     net.SetInput(blob);

        //     //get output layer name
        //     var outNames = net.UnconnectedOutLayersNames;
        //     //create mats for output layer
        //     var outs = outNames.Select(_ => new Mat()).ToArray();

        //     net.Forward(outs, outNames);



        //     _imgOrigin.Save(_pathFileImgOrigin + $"detected.{_imgOriginFileExt}");
        // }



    }

    public class PredictYoloResult
    {
        public int ClassesId { get; set; }
        public string Lable { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }

        public float Confidence { get; set; }
        public float Probability { get; set; }

    }


}
