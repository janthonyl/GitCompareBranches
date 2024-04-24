using GitCompareBranches.Models;
using System.Diagnostics;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

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
            public DateTime dateTime { get; set; }            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                txtRepo.Text = @"C:\A\Darsy-CMO";
                populateBranchesDropDowns(txtRepo.Text);
                cboBranches1.SelectedItem = "dev";
                cboBranches2.SelectedItem = "master";
            }
            catch { }
            dgBranch1.DataSource = new BindingSource { DataSource = new List<Commit>() };
            dgBranch2.DataSource = new BindingSource { DataSource = new List<Commit>() };
            dgBranch1.CellContentClick += DgBranch1_CellContentClick;
            dgBranch2.CellContentClick += DgBranch2_CellContentClick;

        }

        private bool boolSortAscending1;
        private bool boolSortAscending2;

        private  void SortTheGrid(object? sender, DataGridViewCellEventArgs e, ref bool boolSortAscending)
        {
            DataGridView dg = (DataGridView)sender;
            if (e.ColumnIndex < 0) return; //He clicked outside the grid, i.e. in the grid margin
            if (e.RowIndex > -1) return;//He clicked a row of data, not the column header
            //Arriving here means he clicked a column header. Sort the underlying data on that column.
            string colName = dg.Columns[e.ColumnIndex].Name;
            BindingSource BS = (BindingSource)dg.DataSource;
            List<Commit> commits = (List<Commit>)BS.DataSource;
            commits.Sort(new SpecificationForSortingPropertiesOrFields<Commit>(colName, boolSortAscending));
            BS.ResetBindings(false);
            boolSortAscending = !boolSortAscending;
        }

        private void DgBranch1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            SortTheGrid(sender, e, ref boolSortAscending1);
        }
        private void DgBranch2_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            SortTheGrid(sender, e, ref boolSortAscending2);
        }


        private char space = Convert.ToChar(32);

        private List<Commit> GetTheListOfCommitsForThisBranch(string branch, string pathToRepo)
        {

            List<Commit> commits = new List<Commit>();
            //Due to embedded carriage returns, we can't use carriage return as a delimiter. Instead, 
            string prefix = "@@##@@";//use this delimiter to separate each entry of output. 
            string args = @$"log  -P <branch>   --format='<prefix>%h %s' ";
            args = args.Replace("<branch>", branch).Replace("<prefix>", prefix).Replace('\'', '\"');
            string strOutput = RunGitCommand("git", args, txtRepo.Text);
            string[] lines = strOutput.Split(new string[] { prefix }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.Length < 10) continue;
                Commit commit = new Commit();
                commit.hash = line.Substring(0, 8);
                commit.msg = line.Substring(9);
                if (commit.msg.StartsWith("Merged PR" + space)) continue;
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
            List<string> branches = GetTheListOfBranchesInThisRepo(pathToRepo);
            cboBranches1.Items.Clear();
            cboBranches2.Items.Clear();
            foreach (string branch in branches)
            {
                cboBranches1.Items.Add(branch);
                cboBranches2.Items.Add(branch);
            }
        }

        private List<string> GetTheListOfBranchesInThisRepo(string pathToRepo)
        {
            List<Commit> commits = new List<Commit>();
            string args = $"branch ";
            string strOutput = RunGitCommand("git", args, txtRepo.Text);
            string[] branches = strOutput.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            branches = branches.Select(b => b.Trim().TrimStart('*').Trim()).OrderBy(b => b).ToArray();
            return branches.ToList();
        }

        private List<Commit> In1ButButNotIn2 = new List<Commit>();
        private List<Commit> In2ButButNotIn1 = new List<Commit>();


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.Lime;
            string branch1 = cboBranches1.SelectedItem?.ToString();
            string branch2 = cboBranches2.SelectedItem?.ToString();
            if (branch1 == null || branch2 == null) return;
            List<Commit> commits1 = GetTheListOfCommitsForThisBranch(branch1, txtRepo.Text);
            List<Commit> commits2 = GetTheListOfCommitsForThisBranch(branch2, txtRepo.Text);
            Dictionary<string, Commit> all_1 = new Dictionary<string, Commit>();
            foreach (Commit c in commits1) if (!all_1.ContainsKey(c.msg)) all_1.Add(c.msg, c);
            Dictionary<string, Commit> all_2 = new Dictionary<string, Commit>();
            foreach (Commit c in commits2) if (!all_2.ContainsKey(c.msg)) all_2.Add(c.msg, c);
            In1ButButNotIn2.Clear();
            In2ButButNotIn1.Clear();
            foreach (Commit c in commits1) if (!all_2.ContainsKey(c.msg)) In1ButButNotIn2.Add(c);
            foreach (Commit c in commits2) if (!all_1.ContainsKey(c.msg)) In2ButButNotIn1.Add(c);
            In1ButButNotIn2 = RemoveCommitsInvolvingUnwantedFolders(In1ButButNotIn2);
            In2ButButNotIn1 = RemoveCommitsInvolvingUnwantedFolders(In2ButButNotIn1);
            foreach (Commit c in In2ButButNotIn1.Concat(In1ButButNotIn2)) c.dateTime = GetTheDateOfThisCommit(c.hash);
            dgBranch1.DataSource = new BindingSource { DataSource = In1ButButNotIn2 };
            dgBranch2.DataSource = new BindingSource { DataSource = In2ButButNotIn1 };
            btnRefresh.BackColor = Color.White;
        }

        private void btnFoldersToInclude_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            cboFoldersToInclude.Items.Add(fbd.SelectedPath);
        }

        private void btnClearFolders_Click(object sender, EventArgs e)
        {
            cboFoldersToInclude.Items.Clear();
        }

        private List<Commit> RemoveCommitsInvolvingUnwantedFolders(List<Commit> commits)
        {
            List<string> folders = cboFoldersToInclude.Items.OfType<string>().Select(f => f).ToList();
            folders = folders.Select(f => f.ToLower()).ToList();
            List<Commit> KeepTheseCommits = new List<Commit>();
            foreach (Commit c in commits)
            {
                bool IsMerge = ThisCommitIsAMerge(c.hash);
                if (IsMerge) continue;
                List<string> files = GetTheListOfFilesInThisCommit(c);
                bool Include = files.Any(file => folders.Any(fold => file.ToLower().StartsWith(fold)));
                if (Include) KeepTheseCommits.Add(c);
            }
            KeepTheseCommits  = KeepTheseCommits.OrderBy(k=>k.dateTime).ToList();
            return KeepTheseCommits;
        }

        private List<string> GetTheListOfFilesInThisCommit(Commit commit)
        {
            string args = @$"diff  -P commit~1 commit --name-only".Replace("commit", commit.hash);
            string strOutput = RunGitCommand("git", args, txtRepo.Text);
            string[] partialPaths =strOutput.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> fullPaths = partialPaths.Select(p => txtRepo.Text + @"\" + p).Select(f => f.Replace("/", @"\")).ToList();
            return fullPaths;
        }

        private string RunGitCommand(string exe, string args, string pathToRepo)
        {
            Process P = new Process();
            P.StartInfo = new ProcessStartInfo(exe, args);
            P.StartInfo.RedirectStandardOutput = true; //Necessary to capture the output. 
            P.StartInfo.RedirectStandardError = true;
            P.StartInfo.RedirectStandardInput = true;
            P.StartInfo.WorkingDirectory = txtRepo.Text;
            P.StartInfo.UseShellExecute = false;
            P.StartInfo.CreateNoWindow = true;
            P.Start();
            string strOutput = P.StandardOutput.ReadToEnd();
            P.WaitForExit();
            string err = P.StandardError.ReadToEnd();
            P.Close();
            if (!string.IsNullOrWhiteSpace(err)) throw new Exception(err);
            return strOutput;
        }

        private bool ThisCommitIsAMerge(string hash)
        {
            string args = @$" log --pretty=%P -n 1 commit".Replace("commit", hash);
            string strOutput = RunGitCommand("git", args, txtRepo.Text);
            string[] hashes = strOutput.Split(new char[] { space, '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            bool IsMerge = hashes.Length > 1;
            return IsMerge;
        }

        private DateTime GetTheDateOfThisCommit(string hash)
        {
            string args = @$"log commit -1 --format='%ad' --date=iso-strict".Replace("commit", hash).Replace('\'', '\"');
            string strOutput = RunGitCommand("git", args, txtRepo.Text);
            strOutput = strOutput.Replace('%'.ToString(), string.Empty);
            DateTime dateTime = DateTime.Parse(strOutput);
            return dateTime;            
        }


    }
}