using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotCloud.YoloCreatorDataTrain
{
    public partial class frmImageLabling : Form
    {
        string _fileImg;
        public string FolderCollection { get; set; }
        public frmImageLabling(string file)
        {
            _fileImg = file;

            InitializeComponent();

            pictureDrawer = new PictureDrawer(picMain, this);

            pictureDrawer.Load(_fileImg);


        }
        PictureDrawer pictureDrawer;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            FileInfo fi = new FileInfo(_fileImg);

            if (!string.IsNullOrEmpty(FolderCollection))
            {
                tsLblCollectionName.Text = FolderCollection;
            }
            //else
            //{
            //    tsLblCollectionName.Text = fi.Directory.Name;
            //}

            this.MouseMove += Form1_MouseMove;

            pictureDrawer.ItemActived += PictureDrawer_ItemActived;
            pictureDrawer.ItemDrawDone += PictureDrawer_ItemDrawDone;
            pictureDrawer.ItemDeleted += PictureDrawer_ItemDeleted;

            LoadGrid();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = e.Location.ToString();

        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            //OpenFileDialog opf = new OpenFileDialog();
            //opf.Multiselect = false;
            //if (opf.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}

        }

        private void PictureDrawer_ItemDeleted(PictureDrawer arg1, Guid arg2)
        {
            LoadGrid();
        }

        private void PictureDrawer_ItemDrawDone(PictureDrawer arg1, CustomGroupBox arg2)
        {
            LoadGrid();
        }

        private void PictureDrawer_ItemActived(PictureDrawer arg1, CustomGroupBox arg2)
        {
            LoadGrid(arg2._BoxId);
        }

        List<ItemLabeled> _items = new List<ItemLabeled>();
        void LoadGrid(Guid? idSelected = null)
        {
            if (idSelected == null)
            {
                pictureDrawer._idBoxCurrent = Guid.Empty;
            }

            _items = new List<ItemLabeled>();
            foreach (var i in pictureDrawer._draweds)
            {
                _items.Add(new ItemLabeled
                {
                    Id = i.Value._BoxId,
                    fileImage = i.Value._PathFile,
                    Height = i.Value.Height,
                    lable = i.Value._Lable,
                    Location = i.Value.Location,
                    relative_center_x = i.Value.relative_center_x,
                    relative_center_y = i.Value.relative_center_y,
                    relative_height = i.Value.relative_height,
                    relative_width = i.Value.relative_width,
                    Width = i.Value.Width,
                    Color = i.Value._BorderColor,
                    Order = i.Value._order
                });
            }

            dgvLabels.DataSource = _items;

            Task.Run(() =>
            {
                Thread.Sleep(100);
                if (dgvLabels == null) return;
                if (dgvLabels.InvokeRequired)
                {
                    dgvLabels.Invoke(new MethodInvoker(() =>
                    {
                        BuildItem();
                    }));

                }
                else
                {
                    BuildItem();
                }


                void BuildItem()
                {
                    foreach (DataGridViewRow r in dgvLabels.Rows)
                    {
                        var id = Guid.Parse(r.Cells["Id"].Value.ToString());
                        if (pictureDrawer._draweds.TryGetValue(id, out CustomGroupBox g))
                        {
                            var fontColor = OppositColor(g._BorderColor);
                            r.Cells["lable"].Style.BackColor = g._BorderColor;
                            r.Cells["Id"].Style.BackColor = g._BorderColor;
                            r.Cells["fileImage"].Style.BackColor = g._BorderColor;
                            r.Cells["Width"].Style.BackColor = g._BorderColor;
                            r.Cells["Height"].Style.BackColor = g._BorderColor;

                            if (idSelected != null && id == idSelected.Value)
                            {
                                dgvLabels.CurrentCell = r.Cells["lable"];
                                r.Cells["lable"].Style.BackColor = g._BorderColor;
                                r.Cells["Id"].Style.BackColor = g._BorderColor;
                                r.Cells["fileImage"].Style.BackColor = g._BorderColor;
                                r.Cells["Width"].Style.BackColor = g._BorderColor;
                                r.Cells["Height"].Style.BackColor = g._BorderColor;
                                dgvLabels.BeginEdit(true);
                            }
                        }

                    }
                }
            });
        }

        public Color OppositColor(Color cl)
        {
            return Color.FromArgb(255 - cl.R, 255 - cl.G, 255 - cl.B);
        }

        Guid _itmIdSelected;

        private void dgvLabels_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var id = Guid.Parse(dgvLabels.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            pictureDrawer.BringToFront(id);
        }

        private void dgvLabels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //PrepareEdit(sender, e);
        }

        private void dgvLabels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Guid.Parse(dgvLabels.Rows[e.RowIndex].Cells["Id"].Value.ToString());

            PrepareEdit(sender, id);
        }
        void PrepareEdit(object sender, Guid id)
        {
            _itmIdSelected = id;
            pictureDrawer.FocusItem(id);
        }

        private void dgvLabels_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLabels_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            AutoSaveOnEdit(sender, e);
        }

        private void dgvLabels_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            AutoSaveOnEdit(sender, e);
        }

        void AutoSaveOnEdit(object sender, DataGridViewCellEventArgs e)
        {
            var id = Guid.Parse(dgvLabels.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            _itmIdSelected = id;
            if (pictureDrawer._draweds.TryGetValue(id, out CustomGroupBox g))
            {
                var newlbl = dgvLabels.Rows[e.RowIndex].Cells["lable"].Value.ToString();
                g._Lable = newlbl;
                //update change other in need
                pictureDrawer.Refresh();
            }

        }

        string _rootDir = AppDomain.CurrentDomain.BaseDirectory;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                MessageBox.Show("Save done to: " + tsLblCollectionName.Text);
            }
        }
        public bool SaveData(bool silenceMode = false)
        {
            if (_items.Count == 0)
            {
                MessageBox.Show("No labled item(s) to save");
                return false;
            }

            var invalidItems = _items.Where(i => i.Width < 5 || i.Height < 5 || string.IsNullOrEmpty(i.lable)
            || i.relative_center_x == 0 || i.relative_center_y == 0 || i.relative_height == 0 || i.relative_width == 0).ToList();
            if (invalidItems.Count > 0)
            {
                foreach (var i in invalidItems)
                {
                    _items.Remove(i);

                    pictureDrawer.Delete(i.Id);
                }

                pictureDrawer.Refresh();
                LoadGrid();
            }

            var folder = string.IsNullOrEmpty(tsLblCollectionName.Text) ? "yolodataset" : tsLblCollectionName.Text;
            if (!silenceMode)
            {
                if (string.IsNullOrEmpty(folder) || folder.Equals("..."))
                {
                    var dir = Interaction.InputBox($"Yolo folder under: {_rootDir}", "Yolo folder name", folder);

                    if (string.IsNullOrEmpty(dir) || dir.Equals("..."))
                    {
                        MessageBox.Show("No collection (folder) to save");
                        return false;
                    }
                    else
                    {
                        tsLblCollectionName.Text = dir;
                        folder = dir;
                    }
                }
            }

            var dirYolo = Path.Combine(_rootDir, folder);

            if (!Directory.Exists(dirYolo)) Directory.CreateDirectory(dirYolo);

            var f = new FileInfo(_fileImg);

            var nf = Path.Combine(dirYolo, f.Name);

            try
            {
                if (_fileImg != nf)
                {
                    File.Copy(_fileImg, nf, true);
                }
            }
            catch { }

            var jsonFile = Path.Combine(dirYolo, $"{f.Name}.data.json");

            using (var sw = new StreamWriter(jsonFile, false))
            {
                sw.Write(JsonConvert.SerializeObject(_items));
                sw.Flush();
            }

            var yoloFileTxt = Path.Combine(dirYolo, $"{NameWithOutExtension(f)}.txt");

            using (var sw = new StreamWriter(yoloFileTxt, false))
            {
                foreach (var i in _items)
                {
                    sw.WriteLine($"{i.lable} {Math.Round(i.relative_center_x, 6)} {Math.Round(i.relative_center_y, 6)} {Math.Round(i.relative_width, 6)} {Math.Round(i.relative_height, 6)}");
                }
                sw.Flush();
            }

            //to keep oginial, after replace index in frmCollection

            File.Copy(yoloFileTxt, yoloFileTxt + ".labled", true);

            return true;
        }

        string NameWithOutExtension(FileInfo i)
        {
            var name = i.Name;
            var idx = name.LastIndexOf(i.Extension);
            return name.Substring(0, idx);
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            var x = _items.Where(i => i.Id == _itmIdSelected).FirstOrDefault();
            if (x == null) return;

            var r = MessageBox.Show($"Delete lable? {x.lable}", $"Delete lable? {x.lable}", MessageBoxButtons.YesNo);
            if (r == DialogResult.No) return;

            pictureDrawer.Delete(_itmIdSelected);
            LoadGrid();
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            pictureDrawer.Refresh();
            LoadGrid();
        }
        private void frmImageLabling_FormClosing(object sender, FormClosingEventArgs e)
        {
            var r = MessageBox.Show("CLOSE? NO SAVE CHANGE", "CLOSE? no save change", MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                return;
            }

            e.Cancel = true;
            return;

        }

        private void dgvLabels_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLabels.SelectedRows.Count == 0) return;

            var id = Guid.Parse(dgvLabels.SelectedRows[0].Cells["Id"].Value.ToString());

            PrepareEdit(sender, id);
        }
    }
}
