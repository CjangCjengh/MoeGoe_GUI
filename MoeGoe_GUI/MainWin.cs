using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MoeGoe_GUI
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();
        }

        private CommandLine cmd;

        private string EXEPATH;
        private string MODELPATH;
        private string CONFIGPATH;
        private string SAVEPATH;

        private string ORIGINPATH;

        private void ClearAll()
        {
            modelPath.Clear();
            configPath.Clear();
            modelPanel.Enabled = false;
            ClearMode();
        }

        private void ClearMode()
        {
            textBox.Clear();
            speakerBox.Items.Clear();
            originPath.Clear();
            ORIGINPATH = null;
            originBox.Items.Clear();
            targetBox.Items.Clear();
            modeControl.Enabled = false;
            savePath.Clear();
            savePanel.Enabled = false;
        }

        private void OpenEXE_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "可执行文件|*.exe"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ClearAll();
                EXEPATH = EXEPath.Text = ofd.FileName;
                modelPanel.Enabled = true;
            }
            ofd.Dispose();
        }

        private void EXEPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(EXEPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    ClearAll();
                    EXEPATH = EXEPath.Text;
                    modelPanel.Enabled = true;
                }
        }

        private void OpenModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|*.pth|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MODELPATH = modelPath.Text = ofd.FileName;
                CheckModel();
            }
            ofd.Dispose();
        }

        private void ModelPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(modelPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MODELPATH = modelPath.Text;
                    CheckModel();
                }
        }

        private void OpenConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "配置文件|*.json"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CONFIGPATH = configPath.Text = ofd.FileName;
                CheckModel();
            }
            ofd.Dispose();
        }

        private void ConfigPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(configPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    CONFIGPATH = configPath.Text;
                    CheckModel();
                }
        }

        private void CheckModel()
        {
            ClearMode();
            if (MODELPATH != null && CONFIGPATH != null)
                Initialize_mode();
        }

        private void Initialize_mode()
        {
            string json = File.ReadAllText(CONFIGPATH);
            Match match = Regex.Match(json,
                "\"speakers\"\\s*:\\s*\\[((?:\\s*\"(?:(?:\\\\.)|[^\\\\\"])*\"\\s*,?\\s*)*)\\]");
            if (match.Success)
            {
                MatchCollection matches = Regex.Matches(match.Groups[1].Value,
                    "\"((?:(?:\\\\.)|[^\\\\\"])*)\"");
                if(matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        string speaker = Regex.Unescape(matches[i].Groups[1].Value);
                        speakerBox.Items.Add(speaker);
                        originBox.Items.Add(speaker);
                        targetBox.Items.Add(speaker);
                    }
                    GetStart();
                    return;
                }
            }
            MessageBox.Show("读取失败！", "",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GetStart()
        {
            cmd = new CommandLine();
            cmd.OutputHandler += Cmd_OutputHandler;
            cmd.Write(EXEPATH);
            cmd.Write(MODELPATH);
            cmd.Write(CONFIGPATH);
            modeControl.Enabled = true;
            savePanel.Enabled = true;
        }

        private void Cmd_OutputHandler(CommandLine sender, string e)
        {
            Invoke(new Action(()=> consoleBox.Text += e));
        }

        private void OpenOrigin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "音频文件|*.wav"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                ORIGINPATH = originPath.Text = ofd.FileName;
            ofd.Dispose();
        }

        private void OriginPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(originPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    ORIGINPATH = originPath.Text;
        }

        private bool IsFilled()
        {
            if (modeControl.SelectedIndex == 0)
            {
                if(textBox.Text.Length == 0)
                {
                    MessageBox.Show("请输入文本！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (speakerBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择说话人！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            else if (modeControl.SelectedIndex == 1)
            {
                if (ORIGINPATH == null)
                {
                    MessageBox.Show("请指定原音频！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if(originBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择原说话人！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (targetBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择目标说话人！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            return false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!IsFilled())
                return;
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "音频文件|*.wav"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SAVEPATH = savePath.Text = sfd.FileName;
                if (modeControl.SelectedIndex == 0)
                    TTS();
                else if (modeControl.SelectedIndex == 1)
                    VC();
                cmd.Write(SAVEPATH);
                cmd.Write("y");
            }
            sfd.Dispose();
        }

        private void TTS()
        {
            cmd.Write("t");
            cmd.Write(Regex.Replace(textBox.Text, @"\r?\n", " "));
            cmd.Write(speakerBox.SelectedIndex.ToString());
        }

        private void VC()
        {
            cmd.Write("v");
            cmd.Write(ORIGINPATH);
            cmd.Write(originBox.SelectedIndex.ToString());
            cmd.Write(targetBox.SelectedIndex.ToString());
        }
    }
}
