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
            List<Commit> commits = new List<Commit>();
            string prefix = "@@##@@";
            string args = $"log  -5  --format='<prefix>%h %s'  ";
            args = args.Replace("<prefix>", prefix).Replace('\'', '\"');
            Process P = new Process();
            P.StartInfo = new ProcessStartInfo("git", args);
            P.StartInfo.RedirectStandardOutput = true; //Necessary to capture the output. 
            P.StartInfo.WorkingDirectory = @"C:\A\Darsy-CMO";
            P.StartInfo.UseShellExecute = false;
            P.Start();
            P.WaitForExit();
            string[] lines = P.StandardOutput.ReadToEnd().Split(new string[] { prefix }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string line in lines)
            {
                if (line.Length < 10) continue;
                Commit commit = new Commit();
                commit.hash = line.Substring(0, 8);
                commit.msg = line.Substring(9);
                commits.Add(commit);
            }
            
         


        }
    }
}