using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace final_osproject
{
    public partial class Form2 : Form
    {
        file_info[] all_file = new file_info[200];
        int all_file_num = 0;
        bool cnt_state = false;
        Thread thread;
        struct read_info
        {
            public int left;
            public int BLOCK_SIZE;
            public int nr;
            public int f_pos;
            public int chars;
        }
        class file_info
        {
            public int i_mode;
            public int i_uid;
            public int i_size;
            public int i_atime;
            public int i_gid;
            public string filp;
            public int read_info_num = 0;
            public read_info[] this_info = new read_info[30];
            public int mod_i_atime;
        }
        void myinit()
        {
           StreamReader sr = new StreamReader("./数据/read/过滤数据.txt", Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                all_file[all_file_num] = new file_info();
                int len = line.Length;
                int j = "inode->i_mode=".Length;
                int i_mode = 0;
                for (;j < len; j++)
                {
                    i_mode = i_mode * 10 + line[j] - '0';
                }
                all_file[all_file_num].i_mode = i_mode;
                line = sr.ReadLine();
                len = line.Length;
                j = "inode->i_uid=".Length;
                int i_uid = 0;
                for (; j < len; j++)
                {
                    i_uid = i_uid * 10 + line[j] - '0';
                }
                all_file[all_file_num].i_uid = i_uid;
                line = sr.ReadLine();
                len = line.Length;
                j = "inode->i_size=".Length;
                int i_size = 0;
                for (; j < len; j++)
                {
                    i_size = i_size * 10 + line[j] - '0';
                }
                all_file[all_file_num].i_size = i_size;
                line = sr.ReadLine();
                len = line.Length;
                j = "inode->i_atime=".Length;
                int i_atime = 0;
                for (; j < len; j++)
                {
                    i_atime = i_atime * 10 + line[j] - '0';
                }
                all_file[all_file_num].i_atime = i_atime;
                line = sr.ReadLine();
                len = line.Length;
                j = "inode->i_gid=".Length;
                int i_gid = 0;
                for (; j < len; j++)
                {
                    i_gid = i_gid * 10 + line[j] - '0';
                }
                all_file[all_file_num].i_gid = i_gid;
                line = sr.ReadLine();
                len = line.Length;
                j = "filp=".Length;
                string filp = "";
                for (; j < len; j++)
                {
                    filp = filp + line[j];
                }
                all_file[all_file_num].filp = filp;
                while ((line = sr.ReadLine())[0] == 'l')
                {
                    len = line.Length;
                    j = "left=".Length;
                    int left = 0;
                    for (; j < len; j++)
                    {
                        left = left * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].this_info[all_file[all_file_num].read_info_num].left = left;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "BLOCK_SIZE=".Length;
                    int BLOCK_SIZE = 0;
                    for (; j < len; j++)
                    {
                        BLOCK_SIZE = BLOCK_SIZE * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].this_info[all_file[all_file_num].read_info_num].BLOCK_SIZE = BLOCK_SIZE;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "nr=".Length;
                    int nr = 0;
                    for (; j < len; j++)
                    {
                        nr = nr * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].this_info[all_file[all_file_num].read_info_num].nr = nr;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "filp->f_pos=".Length;
                    int f_pos = 0;
                    for (; j < len; j++)
                    {
                        f_pos = f_pos * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].this_info[all_file[all_file_num].read_info_num].f_pos = f_pos;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "chars=".Length;
                    int chars = 0;
                    for (; j < len; j++)
                    {
                        chars = chars * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].this_info[all_file[all_file_num].read_info_num].chars = chars;
                    all_file[all_file_num].read_info_num++;
                }
                len = line.Length;
                j = "i_atime=".Length;
                int i_mod = 0;
                for (; j < len; j++)
                {
                    i_mod = i_mod * 10 + line[j] - '0';
                }
                all_file[all_file_num].mod_i_atime = i_mod;
                line = sr.ReadLine();
                all_file_num++;
            }
        }
        public Form2()
        {
            myinit();
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void start_ana()
        {
            Label temLabel;
            Color temColor;
            for (int i = 0; i < all_file_num; i++)
            {
                string filp = "Filp:" + all_file[i].filp;
                int len = filp.Length;
                for (int j = 0; j < len; j++)
                {
                    file_lable_2.Text = filp.Substring(0, j + 1);
                    Thread.Sleep(1000);
                }
                int i_mode = all_file[i].i_mode;
                temLabel = (Label)this.GetType().GetField("i_mode_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                temLabel.Text = i_mode + "";
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;

                int i_uid = all_file[i].i_uid;
                temLabel = (Label)this.GetType().GetField("i_uid_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                temLabel.Text = i_uid + "";
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;

                int i_size = all_file[i].i_size;
                temLabel = (Label)this.GetType().GetField("i_size_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                temLabel.Text = i_size + "";
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;

                int i_atime = all_file[i].i_atime;
                temLabel = (Label)this.GetType().GetField("i_atime_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                temLabel.Text = i_atime + "";
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;

                int i_gid = all_file[i].i_gid;
                temLabel = (Label)this.GetType().GetField("i_gid_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                temLabel.Text = i_gid + "";
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;
                for (int j = 0; j < all_file[i].read_info_num; j++)
                {
                    string temp = "BLOCK_SIZE=" + all_file[i].this_info[j].BLOCK_SIZE;
                    len = temp.Length;
                    for (int k = 0; k < len; k++)
                    {
                        temLabel = (Label)this.GetType().GetField("BLOCK_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                        
                        temLabel.Text = temp.Substring(0, k + 1);

                        Thread.Sleep(1000);
                    }
                    temp = "left=" + (all_file[i].this_info[j].left + all_file[i].this_info[j].chars);
                    len = temp.Length;
                    for (int k = 0; k < len; k++)
                    {
                        temLabel = (Label)this.GetType().GetField("left_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                        temLabel.Text = temp.Substring(0, k + 1);
                        Thread.Sleep(1000);

                    }
                   temp = "nr=" + all_file[i].this_info[j].nr;
                    len = temp.Length;
                    for (int k = 0; k < len; k++)
                    {
                        temLabel = (Label)this.GetType().GetField("nr_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                        temLabel.Text = temp.Substring(0, k + 1);
                        Thread.Sleep(1000);

                    }
                    temp = "chars=" + all_file[i].this_info[j].chars;
                    len = temp.Length;
                    for (int k = 0; k < len; k++)
                    {
                        temLabel = (Label)this.GetType().GetField("chars_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

                        temLabel.Text = temp.Substring(0, k + 1);
                        Thread.Sleep(1000);

                    }
                    temLabel = (Label)this.GetType().GetField("file_" + j + "_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    temColor = temLabel.BackColor;
                    temLabel.BackColor = Color.Cyan;
                    
                    temLabel.Text = all_file[i].this_info[j].chars + "";
                   
                    Thread.Sleep(1000);
                    temLabel.BackColor = temColor;
                    Graphics g = this.CreateGraphics();

                    //出来一个画笔,这只笔画出来的颜色是红的
                    Pen p = new Pen(Brushes.Red);

                    //创建两个点
                    Point p1 = new Point(80 + 22 * j, 450);
                    Point p2 = new Point(450 + 22 * j, 190);

                    //将两个点连起来
                    g.DrawLine(p, p1, p2);

                    Thread.Sleep(1000);
                    g.Clear(this.BackColor);
                    g.Dispose();
                    temLabel = (Label)this.GetType().GetField("buffer_"+ "label_" + j, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    temColor = temLabel.BackColor;
                    temLabel.BackColor = Color.Cyan;
                    
                    temLabel.Text = all_file[i].this_info[j].chars + "";
                    Thread.Sleep(1000);
                    temLabel.BackColor = temColor;
                    f_pos_label.Left += 22;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart thStart = new ThreadStart(start_ana);//threadStart委托 
            thread = new Thread(thStart);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!cnt_state)
            {
                cnt_state = true;
                button3.Text = "继续";
                thread.Suspend();

            }
            else
            {
                cnt_state = false;
                button3.Text = "暂停";
                thread.Resume();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            Form1 f = new Form1();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void file_lable_2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void BLOCK_label_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
