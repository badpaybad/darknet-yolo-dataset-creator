
using Newtonsoft.Json;
using RobotCloud.YoloCreatorDataTrain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotCloud.YoloCreatorDataTrain
{
    public partial class frmCollection : Form
    {
        public frmCollection()
        {
            InitializeComponent();
        }

        string _rootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

        List<string> _listFolders = new List<string>();

        private void formCollection_Load(object sender, EventArgs e)
        {
            if (lsvCollection.InvokeRequired)
            {
                lsvCollection.Invoke(new MethodInvoker(() =>
                {
                    LoadAllCollectionFromRootFolder();
                }));
            }
            else
            {

                LoadAllCollectionFromRootFolder();
            }
        }

        private void LoadAllCollectionFromRootFolder()
        {
            _listFolders.Clear();

            var dirs = Directory.GetDirectories(_rootFolder);

            lsvCollection.Items.Clear();

            lsvCollection.Columns.Clear();

            lsvCollection.Columns.Add(new ColumnHeader()
            {
                Text = "Folder name",
                Width = 150
            });
            lsvCollection.Columns.Add(new ColumnHeader()
            {
                Text = "Path",
                Width = 350
            });
            foreach (var d in dirs)
            {
                var files = Directory.GetFiles(d);
                _listFolders.Add(d);
            }

            foreach (var d in _listFolders)
            {
                DirectoryInfo di = new DirectoryInfo(d);
                ListViewItem itm = new ListViewItem(di.Name);
                itm.SubItems.Add(d);
                itm.ToolTipText = d;

                lsvCollection.Items.Add(itm);

            }
        }

        private void lsvCollection_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lsvCollection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            tsStatusBottom.Text = e.Item.SubItems[1].Text;

            if (Directory.Exists(tsStatusBottom.Text) == false) return;

            var files = Directory.GetFiles(tsStatusBottom.Text).ToList();

            files = files.Where(i => IsImageFile(i)).ToList();

            lsvFiles.Items.Clear();
            lsvFiles.Columns.Clear();
            lsvFiles.Columns.Add(new ColumnHeader { Text = "File img", Width = 500 });
            lsvFiles.Columns.Add(new ColumnHeader { Text = "Yolo data", Width = 200 });

            SplitToRun(files, async (itms) =>
            {
                if (lsvCollection.InvokeRequired)
                {
                    lsvCollection.Invoke(new MethodInvoker(() =>
                    {
                        BuildItem(itms);
                    }));
                }
                else
                {
                    BuildItem(itms);
                }
            });

            void BuildItem(List<string> itms)
            {
                foreach (var f in itms)
                {
                    var tempF = f.ToLower();
                    if (IsImageFile(tempF))
                    {
                        ListViewItem itm = new ListViewItem(f);
                        //itm.SubItems.Add(f);
                        itm.SubItems.Add(GetFileDataYolo(f));
                        lsvFiles.Items.Add(itm);
                    }
                }
            }


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

            return tempF.IndexOf(".data.json") < 0 && tempF.IndexOf(".txt") < 0
                                 && tempF.IndexOf("train.txt") < 0
                              && tempF.IndexOf("obj.data") < 0
                                && tempF.IndexOf("obj.names") < 0
                                && tempF.IndexOf(".labled") < 0
                                  && tempF.IndexOf("yolo-obj.cfg") < 0;
        }
        static string GetFileDataYolo(string imgFile)
        {
            var idx = imgFile.LastIndexOf(".");

            var file = imgFile.Substring(0, idx) + ".txt.labled";

            if (File.Exists(file)) return file;

            return string.Empty;
        }


        private void lsvCollection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsvFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            tsTxtSelectedImg.Text = e.Item.Text;
        }

        private void lsvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tsBtnLoadImage_Click(object sender, EventArgs e)
        {
            if (lsvFiles.SelectedItems.Count > 0)
            {
                frmImageLabling frm = new frmImageLabling(lsvFiles.SelectedItems[0].Text);
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Select an img first");
            }

        }

        private void tsBtnCreateYoloDataset_Click(object sender, EventArgs e)
        {

            /*obj.names
             * classes= 3
train  = C:/robot/Yolo_mark/x64/Release/koface/train.txt
valid  = C:/robot/Yolo_mark/x64/Release/koface/train.txt
names = C:/robot/Yolo_mark/x64/Release/koface/obj.names		
backup = C:/robot/Yolo_mark/x64/Release/koface/backup*/

            //classes = number of lable you have 

            //train.txt valid.txt is path file image labled

            //yolo-obj.cfg have to modify convolutional setting, at line 230 classes=
            //filters=40 # (classes + coords +1)*num

            // command line to train: darknet detector train /obj.data /yolo-obj.cfg darknet19_448.conv.23
            if (lsvCollection.SelectedItems.Count == 0) return;

            string folderLabled = lsvCollection.SelectedItems[0].SubItems[1].Text;

            List<string> classes = FindClasses_CreateFile_TrainTxt(folderLabled, out string fileTrainTxt);

            var newFileObjNames = Path.Combine(folderLabled, "obj.names");

            using (var sw = new StreamWriter(newFileObjNames, false))
            {
                foreach (var n in classes)
                {
                    sw.WriteLine(n);
                }
                sw.Flush();
            }

            var objData = string.Empty;

            using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "yolotemplate/obj.data")))
            {
                objData = sr.ReadToEnd();
            }

            var newFileObjData = Path.Combine(folderLabled, "obj.data");

            var folderWeightsBackup = Path.Combine(folderLabled, "backup");
            if (Directory.Exists(folderWeightsBackup) == false) Directory.CreateDirectory(folderWeightsBackup);

            objData = objData.Replace("classes= 3",
                $"classes= {classes.Count}");
            objData = objData.Replace("train= C:/robot/Yolo_mark/x64/Release/koface/train.txt",
                $"train= {fileTrainTxt}");
            objData = objData.Replace("valid= C:/robot/Yolo_mark/x64/Release/koface/train.txt",
                $"valid= {fileTrainTxt}");
            objData = objData.Replace("names= C:/robot/Yolo_mark/x64/Release/koface/obj.names",
                $"names= {newFileObjNames}");
            objData = objData.Replace("backup= C:/robot/Yolo_mark/x64/Release/koface/backup",
                $"backup= {folderWeightsBackup}");

            using (var sw = new StreamWriter(newFileObjData, false))
            {
                sw.WriteLine(objData);
                sw.Flush();
            }
            var dataCfgYolo = string.Empty;

            using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "yolotemplate/yolo-obj.cfg")))
            {
                dataCfgYolo = sr.ReadToEnd();
            }

            var maxBatch = classes.Count * 2000;
            maxBatch = maxBatch < 6000 ? 6000 : maxBatch;

            var step1 = maxBatch * 0.8;
            var step2 = maxBatch * 0.9;

            dataCfgYolo = dataCfgYolo.ReplaceAll("max_batches = 6000 # min 6000, classes * 2000", $"max_batches={maxBatch}");
            dataCfgYolo = dataCfgYolo.ReplaceAll("steps=4800,5400 # 80% maxbatch, 90% max_batches", $"steps={step1},{step2}");
            dataCfgYolo = dataCfgYolo.ReplaceAll("classes=2 # so id cua hoc sinh", $"classes={classes.Count}");
            dataCfgYolo = dataCfgYolo.ReplaceAll("filters=21 # (classes + 5)*3", $"filters={(classes.Count + 4 + 1) * 3}");

            var newFileCfg = Path.Combine(folderLabled, "yolo-obj.cfg");
            using (var sw = new StreamWriter(newFileCfg, false))
            {
                sw.WriteLine(dataCfgYolo);
                sw.Flush();
            }

            var darknetFile = Path.Combine("D:/robot/darknet", "darknet.exe");

            var filePreTrain = Path.Combine("D:/robot/darknet", "yolov4.conv.137");

            var cmdTrainYolo = $"\"{darknetFile}\" detector train " +
                $"\"{newFileObjData}\" " +
                $"\"{newFileCfg}\" " +
                $"\"{filePreTrain}\" -gpus 0";

            tsTxtCmdYolo.Text = cmdTrainYolo;

            using (var sw = new StreamWriter(Path.Combine(folderLabled, $"yolo.cmd.bat")))
            {
                sw.WriteLine(cmdTrainYolo);
                sw.Flush();
            }


            var dataCfgYoloTiny = string.Empty;

            using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "yolotemplate/yolov4-tiny-obj.cfg")))
            {
                dataCfgYoloTiny = sr.ReadToEnd();
            }

            dataCfgYoloTiny = dataCfgYoloTiny.ReplaceAll("max_batches = 238000 # classs * 2000 | min 6000", $"max_batches={maxBatch}");
            dataCfgYoloTiny = dataCfgYoloTiny.ReplaceAll("steps=190400,214200  # 80% maxbatches, 90% maxbatches", $"steps={step1},{step2}");
            dataCfgYoloTiny = dataCfgYoloTiny.ReplaceAll("classes=119 #lay so id cua hoc", $"classes={classes.Count}");
            dataCfgYoloTiny = dataCfgYoloTiny.ReplaceAll("filters=372 # (class +5 )*3", $"filters={(classes.Count + 5) * 3}");

            var newFileCfgTiny = Path.Combine(folderLabled, "yolov4-tiny-obj.cfg");
            using (var sw = new StreamWriter(newFileCfgTiny, false))
            {
                sw.WriteLine(dataCfgYoloTiny);
                sw.Flush();
            }

            var cmdTrainYoloTiny = $"\"{darknetFile}\" detector train " +
                $"\"{newFileObjData}\" " +
                $"\"{newFileCfgTiny}\" " +
                $"\"{filePreTrain}\" -gpus 0";

            tsTxtCmdYoloTiny.Text = cmdTrainYoloTiny;

            using (var sw = new StreamWriter(Path.Combine(folderLabled, $"yolotiny.cmd.bat")))
            {
                sw.WriteLine(cmdTrainYoloTiny);
                sw.Flush();
            }

            var faceOnly = Path.Combine(folderLabled, "yolo-face.cfg");
            using (var sw = new StreamWriter(faceOnly, false))
            {
                sw.WriteLine(dataCfgYoloTiny);
                sw.Flush();
            }

            var cmdTrainFace = $"\"{darknetFile}\" detector train " +
               $"\"{newFileObjData}\" " +
               $"\"{faceOnly}\" " +
               $"\"{filePreTrain}\" -gpus 0";

            using (var sw = new StreamWriter(Path.Combine(folderLabled, $"yoloface.cmd.bat")))
            {
                sw.WriteLine(cmdTrainFace);
                sw.Flush();
            }

        }

        private List<string> FindClasses_CreateFile_TrainTxt(string folderLabled, out string fileTrainTxt)
        {
            List<string> classes = new List<string>();

            //var datasetFolder = Path.Combine(folderLabled, "dataset");
            //if (Directory.Exists(datasetFolder) == false) Directory.CreateDirectory(datasetFolder);

            var imgsToTrain = GetImgsInFolderToTrain(folderLabled);

            fileTrainTxt = Path.Combine(folderLabled, "train.txt");
            using (var sw = new StreamWriter(fileTrainTxt, false))
            {
                foreach (var f in imgsToTrain)
                {
                    sw.WriteLine(f);
                }
                sw.Flush();
            }

            //get file img.txt.labled base on image name

            var labledFiles = GetLabledFilesInFolder(folderLabled);

            Dictionary<string, string> labledContent = new Dictionary<string, string>();
            foreach (var l in labledFiles)
            {
                using (var sr = new StreamReader(l))
                {
                    string content = sr.ReadToEnd();
                    var s = content.Split('\n')
                        .Select(i => i.Split(' ')[0].Trim(new[] { ' ', '\n', '\r' }))
                        .Where(i => !string.IsNullOrEmpty(i))
                        .ToList();
                    classes.AddRange(s);

                    labledContent.Add(l, content);
                }
            }

            classes = classes.Where(i => string.IsNullOrEmpty(i) == false).Distinct().ToList();

            //replace labled = index for img.txt

            foreach (var l in labledContent)
            {
                var fileTxt = l.Key;
                var idx = fileTxt.LastIndexOf(".txt.labled");
                if (idx > 0)
                {
                    fileTxt = fileTxt.Substring(0, idx) + ".txt";
                }

                var c = ReplaceLabelByIndexForImgTxt(l.Value, classes);

                using (var sw = new StreamWriter(fileTxt, false))
                {
                    sw.WriteLine(c);
                    sw.Flush();
                }

            }

            return classes;
        }

        string ReplaceLabelByIndexForImgTxt(string content, List<string> labels)
        {
            var tempLines = content.Split('\n').Select(i => i.Trim()).ToList();
            var newContent = "";
            foreach (var l in labels)
            {
                if (string.IsNullOrEmpty(l)) continue;

                foreach (var line in tempLines)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    if (line.StartsWith(l, StringComparison.OrdinalIgnoreCase) == false) continue;

                    var firstSpace = line.IndexOf(" ");

                    if (firstSpace > 0)
                    {
                        newContent += labels.IndexOf(l) + " " + line.Substring(firstSpace + 1).Trim() + "\n";
                    }
                }
            }

            return newContent;
        }

        private static List<string> GetLabledFilesInFolder(string folderLabled)
        {
            return Directory.GetFiles(folderLabled)
                            .Where(i => i.IndexOf(".txt.labled") > 0 && i.IndexOf("data.json") < 0
                              && i.IndexOf("train.txt") < 0
                              && i.IndexOf("obj.data") < 0
                                && i.IndexOf("obj.names") < 0
                                  && i.IndexOf("yolo-obj.cfg") < 0
                            ).Distinct().ToList();
        }

        private static List<string> GetImgsInFolderToTrain(string folderLabled)
        {
            return Directory.GetFiles(folderLabled)
                            .Where(i => i.IndexOf(".txt") < 0 && i.IndexOf("data.json") < 0
                            && i.IndexOf("train.txt") < 0
                              && i.IndexOf("obj.data") < 0
                                && i.IndexOf("obj.names") < 0
                                  && i.IndexOf("yolo-obj.cfg") < 0
                            )
                            .Where(i => IsImageFile(i))
                            .Distinct()
                            .ToList();
        }


        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsBtnRunCmd_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            //startInfo.RedirectStandardInput = true;
            //startInfo.RedirectStandardError = true;
            //startInfo.RedirectStandardOutput = true;
            // startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            process.StartInfo = startInfo;
            process.Start();

            // process.StandardInput.WriteLine(tsTxtSelectedImg.Text);
            // process.StandardInput.Flush();
            // process.StandardInput.Close();

        }

        private void tsBtnNewImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            // openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                frmImageLabling childForm = new frmImageLabling(openFileDialog.FileName);
                if (lsvCollection.SelectedItems.Count > 0)
                {
                    childForm.FolderCollection = lsvCollection.SelectedItems[0].Text;
                }
                childForm.MdiParent = this.MdiParent;
                childForm.Text = openFileDialog.FileName;
                childForm.Show();
            }
        }

        private void tsBtnRebuildPlainData_Click(object sender, EventArgs e)
        {
            if (lsvCollection.SelectedItems.Count > 0)
            {
                var collectionName = lsvCollection.SelectedItems[0].Text;
                var files = Directory.GetFiles(tsStatusBottom.Text); foreach (var f in files)
                {
                    var tempF = f.ToLower();
                    if (IsImageFile(tempF))
                    {
                        var x = new frmImageLabling(tempF) { FolderCollection = collectionName };
                        x.LoadData();
                        x.SaveData(true);
                    }
                }

                MessageBox.Show("Rebuild done: " + lsvCollection.SelectedItems[0].Text);
            }
            else
            {
                MessageBox.Show("Select collection to rebuild data for imgs");
            }

        }
        static Random _rnd = new Random();
        public static async Task SplitToRun<T>(List<T> allItems, Func<List<T>, Task> doBatch, int batchSize = 100)
        {
            if (allItems == null || allItems.Count == 0) return;

            var skip = 0;
            while (true)
            {
                var batch = allItems.Skip(skip).Take(batchSize).ToList();

                if (batch == null || batch.Count == 0) { return; }

                try
                {
                    await doBatch(batch);

                    skip = skip + batchSize;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await Task.Delay(_rnd.Next(10, 100));
                }
            }

        }

        private void tsBtnAddExistedFolder_Click(object sender, EventArgs e)
        {
            var ofd = new FolderBrowserDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var d = ofd.SelectedPath;

                DirectoryInfo di = new DirectoryInfo(d);
                ListViewItem itm = new ListViewItem(di.Name);
                itm.SubItems.Add(d);
                itm.ToolTipText = d;

                lsvCollection.Items.Add(itm);
            }
        }

        public void lsvCollection_ShowStatus(string status = "Waiting...")
        {
            if (lsvCollection.InvokeRequired)
            {
                lsvCollection.Invoke(new MethodInvoker(() => { Show(); }));
            }
            else
            {
                Show();
            }

            void Show()
            {
                lsvCollection.Items.Clear();

                lsvCollection.Columns.Clear();

                lsvCollection.Columns.Add(status,200);

                ListViewItem itm = new ListViewItem(status);
                itm.SubItems.Add(status);
                itm.ToolTipText = status;
                lsvCollection.Items.Add(itm);
            }
        }

        private void tsBtnNewCollectionFromFolder_Click(object sender, EventArgs e)
        {
            var ofd = new FolderBrowserDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lsvCollection_ShowStatus();

                Task.Run(() =>
                {
                    var d = ofd.SelectedPath;

                    AutoCreateDatasetFromFolder autoCreateDatasetFromFolder = new AutoCreateDatasetFromFolder();

                    autoCreateDatasetFromFolder.OnCreateFile += (f) =>
                    {

                    };

                    autoCreateDatasetFromFolder.OnRemain += (r) =>
                    {
                        lsvCollection_ShowStatus("Remain: " + r);
                    };

                    var t = autoCreateDatasetFromFolder.Do(d);

                    t.GetAwaiter().GetResult();

                    if (lsvCollection.InvokeRequired)
                    {
                        lsvCollection.Invoke(new MethodInvoker(() =>
                        {
                            LoadAllCollectionFromRootFolder();
                        }));
                    }
                    else
                    {
                        LoadAllCollectionFromRootFolder();
                    }

                });

            }


        }

        private void tsBtnCropRebuild_Click(object sender, EventArgs e)
        {
            string folderLabled = lsvCollection.SelectedItems[0].SubItems[1].Text;

            var listImg = Directory.GetFiles(folderLabled).Where(i => IsImageFile(i)).ToList();

            var folderOrg = Path.Combine(folderLabled, "original");
            if (Directory.Exists(folderOrg) == false) Directory.CreateDirectory(folderOrg);

            foreach(var itm in listImg)
            {
                ItemLabeled i;

                var fi = new FileInfo(itm);

                File.Move(itm, Path.Combine(folderOrg, fi.Name));

                var lbl = fi.Name.Replace(fi.Extension,"");

                var type = GetImageFormat(itm, out string extension);

                //Image<Bgr, byte> photo = new Image<Bgr, byte>(itm);

                //var detecteds = DetectFace(photo).Take(1).ToList();

                //foreach(var f in detecteds)
                //{
                //    var croped = f.Key.GetSubRect(f.Value);

                //    using (var bmpOrg = new Bitmap(new MemoryStream(croped.ToJpegData())))
                //    {
                //        string imgFileName = fi.Name;
                //        bmpOrg.Save(imgFileName);

                //        i = new ItemLabeled
                //        {
                //            Id = Guid.NewGuid(),
                //            lable = lbl,
                //            Width = bmpOrg.Width,
                //            fileImage = imgFileName,
                //            Height = bmpOrg.Height,
                //            Location = new Point(0, 0),
                //        };

                //        i.CalculateRelativeYolo();
                //    }

                //    using (var sw = new StreamWriter(Path.Combine(folderLabled, $"{fi.Name}.txt.labled"), false))
                //    {
                //        sw.WriteLine($"{i.lable} {Math.Round(i.relative_center_x, 6)} {Math.Round(i.relative_center_y, 6)} {Math.Round(i.relative_width, 6)} {Math.Round(i.relative_height, 6)}");
                //        sw.Flush();
                //    }

                //    using (var sw = new StreamWriter(Path.Combine(folderLabled, $"{fi.Name}.data.json"), false))
                //    {
                //        sw.Write(JsonConvert.SerializeObject(new List<ItemLabeled> { i }));
                //        sw.Flush();
                //    }

                //}

               
            }

        }
        //public  List<KeyValuePair<Image<Bgr, byte>, Rectangle>> DetectFace(Image<Bgr, byte> photo)
        //{
        //    using (FaceDetection _faceDetection = new FaceDetection())
        //    {
        //        var faceInPhoto = _faceDetection.DetectByHaarCascade(photo);
        //        // var faceInPhoto = _faceDetection.DetectByDnnCaffe(photo);

        //        var refilter = new List<KeyValuePair<Image<Bgr, byte>, Rectangle>>();
        //        using (FaceDetection eyesDetection = new FaceDetection())
        //        {
        //            for (int i = 0; i < faceInPhoto.Count; i++)
        //            {
        //                var f = faceInPhoto[i];
        //                var detected = eyesDetection.DetectByHaarCascade(f.Face);

        //                ////f.Key.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"_face_dnn_{i}_{Math.Abs(Guid.NewGuid().GetHashCode())}.png"));

        //                if (detected.Count > 0)
        //                {
        //                    refilter.Add(new KeyValuePair<Image<Bgr, byte>, Rectangle>(f.Face, f.FaceBound));
        //                }
        //            }
        //        }


        //        return refilter;
        //    }

        //}
        public System.Drawing.Imaging.ImageFormat GetImageFormat(string fileName, out string extension)
        {
            extension = Path.GetExtension(fileName);

            switch (extension.ToLower())
            {
                case @".bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;

                case @".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;

                case @".ico":
                    return System.Drawing.Imaging.ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;

                case @".png":
                    return System.Drawing.Imaging.ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return System.Drawing.Imaging.ImageFormat.Tiff;

                case @".wmf":
                    return System.Drawing.Imaging.ImageFormat.Wmf;

                default:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
        }

        public Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            return src.Clone(new Rectangle(x, y, width, height), src.PixelFormat);
        }
    }
}

