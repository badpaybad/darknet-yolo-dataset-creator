using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace YoloCutomTrainModel.UnitTest
{
    [TestClass]
    public class Test_580552_590576
    {
       
        [TestMethod]
        public void TestListFolder()
        {
            List<string> folders = new List<string>()
            {
            @"D:\robot\yolo-dataset-creator\585tiny",@"D:\robot\yolo-dataset-creator\585face182",@"D:\robot\yolo-dataset-creator\585tinyyolo",
            };

            foreach (var f in folders)
            {
                try
                {
                    var imgsToTest = Directory.GetFiles(f).Where(i => IsImageFile(i)).ToList();

                    foreach (var img in imgsToTest)
                    {
                        Predict(img, f, 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + f + " " + ex.Message);

                }

            }
        }

        [TestMethod]
        public void TestCustom_Model_Train20210114()
        {
            var dirConfig = @"E:\shared\du\trained\20210114\t1286";

            var imgsToTest = Directory.GetFiles(Path.Combine(dirConfig, "test")).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirConfig, 1);
            }

        }
        [TestMethod]
        public void TestCustom_Model_Train()
        {
            var dirData = @"D:\robot\yolo-dataset-creator\20210105.1";

            var imgsToTest = Directory.GetFiles(dirData).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirData, 1);
            }

        }

        [TestMethod]
        public void TestCustom_Model_Train_OmtFace()
        {
            var dirData = @"D:\robot\yolo-dataset-creator\20210106";

            var imgsToTest = Directory.GetFiles(dirData).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirData);
            }

        }

        [TestMethod]
        public void TestDetectHinhNguoi()
        {
            var dirData = @"D:\robot\yolo-dataset-creator\20210106.1";

            var imgsToTest = Directory.GetFiles(dirData).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirData);
            }

        }
        [TestMethod]
        public void TestDetect_Face585()
        {
            var dirData = @"D:\robot\yolo-dataset-creator\20210106.2";

            var imgsToTest = Directory.GetFiles(dirData).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirData, 3);
            }

        }
        [TestMethod]
        public void TestDetect_Face585_0107()
        {
            var dirData = @"D:\robot\yolo-dataset-creator\20210107";

            var imgsToTest = Directory.GetFiles(dirData).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirData, 1);
            }

        }
        private static List<String> getOutputNames(Net net)
        {            
            return net.GetUnconnectedOutLayersNames()?.ToList();            

        }

        [TestMethod]
        public void TestCustom_Model_Train20210115()
        {
            var dirConfig = @"E:\shared\du\trained\20210115\face";

            var imgsToTest = Directory.GetFiles(Path.Combine(dirConfig, "test")).Where(i => IsImageFile(i)).ToList();

            foreach (var img in imgsToTest)
            {
                Predict(img, dirConfig, 1);
            }

        }
        public void Predict(string imgPath, string dirConfig, int takeTop = int.MaxValue)
        {
            //https://answers.opencv.org/question/205657/darknet-yolo-extract-data-from-dnnforward/
            Stopwatch sw = Stopwatch.StartNew();

            var dirTest = dirConfig;
            string[] labels = File.ReadAllLines(Path.Combine(dirTest, "obj.names")).ToArray();

            var fileInfo = new FileInfo(imgPath);
            var org = new Mat(imgPath);

            var blob = CvDnn.BlobFromImage(org, 1.0 / 255, new Size(416, 416), new Scalar(), true, false);

            var fileCfg = Path.Combine(dirTest, "yolo-obj_final.weights").Replace("\\", "/");
            if (!File.Exists(fileCfg))
            {
                fileCfg = Path.Combine(dirTest, "backup/yolo-obj_final.weights").Replace("\\", "/");
            }
            if (!File.Exists(fileCfg))
            {
                fileCfg = Path.Combine(dirTest, "backup/yolo-face_final.weights").Replace("\\", "/");
            }
            if (!File.Exists(fileCfg))
            {
                fileCfg = Path.Combine(dirTest, "backup/yolo-obj_best.weights").Replace("\\", "/");
            }
            if (!File.Exists(fileCfg))
            {
                fileCfg = Path.Combine(dirTest, "backup/yolo-face_last.weights").Replace("\\", "/");
            }
            if (!File.Exists(fileCfg))
            {
                fileCfg = Path.Combine(dirTest, "backup/yolo-obj_last.weights").Replace("\\", "/");
            }
            var net = CvDnn.ReadNetFromDarknet(Path.Combine(dirTest, "yolo-obj.cfg").Replace("\\", "/")
                , fileCfg);

            var lableds = getOutputNames(net);

            net.SetInput(blob);

            var outNames = net.GetUnconnectedOutLayersNames();
            var outs = outNames.Select(_ => new Mat()).ToArray();

            net.Forward(outs, outNames);

            var detected = ParseResult(labels, outs, org, 0.5f, 0.3f);

            detected = detected.OrderByDescending(i => i.Probability).ThenByDescending(i => i.Confidence)
                .Skip(0).Take(takeTop).ToList();

            sw.Stop();

            Console.WriteLine($"{sw.ElapsedMilliseconds}");

            using (var sw1 = new StreamWriter(imgPath + ".txt", false))
            {
                sw1.Write(JsonConvert.SerializeObject(detected));
                sw1.Flush();
            }
            var dirResult = Path.Combine(dirTest, "result");
            if (Directory.Exists(dirResult) == false) Directory.CreateDirectory(dirResult);

            foreach (var d in detected)
            {
                org.Rectangle(new Point(d.X, d.Y), new Point(d.CenterX + d.Width / 2, d.CenterY + d.Width / 2), Scalar.Red, 2);
                var textSize = Cv2.GetTextSize(d.Lable, HersheyFonts.HersheyPlain, 1.5, 1, out var baseline);

                Cv2.PutText(org, d.Lable + $" - p:{d.Probability} c:{d.Confidence}",
                   new Point(0, d.CenterX - 2), HersheyFonts.HersheySimplex, 1, Scalar.White, 2);

                Cv2.PutText(org, d.Lable + $" - p:{d.Probability} c:{d.Confidence}",
                    new Point(0, d.CenterX), HersheyFonts.HersheySimplex, 1, Scalar.Blue, 2);

                org.Rectangle(new Point(d.CenterX, d.CenterY), new Point(d.CenterX + 5, d.CenterY + 5), Scalar.Blue, 2);

                string fileName = Path.Combine(dirResult, fileInfo.Name + $"-{d.Lable}-p-{d.Probability}-c-{d.Confidence}" + fileInfo.Extension);
                try { File.Delete(fileName); } catch { }

                org.SaveImage(fileName);
            }

            //org.SaveImage(imgPath + ".drawed" + fileInfo.Extension);
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
                    Cv2.MinMaxLoc(prob.Row(j).ColRange(prefix_idx5, prob.Cols), out _, out double maxVal, out _, out Point max);
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

        static bool IsImageFile(string tempF)
        {
            var extension = Path.GetExtension(tempF);

            switch (extension.ToLower())
            {
                case @".bmp":
                case @".gif":
                case @".ico":
                case @".jpg":
                case @".jpeg":
                case @".png":
                case @".tif":
                case @".tiff":
                case @".wmf":
                    return true;
                default:
                    return false;
            }
        }
    }
}
