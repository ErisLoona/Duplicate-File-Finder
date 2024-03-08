using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Dupe_Finder
{
    public partial class MainMenu : Form
    {
        public List<string> selectedPaths = new List<string>();
        public bool warn = true;
        public List<FoundFiles> foundFile = new List<FoundFiles>();
        public Stopwatch sw = new Stopwatch();
        public TimeSpan[] timers = new TimeSpan[3];
        ConcurrentBag<string> tempIDbag = new ConcurrentBag<string>();

        public class FoundFiles
        {
            public string? Name;
            public string? Path;
            public byte[]? ID;
            public List<Duplicate> Duplicates = new List<Duplicate>();
        }

        public class Duplicate
        {
            public string? Name;
            public string? Path;
            public byte[]? ID;
            public bool Selected;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //cleaning paths
            if (selectedPaths.Count() > 1)
            {
                int step = 0;
                do
                {
                    bool isupmost = true;
                    for (int i = step + 1; i < selectedPaths.Count(); i++)
                    {
                        if (selectedPaths[step].Length > selectedPaths[i].Length)
                        {
                            if (selectedPaths[step].Contains(selectedPaths[i]) && selectedPaths[step].Count(bs => bs == '\\') > selectedPaths[i].Count(bs => bs == '\\'))
                            {
                                isupmost = false;
                                selectedPaths.RemoveAt(step);
                                break;
                            }
                        }
                        else
                        {
                            if (selectedPaths[i].Contains(selectedPaths[step]) && selectedPaths[step].Count(bs => bs == '\\') < selectedPaths[i].Count(bs => bs == '\\'))
                            {
                                isupmost = false;
                                selectedPaths.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (isupmost == true)
                        step++;
                } while (step < selectedPaths.Count());
            }
            #region UI Updates
            whereSearching.Items.Clear();
            whereSearching.Enabled = false;
            titleLabel.Enabled = false;
            whereSearching.Visible = false;
            titleLabel.Visible = false;
            selectLocationsButton.Enabled = false;
            selectLocationsButton.Visible = false;
            startButton.Enabled = false;
            startButton.Visible = false;
            foundList.Enabled = true;
            foundList.Visible = true;
            dupesList.Enabled = true;
            dupesList.Visible = true;
            statusLabel.Enabled = true;
            statusLabel.Visible = true;
            #endregion
            statusLabel.Refresh();
            sw.Start();
            foreach (string whereLook in selectedPaths)
                if (Directory.Exists(whereLook))
                    FindMeFiles(whereLook);
            sw.Stop();
            timers[0] = sw.Elapsed;
            sw.Reset();
            if (foundFile.Count() == 0 || foundFile.Count() == 1)
            {
                if (selectedPaths.Count() > 1)
                    MessageBox.Show("The selected locations are empty or only have folders!", "Empty locations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("The selected location is empty or only has folders!", "Empty location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                warn = false;
                cancelButton_Click(sender, EventArgs.Empty);
                return;
            }
            progressBar.Maximum = foundFile.Count();
            statusLabel.Text = "Found " + Convert.ToString(foundFile.Count()) + " files.\nHashing..";
            statusLabel.Refresh();
            sw.Start();
            thread1.RunWorkerAsync();
        }

        private void thread1_DoWork(object sender, DoWorkEventArgs e)
        {
            Parallel.ForEach(foundFile, (file) =>
            {
                using (FileStream fs = File.OpenRead(file.Path!))
                {
                    fs.Position = 0;
                    tempIDbag.Add(Convert.ToBase64String(MD5.HashData(fs)) + file.Path!);
                }
            });
        }

        private void thread1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> tempIDs = tempIDbag.ToList();
            for (int i = 0; i < foundFile.Count(); i++)
            {
                byte[] tempID = new byte[16];
                for (int j = 0; j < tempIDs.Count(); j++)
                    if (tempIDs[j].Length > foundFile[i].Path!.Length)
                        if (tempIDs[j].Substring(tempIDs[j].Length - foundFile[i].Path!.Length, foundFile[i].Path!.Length) == foundFile[i].Path!)
                        {
                            tempID = Convert.FromBase64String(tempIDs[j].Substring(0, tempIDs[j].Length - foundFile[i].Path!.Length));
                            tempIDs.RemoveAt(j);
                            break;
                        }
                FoundFiles temp = new FoundFiles();
                temp.Name = foundFile[i].Name;
                temp.Path = foundFile[i].Path;
                temp.ID = tempID;
                Duplicate tempd = new Duplicate();
                tempd.Name = foundFile[i].Name;
                tempd.Path = foundFile[i].Path;
                tempd.ID = tempID;
                tempd.Selected = true;
                temp.Duplicates!.Add(tempd);
                foundFile[i] = temp;
            }
            tempIDs.Clear();
            tempIDbag.Clear();
            sw.Stop();
            timers[1] = sw.Elapsed;
            sw.Reset();
            //sorting
            statusLabel.Text = "Hashing completed.\nFinding duplicates..";
            progressBar.Enabled = true;
            progressBar.Visible = true;
            progressBar.Refresh();
            statusLabel.Refresh();
            progressBar.Value = 0;
            sw.Start();
            int pass = 0;
            do
            {
                bool unique = true;
                for (int i = pass + 1; i < foundFile.Count(); i++)
                {
                    if (Enumerable.SequenceEqual(foundFile[pass].ID!, foundFile[i].ID!))
                    {
                        unique = false;
                        Duplicate tempd = new Duplicate();
                        tempd.Name = foundFile[i].Name;
                        tempd.Path = foundFile[i].Path;
                        tempd.ID = foundFile[i].ID;
                        tempd.Selected = false;
                        foundFile[pass].Duplicates!.Add(tempd);
                        foundFile.Remove(foundFile[i]);
                        i--;
                    }
                }
                if (unique == true)
                    foundFile.Remove(foundFile[pass]);
                else
                    pass++;
                progressBar.Value++;
            } while (pass < foundFile.Count());
            sw.Stop();
            timers[2] = sw.Elapsed;
            sw.Reset();
            if (foundFile.Count() == 0)
            {
                MessageBox.Show("No duplicates found!", "No duplicates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                warn = false;
                cancelButton_Click(returnButton, EventArgs.Empty);
                return;
            }
            #region UI Updates
            statusLabel.Enabled = false;
            statusLabel.Visible = false;
            statusLabel.Text = "Counting the files";
            progressBar.Enabled = false;
            progressBar.Visible = false;
            progressBar.Value = 0;
            keepSelected.Enabled = true;
            keepSelected.Visible = true;
            delSelected.Enabled = true;
            delSelected.Visible = true;
            returnButton.Enabled = true;
            returnButton.Visible = true;
            countedLabel.Text = "Files counted in\n" + Convert.ToString(timers[0]);
            countedLabel.Enabled = true;
            countedLabel.Visible = true;
            hashedLabel.Text = "Files hashed in\n" + Convert.ToString(timers[1]);
            hashedLabel.Enabled = true;
            hashedLabel.Visible = true;
            dupesLabel.Text = "Found duplicates in\n" + Convert.ToString(timers[2]);
            dupesLabel.Enabled = true;
            dupesLabel.Visible = true;
            #endregion
            foreach (FoundFiles file in foundFile)
                foundList.Items.Add(file.Name!);
            GC.Collect();
        }

        public void FindMeFiles(string path)
        {
            try
            {
                string[] entries = Directory.GetFiles(path);
                foreach (string entry in entries)
                {
                    FoundFiles temp = new FoundFiles();
                    temp.Path = entry;
                    temp.Name = Path.GetFileName(entry);
                    Duplicate tempd = new Duplicate();
                    tempd.Name = Path.GetFileName(entry);
                    tempd.Path = entry;
                    tempd.Selected = true;
                    temp.Duplicates!.Add(tempd);
                    foundFile.Add(temp);
                }
                string[] subdirs = Directory.GetDirectories(path);
                foreach (string subdir in subdirs)
                    FindMeFiles(subdir);
            }
            catch { }
        }

        private void keepSelectedButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete all files that are NOT selected.\nAre you sure you want to continue?", "Keep selected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            statusLabel.Enabled = true;
            statusLabel.Text = "Deleting files...";
            statusLabel.Visible = true;
            thread2.RunWorkerAsync(argument: false);
        }

        private void delSelected_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete all files that ARE selected.\nAre you sure you want to continue?", "Delete selected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            statusLabel.Enabled = true;
            statusLabel.Text = "Deleting files...";
            statusLabel.Visible = true;
            thread2.RunWorkerAsync(argument: true);
        }

        private void thread2_DoWork(object sender, DoWorkEventArgs e)
        {
            bool selected = (bool)e.Argument!;
            Parallel.ForEach(foundFile, (file) =>
            {
                foreach (Duplicate dupe in file.Duplicates)
                    if (dupe.Selected == selected)
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(dupe.Path!, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            });
        }

        private void thread2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            warn = false;
            cancelButton_Click(sender, EventArgs.Empty);
        }

        private void foundList_SelectedIndexChanged(object sender, EventArgs e)
        {
            dupesList.Items.Clear();
            if (foundList.SelectedIndex != -1)
            {
                foreach (Duplicate file in foundFile[foundList.SelectedIndex].Duplicates)
                {
                    if (file.Selected == true)
                    {
                        dupesList.ItemCheck -= dupesList_ItemCheck;
                        dupesList.Items.Add(Convert.ToString(file.Name + "  at  " + file.Path), true);
                        dupesList.ItemCheck += dupesList_ItemCheck;
                    }
                    else
                        dupesList.Items.Add(Convert.ToString(file.Name + "  at  " + file.Path), false);
                }
            }
        }

        private void dupesList_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            Duplicate tempd = new Duplicate();
            tempd.Name = foundFile[foundList.SelectedIndex].Duplicates[e.Index].Name;
            tempd.Path = foundFile[foundList.SelectedIndex].Duplicates[e.Index].Path;
            tempd.ID = foundFile[foundList.SelectedIndex].Duplicates[e.Index].ID;
            tempd.Selected = !foundFile[foundList.SelectedIndex].Duplicates[e.Index].Selected;
            foundFile[foundList.SelectedIndex].Duplicates[e.Index] = tempd;
        }

        private void dupesList_MouseDown(object sender, MouseEventArgs e)
        {
            dupesList.SelectedIndex = dupesList.IndexFromPoint(e.Location);
            if (e.Button == MouseButtons.Right)
                if (dupesList.SelectedIndex != -1)
                    dupeOpen.Show(Cursor.Position);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "/select," + foundFile[foundList.SelectedIndex].Duplicates[dupesList.SelectedIndex].Path);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process f = new Process();
            f.StartInfo = new ProcessStartInfo(foundFile[foundList.SelectedIndex].Duplicates[dupesList.SelectedIndex].Path!)
            {
                UseShellExecute = true
            };
            f.Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (warn == true)
                if (MessageBox.Show("Are you sure?\nYour progress will be lost.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            #region UI Updates
            selectLocationsButton.Enabled = true;
            selectLocationsButton.Visible = true;
            startButton.Visible = true;
            foundList.Enabled = false;
            foundList.Visible = false;
            foundList.Items.Clear();
            dupesList.Enabled = false;
            dupesList.Visible = false;
            dupesList.Items.Clear();
            progressBar.Enabled = false;
            progressBar.Visible = false;
            progressBar.Value = 0;
            statusLabel.Enabled = false;
            statusLabel.Visible = false;
            statusLabel.Text = "Counting the files";
            keepSelected.Enabled = false;
            keepSelected.Visible = false;
            delSelected.Enabled = false;
            delSelected.Visible = false;
            returnButton.Enabled = false;
            returnButton.Visible = false;
            countedLabel.Text = "Files counted in";
            countedLabel.Enabled = false;
            countedLabel.Visible = false;
            hashedLabel.Text = "Files hashed in";
            hashedLabel.Enabled = false;
            hashedLabel.Visible = false;
            dupesLabel.Text = "Found duplicates in";
            dupesLabel.Enabled = false;
            dupesLabel.Visible = false;
            #endregion
            selectedPaths.Clear();
            foundFile.Clear();
            GC.Collect();
            warn = true;
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (returnButton.Visible == true)
                if (MessageBox.Show("You will need to search for duplicates again.\nAre you sure you want to exit?", "No deletions", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    e.Cancel = true;
        }

        private void selectLocationsButton_Click(object sender, EventArgs e)
        {
            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                titleLabel.Enabled = true;
                titleLabel.Visible = true;
                whereSearching.Enabled = true;
                whereSearching.Visible = true;
                selectedPaths.Add(folderSelector.SelectedPath);
                whereSearching.Items.Add(folderSelector.SelectedPath);
                startButton.Enabled = true;
            }
        }

        private void whereSearching_MouseDown(object sender, MouseEventArgs e)
        {
            whereSearching.SelectedIndex = whereSearching.IndexFromPoint(e.Location);
            if (e.Button == MouseButtons.Right)
                if (whereSearching.SelectedIndex != -1)
                    locationDelete.Show(Cursor.Position);
        }

        private void removeLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedPaths.RemoveAt(whereSearching.SelectedIndex);
            whereSearching.Items.RemoveAt(whereSearching.SelectedIndex);
            if (whereSearching.Items.Count == 0)
            {
                titleLabel.Enabled = false;
                titleLabel.Visible = false;
                whereSearching.Enabled = false;
                whereSearching.Visible = false;
                startButton.Enabled = false;
            }
        }

        public MainMenu()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
