using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();
            LENGTHSCALE = 1;
            NOISESCALE = 0.667M;
            NOISESCALEW = 0.8M;
            F0SCALE = 1;
            string p1 = Environment.CurrentDirectory + @"\MoeGoe.exe";
            string p2 = Environment.CurrentDirectory + @"\MoeGoe\MoeGoe.exe";
            if (File.Exists(p1))
            {
                EXEPATH = EXEPath.Text = p1;
                modelControl.Enabled = true;
            }
            else if (File.Exists(p2))
            {
                EXEPATH = EXEPath.Text = p2;
                modelControl.Enabled = true;
            }
        }

        private CommandLine cmd;

        private string EXEPATH;
        private string MODELPATH;
        private string CONFIGPATH;
        private string HUBERTPATH;
        private string SAVEPATH;

        private string ORIGINPATH;

        private decimal LENGTHSCALE;
        private decimal NOISESCALE;
        private decimal NOISESCALEW;
        private decimal F0SCALE;

        private bool USEF0;

        private void ClearAll()
        {
            ClearVITS();
            ClearHubertVITS();
        }

        private void ClearVITS()
        {
            modelPath.Clear();
            MODELPATH = null;
            configPath.Clear();
            CONFIGPATH = null;
            ClearMode();
        }

        private void ClearMode()
        {
            textBox.Clear();
            speakerBox.Items.Clear();
            LENGTHSCALE = 1;
            NOISESCALE = 0.667M;
            NOISESCALEW = 0.8M;
            originPath.Clear();
            ORIGINPATH = null;
            originBox.Items.Clear();
            targetBox.Items.Clear();
            modeControl.Enabled = false;
            savePath.Clear();
            savePanel.Enabled = false;
        }

        private void ClearHubertVITS()
        {
            HModelPath.Clear();
            MODELPATH = null;
            HConfigPath.Clear();
            CONFIGPATH = null;
            hubertPath.Clear();
            HUBERTPATH = null;
            ClearHubertVC();
        }

        private void ClearHubertVC()
        {
            HOriginPath.Clear();
            ORIGINPATH = null;
            HTargetBox.Items.Clear();
            LENGTHSCALE = 1;
            NOISESCALE = 0.1M;
            NOISESCALEW = 0.1M;
            F0SCALE = 1;
            HVCPanel.Enabled = false;
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
                modelControl.Enabled = true;
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
                    modelControl.Enabled = true;
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
                InitializeSpeakers();
        }

        private void InitializeSpeakers()
        {
            string json = File.ReadAllText(CONFIGPATH);
            Match useF0 = Regex.Match(json, "\"use_f0\"\\s*:\\s*([A-Za-z]+)");
            if (useF0.Success)
                USEF0 = bool.Parse(useF0.Groups[1].Value);
            else
                USEF0 = false;
            Match match = Regex.Match(json,
                "\"speakers\"\\s*:\\s*\\[((?:\\s*\"(?:(?:\\\\.)|[^\\\\\"])*\"\\s*,?\\s*)*)\\]");
            if (match.Success)
            {
                MatchCollection matches = Regex.Matches(match.Groups[1].Value,
                    "\"((?:(?:\\\\.)|[^\\\\\"])*)\"");
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        string speaker = Regex.Unescape(matches[i].Groups[1].Value);
                        AddSpeaker(speaker);
                    }
                }
            }
            else
            {
                match = Regex.Match(json,
                "\"n_speakers\"\\s*:\\s*(\\d+)");
                int nspeakers = 0;
                if (match.Success)
                    nspeakers = int.Parse(match.Groups[1].Value);
                if (nspeakers == 0)
                    AddSpeaker("0");
                else
                    for (int i = 0; i < nspeakers; i++)
                        AddSpeaker(i.ToString());
            }
            GetStart();
        }

        private void AddSpeaker(string speaker)
        {
            if (modelControl.SelectedIndex == 0)
            {
                speakerBox.Items.Add(speaker);
                originBox.Items.Add(speaker);
                targetBox.Items.Add(speaker);
            }
            else if (modelControl.SelectedIndex == 1)
                HTargetBox.Items.Add(speaker);
        }

        private void GetStart()
        {
            cmd = new CommandLine();
            cmd.OutputHandler += Cmd_OutputHandler;
            cmd.Write("\"" + EXEPATH + "\"");
            cmd.Write(MODELPATH);
            cmd.Write(CONFIGPATH);
            if (modelControl.SelectedIndex == 0)
                modeControl.Enabled = true;
            else if (modelControl.SelectedIndex == 1)
            {
                cmd.Write(HUBERTPATH);
                HVCPanel.Enabled = true;
            }
            savePanel.Enabled = true;
        }

        private void Cmd_OutputHandler(CommandLine sender, string e)
        {
            Invoke(new Action(() => consoleBox.Text += e));
        }

        private void OpenOrigin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "音频文件|*.wav;*.mp3;*.ogg;*.opus"
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
            if (modelControl.SelectedIndex == 0)
            {
                if (modeControl.SelectedIndex == 0)
                {
                    if (textBox.Text.Length == 0)
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
                    if (originBox.SelectedIndex == -1)
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
            }
            else if (modelControl.SelectedIndex == 1)
            {
                if (ORIGINPATH == null)
                {
                    MessageBox.Show("请指定原音频！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (HTargetBox.SelectedIndex == -1)
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
                if (modelControl.SelectedIndex == 0)
                {
                    if (modeControl.SelectedIndex == 0)
                        TTS();
                    else if (modeControl.SelectedIndex == 1)
                        VC();
                    cmd.Write(SAVEPATH);
                }
                else if (modelControl.SelectedIndex == 1)
                {
                    cmd.Write(ORIGINPATH);
                    cmd.Write(HTargetBox.SelectedIndex.ToString());
                    if (USEF0)
                        cmd.Write($"[LENGTH={LENGTHSCALE}][NOISE={NOISESCALE}][NOISEW={NOISESCALEW}][F0={F0SCALE}]{SAVEPATH}");
                    else
                        cmd.Write($"[LENGTH={LENGTHSCALE}][NOISE={NOISESCALE}][NOISEW={NOISESCALEW}]{SAVEPATH}");
                }
                cmd.Write("y");
            }
            sfd.Dispose();
        }

        private void TTS()
        {
            cmd.Write("t");
            cmd.Write($"[LENGTH={LENGTHSCALE}][NOISE={NOISESCALE}][NOISEW={NOISESCALEW}]{Regex.Replace(textBox.Text, @"\r?\n", " ")}");
            cmd.Write(speakerBox.SelectedIndex.ToString());
        }

        private void VC()
        {
            cmd.Write("v");
            cmd.Write(ORIGINPATH);
            cmd.Write(originBox.SelectedIndex.ToString());
            cmd.Write(targetBox.SelectedIndex.ToString());
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            CleanWin win = new CleanWin(textBox, cmd);
            cmd.OutputHandler -= Cmd_OutputHandler;
            win.ShowDialog();
            cmd.OutputHandler += Cmd_OutputHandler;
            win.Dispose();
        }

        private decimal[] GetParameters()
        {
            return new decimal[] { LENGTHSCALE, NOISESCALE, NOISESCALEW, F0SCALE };
        }

        private void SetParameters(decimal lengthScale, decimal noiseScale, decimal noiseScaleW)
        {
            LENGTHSCALE = lengthScale;
            NOISESCALE = noiseScale;
            NOISESCALEW = noiseScaleW;
        }

        private void SetParameters(decimal lengthScale, decimal noiseScale, decimal noiseScaleW, decimal f0Scale)
        {
            LENGTHSCALE = lengthScale;
            NOISESCALE = noiseScale;
            NOISESCALEW = noiseScaleW;
            F0SCALE = f0Scale;
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            AdvancedWin win = new AdvancedWin(GetParameters, SetParameters);
            win.ShowDialog();
            win.Dispose();
        }

        private void ModelControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    ClearVITS();
                    break;
                case 1:
                    ClearHubertVITS();
                    break;
            }
        }

        private void CheckModelHubert()
        {
            ClearHubertVC();
            if (MODELPATH != null && CONFIGPATH != null && HUBERTPATH != null)
                InitializeSpeakers();
        }

        private void HOpenModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|*.pth|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MODELPATH = HModelPath.Text = ofd.FileName;
                CheckModelHubert();
            }
            ofd.Dispose();
        }

        private void HModelPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(HModelPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MODELPATH = HModelPath.Text;
                    CheckModelHubert();
                }
        }

        private void HOpenConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "配置文件|*.json"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CONFIGPATH = HConfigPath.Text = ofd.FileName;
                CheckModelHubert();
            }
            ofd.Dispose();
        }

        private void HConfigPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(HConfigPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    CONFIGPATH = HConfigPath.Text;
                    CheckModelHubert();
                }
        }

        private void HOpenHubert_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|*.pt|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                HUBERTPATH = hubertPath.Text = ofd.FileName;
                CheckModelHubert();
            }
            ofd.Dispose();
        }

        private void HubertPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(HConfigPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HUBERTPATH = hubertPath.Text;
                    CheckModelHubert();
                }
        }

        private void HOpenOrigin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "音频文件|*.wav;*.mp3;*.ogg;*.opus"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                ORIGINPATH = HOriginPath.Text = ofd.FileName;
            ofd.Dispose();
        }

        private void HOriginPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(HOriginPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    ORIGINPATH = HOriginPath.Text;
        }

        private void HAdvancedControl_Click(object sender, EventArgs e)
        {
            HAdvancedWin win = new HAdvancedWin(GetParameters, SetParameters, USEF0);
            win.ShowDialog();
            win.Dispose();
        }
    }
}
