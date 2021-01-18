using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCloud.YoloCreatorDataTrain
{
    public class AutoCreateDatasetFromFolder
    {
        public string _dir = @"D:\robot\yolo-dataset-creator\FacenetDataset_182";

        public event Action<string> OnCreateFile;

        public event Action<int> OnRemain;

        public async Task Do(string pathDirOrg = @"D:\robot\yolo-dataset-creator\FacenetDataset_182")
        {
            _dir = pathDirOrg;

            var subfolder = Directory.GetDirectories(_dir);

            var dirName = new DirectoryInfo(_dir);

            var dirCollection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dirName.Name);

            if (Directory.Exists(dirCollection) == false) Directory.CreateDirectory(dirCollection);

            var i = 0;
            foreach (var sd in subfolder)
            {
                i++;

                int remain = subfolder.Length - i;
                OnRemain?.Invoke(remain < 0 ? 0 : remain);

                var dirNow = new DirectoryInfo(sd);
                int.TryParse(dirNow.Name, out int studentId);

                if (studentId == 0) continue;

                var files = Directory.GetFiles(sd).Where(fi => ImageHelper.IsImageFile(fi)).ToList();

                foreach (var f in files)
                {
                    OnCreateFile?.Invoke(f);

                    using (var bmp = new Bitmap(f))
                    {
                        var croped = ImageHelper.Crop(bmp, 5, 5, bmp.Width - 5, bmp.Height - 5);

                        var fiInfo = new FileInfo(f);

                        var lbl = fiInfo.Name.Replace(fiInfo.Extension, "");

                        var newFile = Path.Combine(dirCollection, lbl + fiInfo.Extension);

                        croped.Save(newFile);

                        var labeled = new ItemLabeled
                        {
                            Id = Guid.NewGuid(),
                            lable = GetIdFromFileName(lbl),
                            Width = croped.Width - 5,
                            fileImage = newFile,
                            Height = croped.Height - 5,
                            Location = new Point(2, 2),
                        };

                        labeled.CalculateRelativeYolo(bmp.Width, bmp.Height);

                        using (var sw = new StreamWriter(Path.Combine(dirCollection, $"{lbl}.txt.labled"), false))
                        {
                            sw.WriteLine($"{labeled.lable} {Math.Round(labeled.relative_center_x, 6)} {Math.Round(labeled.relative_center_y, 6)} {Math.Round(labeled.relative_width, 6)} {Math.Round(labeled.relative_height, 6)}");
                            sw.Flush();
                        }

                        using (var sw = new StreamWriter(Path.Combine(dirCollection, $"{lbl}{fiInfo.Extension}.data.json"), false))
                        {
                            sw.Write(JsonConvert.SerializeObject(new List<ItemLabeled> { labeled }));
                            sw.Flush();
                        }
                    }

                }
            }
        }
        public string GetIdFromFileName(string fileName)
        {
            return fileName.Split('_')[0];
        }
    }

}
