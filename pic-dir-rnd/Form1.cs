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

namespace pic_dir_rnd
{
    public partial class Form1 : Form
    {
        FileInfo[] fil;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                textBox2.Text = folderBrowserDialog2.SelectedPath;
        }

        /// <summary>
        /// 获取垃圾图片文件
        /// </summary>
        /// <param name="path">垃圾图片目录路径</param>
        void getJunkFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            fil = dir.GetFiles();

            var length = fil.Length;

            int index = new Random().Next(0, length);
            MessageBox.Show(index.ToString());
            MessageBox.Show(fil[index].FullName);
        }

        /// <summary>
        /// 随机复制垃圾图片
        /// </summary>
        /// <param name="path">要复制垃圾图片的目录</param>
        void copyRandomFiles(string path)
        {
            //开始工作
            //随机要添加的垃圾数量
            int num = new Random().Next(6, 777);

            for(int i = 0; i < num; ++i)
            {
                var length = fil.Length;
                //随机文件名
                string rndFileName = listBox1.GetItemText(listBox1.Items[new Random().Next(0, listBox1.Items.Count)]) + 
                    listBox2.GetItemText(listBox2.Items[new Random().Next(0, listBox2.Items.Count)]) + 
                    listBox3.GetItemText(listBox3.Items[new Random().Next(0, listBox3.Items.Count)]) +
                    (new Random().Next(0, 100) % 2 == 0 ? ".png" : ".jpg");
                //如果不存在就丢一个过去
                if(!File.Exists(Path.Combine(path,rndFileName)))
                    File.Copy(fil[new Random().Next(0, length)].FullName,Path.Combine(path, rndFileName));
            }

            DirectoryInfo dir = new DirectoryInfo(path);

            DirectoryInfo[] dii = dir.GetDirectories();

            foreach (DirectoryInfo d in dii)

            {
                copyRandomFiles(d.FullName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(textBox1.Text))
            {
                MessageBox.Show("请选择正确的目标目录！");
                return;
            }

            if (!Directory.Exists(textBox2.Text))
            {
                MessageBox.Show("请选择正确的垃圾图片目录！");
                return;
            }

            getJunkFiles(textBox2.Text);

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;

            copyRandomFiles(textBox1.Text);

            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
        }
    }
}
