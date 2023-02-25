using System.Diagnostics;

namespace GitCompareBranches
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class Commit
        {
            public string hash { get; set; }
            public string msg { get; set; }
            public List<FileInfo> files { get; set; }
            public Commit()
            {
                files = new List<FileInfo>();
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            txtRepo.Text = @"C:\A\Darsy-CMO";
            populateBranchesDropDowns(txtRepo.Text);
        }

        private List<Commit> GetAllCommitsForThisBranch(string branch, string pathToRepo)
        {
            List<Commit> commits = new List<Commit>();
            string prefix = "@@##@@";
            string args = $"log  -5 <branch>  --format='<prefix>%h %s'  ";
            args = args.Replace("<branch>", branch).Replace("<prefix>", prefix).Replace('\'', '\"');
            Process P = new Process();
            P.StartInfo = new ProcessStartInfo("git", args);
            P.StartInfo.RedirectStandardOutput = true; //Necessary to capture the output. 
            P.StartInfo.WorkingDirectory = pathToRepo;
            P.StartInfo.UseShellExecute = false;
            P.Start();
            P.WaitForExit();
            string[] lines = P.StandardOutput.ReadToEnd().Split(new string[] { prefix }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.Length < 10) continue;
                Commit commit = new Commit();
                commit.hash = line.Substring(0, 8);
                commit.msg = line.Substring(9);
                commits.Add(commit);
            }
            return commits;
        }

        private void btnRepo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            txtRepo.Text = fbd.SelectedPath;
            populateBranchesDropDowns(fbd.SelectedPath);
            
        }
        private void populateBranchesDropDowns(string pathToRepo)
        {
            List<string> branches = GetListOfBranchesInThisRepo(pathToRepo);
            cboBranches1.Items.Clear();
            cboBranches2.Items.Clear();
            foreach (string branch in branches)
            {
                cboBranches1.Items.Add(branch);
                cboBranches2.Items.Add(branch);
            }

        }

        private List<string> GetListOfBranchesInThisRepo(string pathToRepo)
        {
            List<Commit> commits = new List<Commit>();            
            string args = $"branch ";
            Process P = new Process();
            P.StartInfo = new ProcessStartInfo("git", args);
            P.StartInfo.RedirectStandardOutput = true; //Necessary to capture the output. 
            P.StartInfo.WorkingDirectory = pathToRepo;
            P.StartInfo.UseShellExecute = false;
            P.Start();
            P.WaitForExit();
            string[] branches = P.StandardOutput.ReadToEnd().Split(new char[] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            return branches.OrderBy(b=>b).ToList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string branch1 = cboBranches1.SelectedItem?.ToString()?.Trim()?.TrimStart('*');
            string branch2 = cboBranches2.SelectedItem?.ToString()?.Trim()?.TrimStart('*');
            if (branch1 == null || branch2 == null) return;




        }
    }
}