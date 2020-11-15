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
using System.Media;

namespace final_osproject
{

    public partial class Form1 : Form
    {
        int all_file_num = 0;
        file_info[] all_file = new file_info[200];
        int current = -1;
        bool cnt_state = false;
        Thread thread;
        struct file_table_info
        {
            public int f_mode;
            public int f_flags;
            public int f_count;
            public int f_inode_off;
            public string f_inode_add;
            public int f_pos;
            public int repeat;
            
        }
        class file_info
        {
            public int pid;
            public int state;
            public string filename_add;
            public string filename;
            public string filp_add;
            public int filp_off;
            public int fd;
            public bool is_error = false;
            public int close_on_exec;
            public int file_table_num;
            public int f_mode;
            public int f_flags;
            public int f_count;
            public string f_inode_add;
            public string f_inode_off;
            public int f_pos;
            public int i_mode;
            public int i_uid;
            public int i_size;
            public string i_mtime;
            public int i_gid;
            public int i_nlinks;
            public int[] i_zone = new int[9];
            public file_table_info[] this_fti = new file_table_info[40];
        }
        public void myinit()
        { 
            
            StreamReader sr = new StreamReader("./数据/二次过滤.txt", Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "new file")
                {
                    all_file[all_file_num] = new file_info();
                    line = sr.ReadLine();
                    int len = line.Length;
                    int j = "current-pid=".Length;
                    int pid = 0;
                    for (; j < len; j++)
                    {
                        pid = pid * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].pid = pid;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "current-state=".Length;
                    int state = 0;
                    for(; j < len; j++)
                    {
                        state = state * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].state = state;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "filename=".Length;
                    string filename_add = "";
                    for (;j < len && line[j] !=  '#'; j++)
                    {
                        filename_add = filename_add + line[j];
                    }
                    j += 3;
                    string filename = "";
                    for (; j < len; j++)
                    {
                        filename = filename + line[j];
                    }
                    all_file[all_file_num].filename_add = filename_add;
                    all_file[all_file_num].filename = filename;
                    line = sr.ReadLine();
                    len = line.Length;
                    j = "fd=".Length;
                    int fd = 0;
                    for (;j < len; j++)
                    {
                        fd = fd * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].fd = fd;
                    line = sr.ReadLine();
                    len = line.Length;
                    if (line == "ERROR")
                    {
                        all_file[all_file_num].is_error = true;
                        all_file_num++;
                        continue;
                    }
                    j = "current-close_on_exec=".Length;
                    int close_on_exec = 0;
                    for(;j < len; j++)
                    {
                        close_on_exec = close_on_exec * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].close_on_exec = close_on_exec;
                    line = sr.ReadLine();
                    bool flag = false;
                    if ((line = sr.ReadLine()) == "file table")
                    {
                        while ((!flag && (line = sr.ReadLine()) == "new term") || (flag && line == "new term"))
                        {
                            flag = false;
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num] = new file_table_info();
                            line = sr.ReadLine();
                            len = line.Length;
                            j = "f_mode=".Length;
                            int f_mode = 0;
                            for (; j < len; j++)
                            {
                                f_mode = f_mode * 10 + line[j] - '0';
                            }
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].f_mode = f_mode;
                            line = sr.ReadLine();
                            len = line.Length;
                            j = "f_flags=".Length;
                            int f_flags = 0;
                            for (; j < len; j++)
                            {
                                f_flags = f_flags * 10 + line[j] - '0';
                            }
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].f_flags = f_flags;
                            line = sr.ReadLine();
                            len = line.Length;
                            j = "f_count=".Length;
                            int f_count = 0;
                            for (; j < len; j++)
                            {
                                f_count = f_count * 10 + line[j] - '0';
                            }
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].f_count = f_count;
                            line = sr.ReadLine();
                            len = line.Length;
                            j = "f_inode=".Length;
                            string f_node_add = "";
                            for (;line[j] != '#'; j++)
                            {
                                f_node_add = f_node_add + line[j];
                            }
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].f_inode_add = f_node_add;
                            j += 3;
                            int f_node_off = 0;
                            for (;j < len; j++)
                            {
                                f_node_off = f_node_off * 10 + line[j] - '0';
                            }
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].f_inode_off = f_node_off;
                            line = sr.ReadLine();
                            len = line.Length;
                            j = "f_pos=".Length;
                            int f_pos = 0;
                            for (;j < len; j++)
                            {
                                f_pos = f_pos * 10 + line[j] - '0';
                            }
                            all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].f_pos = f_pos;
                            line = sr.ReadLine();
                            len = line.Length;
                            if (len > 0 && line[0] == 'r')
                            {
                                j = "repeats=".Length;
                                int repeats = 0;
                                for (; line[j] != 't'; j++)
                                {
                                    repeats = repeats * 10 + line[j] - '0';
                                }
                                all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].repeat = repeats;
                            }
                            else
                            {
                                flag = true;
                                all_file[all_file_num].this_fti[all_file[all_file_num].file_table_num].repeat = 1;
                            }
                            all_file[all_file_num].file_table_num++;
                        }
                    }
                    len = line.Length;
                    string filp_add = "";
                    int filp_off = 0;
                    j = "filp=".Length;
                    for (;j < len && line[j] != '#'; j++)
                    {
                        filp_add = filp_add + line[j];
                    }
                    j += 3;
                    for (;j < len; j++)
                    {
                        filp_off = filp_off * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].filp_add = filp_add;
                    all_file[all_file_num].filp_off = filp_off;
                    line = sr.ReadLine();
                    len = line.Length;
                    int ff_mode = 0;
                    j = "f_mode=".Length;
                    for (;j < len; j++)
                    {
                        ff_mode = ff_mode * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].f_mode = ff_mode;
                    line = sr.ReadLine();
                    len = line.Length;
                    int ff_flags = 0;
                    j = "f_flags=".Length;
                    for (; j < len; j++)
                    {
                        ff_flags = ff_flags * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].f_flags = ff_flags;
                    line = sr.ReadLine();
                    len = line.Length;
                    int ff_count = 0;
                    j = "f_count=".Length;
                    for (;j < len; j++)
                    {
                        ff_count = ff_count * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].f_count = ff_count;
                    line = sr.ReadLine();
                    len = line.Length;
                    string ff_inode_add = "";
                    j = "f_inode=".Length;
                    for (;j < len && line[j] != '#'; j++)
                    {
                        ff_inode_add = ff_inode_add + line[j];
                    }
                    all_file[all_file_num].f_inode_add = ff_inode_add;
                    j += 3;
                    string ff_inode_off = "";
                    for(; j < len; j++)
                    {
                        ff_inode_off = ff_inode_off + line[j];
                    }
                    all_file[all_file_num].f_inode_off = ff_inode_off;
                    line = sr.ReadLine();
                    len = line.Length;
                    int ff_pos = 0;
                    j = "f_pos=".Length;
                    for(; j < len; j++)
                    {
                        ff_pos = ff_pos * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].f_pos = ff_pos;
                    line = sr.ReadLine();
                    len = line.Length;
                    int i_mode = 0;
                    j = "i_mode=".Length;
                    for(; j < len; j++)
                    {
                        i_mode = i_mode * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].i_mode = i_mode;
                    line = sr.ReadLine();
                    len = line.Length;
                    int i_uid = 0;
                    j = "i_uid=".Length;
                    for(; j < len; j++)
                    {
                        i_uid = i_uid * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].i_uid = i_uid;
                    line = sr.ReadLine();
                    len = line.Length;
                    int i_size = 0;
                    j = "i_size=".Length;
                    for(; j < len;j++)
                    {
                        i_size = i_size * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].i_uid = i_uid;
                    line = sr.ReadLine();
                    len = line.Length;
                    string i_mtime = "";
                    j = "i_mtime=".Length;
                    for(; j < len; j++)
                    {
                        i_mtime = i_mtime + line[j];
                    }
                    all_file[all_file_num].i_mtime = i_mtime;
                    line = sr.ReadLine();
                    len = line.Length;
                    int i_gid = 0;
                    j = "i_gid=".Length;
                    for (;j < len; j++)
                    {
                        i_gid = i_gid * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].i_gid = i_gid;
                    line = sr.ReadLine();
                    len = line.Length;
                    int i_nlinks = 0;
                    j = "i_nlinks=".Length;
                    for (; j < len; j++)
                    {
                        i_nlinks = i_nlinks * 10 + line[j] - '0';
                    }
                    all_file[all_file_num].i_nlinks = i_nlinks;
                    for (int k = 0; k < 9; k++)
                    {
                        line = sr.ReadLine();
                        len = line.Length;
                        int i_zone = 0;
                        j = "i_zone[0]=".Length;
                        for (;j < len; j++)
                        {
                            i_zone = i_zone * 10 + line[j] - '0';
                        }
                        all_file[all_file_num].i_zone[k] = i_zone;
                    }
                    all_file_num++;
                }
            } 
        }
        public Form1()
        {
            myinit();
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            /*
            int max = -1;
            for (int i = 0; i < all_file_num; i++)
            {
                int temp = -1;
                if (!all_file[i].is_error)
                    temp = all_file[i].file_table_num;
                if (temp > max)
                    max = temp;
            }
            task_label.Text = max + "";
            */
        }

       
        /****************************/
        private void new_file(int id)
        {
            
            if (all_file[id].is_error)
            {
                return;   
            }
            int i = 0;
            Label temLabel;
            Color temColor;
            SoundPlayer player = new SoundPlayer();
            
            string filename = all_file[id].filename;
            string filename_add = all_file[id].filename_add;
            string text = "New File\n" + filename_add;
            for (i = 0; i < text.Length; i++)
            {
                new_file_label.Text = text.Substring(0, i + 1);
                Thread.Sleep(1000);
            }
            Graphics g = this.CreateGraphics();

            //出来一个画笔,这只笔画出来的颜色是红的
            Pen p = new Pen(Brushes.Red);

            //创建两个点
            Point p1 = new Point(900, 100);
            Point p2 = new Point(1200, 100);

            //将两个点连起来
            g.DrawLine(p, p1, p2);

            Thread.Sleep(1000);
            g.Clear(this.BackColor);
            g.Dispose();
            text = "New File\n" + filename;
            for (i = 0; i < text.Length; i++)
            {
                new_file_label.Text = text.Substring(0, i + 1);
                Thread.Sleep(1000);
            }
            
            int fd = all_file[id].fd;
            for (i = 0; i < fd; i++)
            {
                player.SoundLocation = "F:/操作系统/课程设计/数据/mengling.wav";
                player.Play();
                Thread.Sleep(1000);
                temLabel = (Label)this.GetType().GetField("filp_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Red;
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;
            }
            player.SoundLocation = "./数据/mengling.wav";
            player.Play();
            Thread.Sleep(1000);
            temLabel = (Label)this.GetType().GetField("filp_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;
            temLabel = (Label)this.GetType().GetField("filp_con_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.Text = "";
            int close_on_exec = all_file[id].close_on_exec;
            for (i = 0; i < 32; i++)
            {
                temLabel = (Label)this.GetType().GetField("close_on_exec_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                string temStr = Convert.ToString(close_on_exec >> i, 2);
                temLabel.Text = temStr.Substring(temStr.Length - 1);
                Thread.Sleep(1000);
                temLabel.BackColor = temColor;
            }
            string filp_add = all_file[id].filp_add;
            text = "Filp address:" + filp_add;
            for (i = 0; i < text.Length; i++)
            {
                filp_label.Text = text.Substring(0, i + 1);
                Thread.Sleep(1000);
            }
            
            g = this.CreateGraphics();
            //出来一个画笔,这只笔画出来的颜色是红的
            p = new Pen(Brushes.Red);

            //创建两个点
            p1 = new Point(900, 200);
            p2 = new Point(1200, 100);

            //将两个点连起来
            g.DrawLine(p, p1, p2);
            Thread.Sleep(1000);
            g.Clear(this.BackColor);
            g.Dispose();
            int filp_off = all_file[id].filp_off;
            text = "Filp offset in the file table:" + filp_off;
            for (i = 0; i < text.Length; i++)
            {
                filp_label.Text = text.Substring(0, i + 1);
                Thread.Sleep(1000);
            }
            temLabel = (Label)this.GetType().GetField("filp_con_" + fd, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temLabel.Text = filp_add;
            Thread.Sleep(1000);
            
            
            TableLayoutPanel temTable;
            Label tempFmode;
            Label tempFflags;
            Label tempFcount;
            Label tempFinode;
            Label tempFpos;
            Label tempFrepeat;
            int num = all_file[id].file_table_num;
            for (i = 0; i < num; i++)
            {
                temLabel = (Label)this.GetType().GetField("file_table_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                temTable = (TableLayoutPanel)this.GetType().GetField("file_table_con_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFmode = (Label)this.GetType().GetField("f_mode_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFflags = (Label)this.GetType().GetField("f_flags_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFcount = (Label)this.GetType().GetField("f_count_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFinode = (Label)this.GetType().GetField("f_inode_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFpos = (Label)this.GetType().GetField("f_pos_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFrepeat = (Label)this.GetType().GetField("filp_" + i + "_repeat", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                tempFmode.Text = all_file[id].this_fti[i].f_mode + "";
                tempFmode.BackColor = Color.Cyan;
                Thread.Sleep(1000);
                tempFmode.BackColor = temColor;
                tempFflags.Text = all_file[id].this_fti[i].f_flags + "";
                tempFflags.BackColor = Color.Cyan;
                Thread.Sleep(1000);
                tempFflags.BackColor = temColor;
                tempFcount.Text = all_file[id].this_fti[i].f_count + "";
                tempFcount.BackColor = Color.Cyan;
                Thread.Sleep(1000);
                tempFcount.BackColor = temColor;
                tempFinode.Text = all_file[id].this_fti[i].f_inode_add;
                tempFinode.BackColor = Color.Cyan;
                Thread.Sleep(1000);
                tempFinode.BackColor = temColor;
                tempFpos.Text = all_file[id].this_fti[i].f_pos + "";
                tempFpos.BackColor = Color.Cyan;
                Thread.Sleep(1000);
                tempFpos.BackColor = temColor;
                tempFrepeat.Text = all_file[id].this_fti[i].repeat + "";
                tempFrepeat.BackColor = Color.Cyan;
                Thread.Sleep(1000);
                tempFrepeat.BackColor = temColor;
                temLabel.BackColor = temColor;
            }
            
            //debug
            //int fd = all_file[id].fd;
            //Graphics g;
            //Pen p;
            //Point p1;
            //Point p2;
            //Label temLabel;
            //Color temColor;
            //int i = 0;
            //debug
            

            temLabel = (Label)this.GetType().GetField("i_mode_label" , System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].i_mode + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("i_uid_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].i_uid + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("i_size_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].i_size + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("i_mtime_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].i_mtime + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("i_gid_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].i_gid + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("i_nlinks_label", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].i_nlinks + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            for (i = 0; i < 8; i++)
            {
                temLabel = (Label)this.GetType().GetField("data_label_" + i, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                temColor = temLabel.BackColor;
                temLabel.BackColor = Color.Cyan;
                Graphics g_ = this.CreateGraphics();
                //出来一个画笔,这只笔画出来的颜色是红的
                Pen p_ = new Pen(Brushes.Red);

                //创建两个点
                Point p1_ = new Point(1205 + 19 * i, 280);
                Point p2_ = new Point(895, 447);
                g_.DrawLine(p_, p1_, p2_);
                temLabel.Text = all_file[id].i_zone[i] + "";
                Thread.Sleep(1000);
 
                g_.Clear(this.BackColor);
                g_.Dispose();
                temLabel.BackColor = temColor;
            }
            g = this.CreateGraphics();
            //出来一个画笔,这只笔画出来的颜色是红的
            p = new Pen(Brushes.Red);

            //创建两个点
            p1 = new Point(750, 313);
            p2 = new Point(80 + 150 * fd, 520);

            //将两个点连起来
            g.DrawLine(p, p1, p2);
            Thread.Sleep(1000);
            g.Clear(this.BackColor);
            g.Dispose();

            temLabel = (Label)this.GetType().GetField("f_mode_" + fd, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].f_mode + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("f_flags_" + fd, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].f_flags+ "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("f_count_" + fd, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].f_count + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("f_inode_" + fd, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].f_inode_add + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            g = this.CreateGraphics();
            //出来一个画笔,这只笔画出来的颜色是红的
            p = new Pen(Brushes.Red);

            //创建两个点
            p1 = new Point(40, 709);
            p2 = new Point(80 + 150 * fd, 520);

            //将两个点连起来
            g.DrawLine(p, p1, p2);
            Thread.Sleep(1000);
            g.Clear(this.BackColor);
            g.Dispose();

            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].filp_off + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;

            temLabel = (Label)this.GetType().GetField("f_pos_" + fd, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            temColor = temLabel.BackColor;
            temLabel.BackColor = Color.Cyan;
            temLabel.Text = all_file[id].f_pos + "";
            Thread.Sleep(1000);
            temLabel.BackColor = temColor;
        }
        private void Task(int id)
        {
            
            int pid = all_file[id].pid;
            int state = all_file[id].state;
            if (current != pid)
            {
                task_running.Enabled = false;
                string text = "Task\nPID=" + pid;
                for (int i = 0; i < text.Length; i++)
                {
                    task_label.Text = text.Substring(0, i + 1);
                    Thread.Sleep(1000);
                }
                if (state == 0)
                {
                    task_running.Enabled = true;
                    Thread.Sleep(1000);
                }
                current = pid;
            }
                 
            new_file(id);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void start_ana()
        {
            for (int i = 0; i < all_file_num; i++)
            {
                Task(i);
                Thread.Sleep(1000);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            ThreadStart thStart = new ThreadStart(start_ana);//threadStart委托 
            thread = new Thread(thStart);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private void tableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label102_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            if (!cnt_state)
            {
                cnt_state = true;
                button7.Text = "继续";
                thread.Suspend();

            }
            else
            {
                cnt_state = false;
                button7.Text = "暂停";
                thread.Resume();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

          
            Form2 f = new Form2();
            f.Show();
        }
    }
}
