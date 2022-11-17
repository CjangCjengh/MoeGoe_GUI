using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoeGoe_GUI
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();

            DEFAULTS = new ExDictionary<string, ExList<string>>(() => { return new ExList<string>(5); });

            if (File.Exists("MoeGoe_GUI.config"))
            {
                foreach (string line in File.ReadLines("MoeGoe_GUI.config"))
                {
                    string[] split = line.Split('>');
                    DEFAULTS.Add(split[0], new ExList<string>(split[1].Split('|'), 5));
                }
            }

            if (!DEFAULTS.ContainsKey("D1"))
                DEFAULTS["D1"].Add("[ZH][ZH]");
            if (!DEFAULTS.ContainsKey("D2"))
                DEFAULTS["D2"].Add("[JA][JA]");
            if (!DEFAULTS.ContainsKey("D3"))
                DEFAULTS["D3"].Add("[KO][KO]");
            if (!DEFAULTS.ContainsKey("D4"))
                DEFAULTS["D4"].Add("[SA][SA]");
            if (!DEFAULTS.ContainsKey("D5"))
                DEFAULTS["D5"].Add("[EN][EN]");
            if (!DEFAULTS.ContainsKey("D6"))
                DEFAULTS["D6"].Add("[SH][SH]");
            if (!DEFAULTS.ContainsKey("D7"))
                DEFAULTS["D7"].Add("[GD][GD]");
            if (!DEFAULTS.ContainsKey("D8"))
                DEFAULTS["D8"].Add("[WZ][WZ]");
            if (!DEFAULTS.ContainsKey("D9"))
                DEFAULTS["D9"].Add("[SZ][SZ]");
            if (!DEFAULTS.ContainsKey("D0"))
                DEFAULTS["D0"].Add("[TH][TH]");

            LENGTHSCALE = 1;
            NOISESCALE = 0.667M;
            NOISESCALEW = 0.8M;
            F0SCALE = 1;

            SPEAKERS = new List<string>();
            SYMBOLS = new List<string>();
            SPEAKERIDDICT = new Dictionary<ComboBox, Dictionary<int, int>>();
            isSeeking = false;

            if (DEFAULTS.ContainsKey("EXEPATHS"))
            {
                EXEPATH = EXEPath.Text = DEFAULTS["EXEPATHS"].Next();
                modelControl.Enabled = true;
            }
        }

        private CommandLine cmd;

        private readonly ExDictionary<string, ExList<string>> DEFAULTS;

        private string EXEPATH;
        private string MODELPATH;
        private string CONFIGPATH;
        private string HUBERTPATH;
        private string W2V2PATH;
        private string SAVEPATH;

        private string ORIGINPATH;
        private string EMOTIONPATH;

        private decimal LENGTHSCALE;
        private decimal NOISESCALE;
        private decimal NOISESCALEW;
        private decimal F0SCALE;

        private readonly List<string> SPEAKERS;
        private readonly List<string> SYMBOLS;

        private readonly Dictionary<ComboBox, Dictionary<int, int>> SPEAKERIDDICT;

        private bool USEF0;

        private bool isSeeking;
        private SoundPlayer player;

        private void ClearAll()
        {
            ClearVITS();
            ClearHubertVITS();
            ClearW2V2VITS();
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
            consoleBox.Clear();
            textBox.Clear();
            speakerBox.Items.Clear();
            speakerBox.Text = "";
            LENGTHSCALE = 1;
            NOISESCALE = 0.667M;
            NOISESCALEW = 0.8M;
            originPath.Clear();
            ORIGINPATH = null;
            originBox.Items.Clear();
            originBox.Text = "";
            targetBox.Items.Clear();
            targetBox.Text = "";
            modeControl.Enabled = false;
            ClearSavePanel();
        }

        private void ClearHubertVITS()
        {
            HModelPath.Clear();
            MODELPATH = null;
            HConfigPath.Clear();
            CONFIGPATH = null;
            hubertPath.Clear();
            HUBERTPATH = null;
            ClearHubertMode();
        }

        private void ClearHubertMode()
        {
            consoleBox.Clear();
            HOriginPath.Clear();
            ORIGINPATH = null;
            HOpenOrigin.Enabled = false;
            HOriginPath.Enabled = false;
            HTargetBox.Items.Clear();
            HTargetBox.Text = "";
            LENGTHSCALE = 1;
            NOISESCALE = 0.1M;
            NOISESCALEW = 0.1M;
            F0SCALE = 1;
            HOriginBox.Items.Clear();
            HOriginBox.Text = "";
            HTargetBox2.Items.Clear();
            HTargetBox2.Text = "";
            HVCControl.Enabled = false;
            ClearSavePanel();
        }

        private void ClearW2V2VITS()
        {
            WModelPath.Clear();
            MODELPATH = null;
            WConfigPath.Clear();
            CONFIGPATH = null;
            W2V2Path.Clear();
            W2V2PATH = null;
            ClearW2V2Mode();
        }

        private void ClearW2V2Mode()
        {
            consoleBox.Clear();
            emotionPath.Clear();
            EMOTIONPATH = null;
            WTextBox.Clear();
            WSpeakerBox.Items.Clear();
            WSpeakerBox.Text = "";
            LENGTHSCALE = 1;
            NOISESCALE = 0.667M;
            NOISESCALEW = 0.8M;
            WOriginPath.Clear();
            ORIGINPATH = null;
            WOriginBox.Items.Clear();
            WOriginBox.Text = "";
            WTargetBox.Items.Clear();
            WTargetBox.Text = "";
            WModeControl.Enabled = false;
            ClearSavePanel();
        }

        private void ClearSavePanel()
        {
            savePath.Clear();
            SAVEPATH = null;
            savePanel.Enabled = false;
            resaveButton.Enabled = false;
            isSeeking = false;
            player = null;
            deleteButton.Enabled = false;
            playButton.Enabled = false;
            stopButton.Enabled = false;
        }

        private void OpenEXE_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "|MoeGoe.exe"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ClearAll();
                DEFAULTS["EXEPATHS"].Add(EXEPATH = EXEPath.Text = ofd.FileName);
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
                    DEFAULTS["EXEPATHS"].Add(EXEPATH = EXEPath.Text);
                    modelControl.Enabled = true;
                }
        }

        private void GetModelBox(out TextBox box, out string key, out Action check)
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    key = "MODELPATHS";
                    box = modelPath;
                    check = CheckModel;
                    break;
                case 1:
                    key = "HMODELPATHS";
                    box = HModelPath;
                    check = CheckModelHubert;
                    break;
                case 2:
                    key = "WMODELPATHS";
                    box = WModelPath;
                    check = CheckModelW2V2;
                    break;
                default:
                    key = null;
                    box = null;
                    check = null;
                    break;
            }
        }

        private void OpenModel_Click(object sender, EventArgs e)
        {
            GetModelBox(out TextBox box, out string key, out Action check);
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|*.pth|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DEFAULTS[key].Add(MODELPATH = box.Text = ofd.FileName);
                check();
            }
            ofd.Dispose();
        }

        private void ModelPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            GetModelBox(out TextBox box, out string key, out Action check);
            if (e.KeyChar == '\r')
                if (!File.Exists(box.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DEFAULTS[key].Add(MODELPATH = box.Text);
                    check();
                }
        }

        private void GetConfigBox(out TextBox box, out string key, out Action check)
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    key = "CONFIGPATHS";
                    box = configPath;
                    check = CheckModel;
                    break;
                case 1:
                    key = "HCONFIGPATHS";
                    box = HConfigPath;
                    check = CheckModelHubert;
                    break;
                case 2:
                    key = "WCONFIGPATHS";
                    box = WConfigPath;
                    check = CheckModelW2V2;
                    break;
                default:
                    key = null;
                    box = null;
                    check = null;
                    break;
            }
        }

        private void OpenConfig_Click(object sender, EventArgs e)
        {
            GetConfigBox(out TextBox box, out string key, out Action check);
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "配置文件|*.json"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DEFAULTS[key].Add(CONFIGPATH = box.Text = ofd.FileName);
                check();
            }
            ofd.Dispose();
        }

        private void ConfigPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            GetConfigBox(out TextBox box, out string key, out Action check);
            if (e.KeyChar == '\r')
                if (!File.Exists(box.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DEFAULTS[key].Add(CONFIGPATH = box.Text);
                    check();
                }
        }

        private void CheckModel()
        {
            ClearMode();
            if (MODELPATH != null && CONFIGPATH != null)
                InitializeSpeakers();
        }

        private bool LoadJsonList(string json, string key, Action<string> action)
        {
            Match match = Regex.Match(json,
                $"\"{key}\"\\s*:\\s*\\[((?:\\s*\"(?:(?:\\\\.)|[^\\\\\"])*\"\\s*,?\\s*)*)\\]");
            if (match.Success)
            {
                MatchCollection matches = Regex.Matches(match.Groups[1].Value,
                    "\"((?:(?:\\\\.)|[^\\\\\"])*)\"");
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        string speaker = Regex.Unescape(matches[i].Groups[1].Value);
                        action(speaker);
                    }
                }
                return true;
            }
            return false;
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
            SPEAKERS.Clear();
            if (!LoadJsonList(json, "speakers", SPEAKERS.Add))
            {
                match = Regex.Match(json,
                "\"n_speakers\"\\s*:\\s*(\\d+)");
                int nspeakers = 0;
                if (match.Success)
                    nspeakers = int.Parse(match.Groups[1].Value);
                if (nspeakers == 0)
                    SPEAKERS.Add("0");
                else
                    for (int i = 0; i < nspeakers; i++)
                        SPEAKERS.Add(i.ToString());
            }
            AddSpeakers();
            SYMBOLS.Clear();
            LoadJsonList(json, "symbols", SYMBOLS.Add);
            GetStart();
        }

        private void AddSpeakers()
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    SelectSpeakers(speakerBox);
                    SelectSpeakers(originBox);
                    SelectSpeakers(targetBox);
                    break;
                case 1:
                    SelectSpeakers(HTargetBox);
                    SelectSpeakers(HTargetBox2);
                    SelectSpeakers(HOriginBox);
                    break;
                case 2:
                    SelectSpeakers(WSpeakerBox);
                    SelectSpeakers(WOriginBox);
                    SelectSpeakers(WTargetBox);
                    break;
            }
        }

        private void GetStart()
        {
            cmd = new CommandLine();
            cmd.OutputHandler += Cmd_OutputHandler;
            cmd.Write($"\"{EXEPATH}\" --escape");
            cmd.Write(MODELPATH);
            cmd.Write(CONFIGPATH);
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    modeControl.Enabled = true;
                    break;
                case 1:
                    cmd.Write(HUBERTPATH);
                    HOpenOrigin.Enabled = true;
                    HOriginPath.Enabled = true;
                    HVCControl.Enabled = true;
                    break;
                case 2:
                    cmd.Write(W2V2PATH);
                    WModeControl.Enabled = true;
                    break;
            }
            savePanel.Enabled = true;
        }

        private void Cmd_OutputHandler(CommandLine sender, string e)
        {
            Invoke(new Action(() =>
            {
                try { consoleBox.Text += Regex.Unescape(e); } catch { consoleBox.Text += e; }
            }));
        }

        private TextBox GetOriginBox()
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    return originPath;
                case 1:
                    return HOriginPath;
                case 2:
                    return WOriginPath;
                default:
                    return null;
            }
        }

        private void OpenOrigin_Click(object sender, EventArgs e)
        {
            TextBox box = GetOriginBox();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "音频文件|*.wav;*.mp3;*.ogg;*.opus"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                DEFAULTS["AUDIOPATHS"].Add(ORIGINPATH = box.Text = ofd.FileName);
            ofd.Dispose();
        }

        private void OriginPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '\r')
                if (!File.Exists(textBox.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    DEFAULTS["AUDIOPATHS"].Add(ORIGINPATH = textBox.Text);
        }

        private bool IsFilled()
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    switch (modeControl.SelectedIndex)
                    {
                        case 0:
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
                        case 1:
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
                    return false;
                case 1:
                    if (ORIGINPATH == null)
                    {
                        MessageBox.Show("请指定原音频！", "",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    switch (HVCControl.SelectedIndex)
                    {
                        case 0:
                            if (HTargetBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择目标说话人！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            return true;
                        case 1:
                            if (HOriginBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择原说话人！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            if (HTargetBox2.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择目标说话人！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            return true;
                    }
                    return false;
                case 2:
                    switch (WModeControl.SelectedIndex)
                    {
                        case 0:
                            if (EMOTIONPATH == null)
                            {
                                MessageBox.Show("请指定情感参考！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (WTextBox.Text.Length == 0)
                            {
                                MessageBox.Show("请输入文本！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            if (WSpeakerBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择说话人！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            return true;
                        case 1:
                            if (ORIGINPATH == null)
                            {
                                MessageBox.Show("请指定原音频！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            if (WOriginBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择原说话人！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            if (WTargetBox.SelectedIndex == -1)
                            {
                                MessageBox.Show("请选择目标说话人！", "",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "音频文件|*.wav"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DEFAULTS["SAVEPATHS"].Add(SAVEPATH = savePath.Text = sfd.FileName);
                SaveAudio();
            }
            sfd.Dispose();
        }

        private void SaveAudio()
        {
            if (!IsFilled())
                return;
            string directory = Path.GetDirectoryName(SAVEPATH);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            deleteButton.Enabled = false;
            playButton.Enabled = false;
            stopButton.Enabled = false;
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    switch (modeControl.SelectedIndex)
                    {
                        case 0:
                            TTS(textBox.Text, GetSelectedID(speakerBox));
                            break;
                        case 1:
                            VC(GetSelectedID(originBox), GetSelectedID(targetBox));
                            break;
                    }
                    cmd.Write(SAVEPATH);
                    break;
                case 1:
                    switch (HVCControl.SelectedIndex)
                    {
                        case 0:
                            cmd.Write(ORIGINPATH);
                            cmd.Write(GetSelectedID(HTargetBox).ToString());
                            if (USEF0)
                                cmd.Write($"[LENGTH={LENGTHSCALE}][NOISE={NOISESCALE}][NOISEW={NOISESCALEW}][F0={F0SCALE}]{SAVEPATH}");
                            else
                                cmd.Write($"[LENGTH={LENGTHSCALE}][NOISE={NOISESCALE}][NOISEW={NOISESCALEW}]{SAVEPATH}");
                            break;
                        case 1:
                            cmd.Write($"[VC]");
                            cmd.Write(ORIGINPATH);
                            cmd.Write(GetSelectedID(HOriginBox).ToString());
                            cmd.Write(GetSelectedID(HTargetBox2).ToString());
                            cmd.Write(SAVEPATH);
                            break;
                    }
                    break;
                case 2:
                    switch (WModeControl.SelectedIndex)
                    {
                        case 0:
                            TTS(WTextBox.Text, GetSelectedID(WSpeakerBox));
                            cmd.Write(EMOTIONPATH);
                            break;
                        case 1:
                            VC(GetSelectedID(WOriginBox), GetSelectedID(WTargetBox));
                            break;
                    }
                    cmd.Write(SAVEPATH);
                    break;
            }
            cmd.Write("y");
            resaveButton.Enabled = true;
            isSeeking = true;
            Task.Run(() =>
            {
                while (true)
                {
                    if (!isSeeking)
                        return;
                    if (File.Exists(SAVEPATH))
                    {
                        Invoke(new Action(() =>
                        {
                            deleteButton.Enabled = true;
                            playButton.Enabled = true;
                            stopButton.Enabled = true;
                            isSeeking = false;
                        }));
                        return;
                    }
                    Thread.Sleep(500);
                }
            });
        }

        private int GetSelectedID(ComboBox box)
        {
            return SPEAKERIDDICT[box][box.SelectedIndex];
        }

        private void TTS(string text, int speaker)
        {
            cmd.Write("t");
            cmd.Write($"[LENGTH={LENGTHSCALE}][NOISE={NOISESCALE}][NOISEW={NOISESCALEW}]{Regex.Replace(text, @"\r?\n", " ")}");
            cmd.Write(speaker.ToString());
        }

        private void VC(int origin, int target)
        {
            cmd.Write("v");
            cmd.Write(ORIGINPATH);
            cmd.Write(origin.ToString());
            cmd.Write(target.ToString());
        }

        private TextBox GetTextBox()
        {
            switch (modelControl.SelectedIndex)
            {
                case 0:
                    return textBox;
                case 2:
                    return WTextBox;
                default:
                    return null;
            }
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            TextBox box = GetTextBox();
            CleanWin win = new CleanWin(box, cmd);
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
                case 2:
                    ClearW2V2VITS();
                    break;
            }
        }

        private void CheckModelHubert()
        {
            ClearHubertMode();
            if (MODELPATH != null && CONFIGPATH != null && HUBERTPATH != null)
                InitializeSpeakers();
        }

        private void HOpenHubert_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|*.pt|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DEFAULTS["HUBERTPATHS"].Add(HUBERTPATH = hubertPath.Text = ofd.FileName);
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
                    DEFAULTS["HUBERTPATHS"].Add(HUBERTPATH = hubertPath.Text);
                    CheckModelHubert();
                }
        }

        private void HAdvancedControl_Click(object sender, EventArgs e)
        {
            HAdvancedWin win = new HAdvancedWin(GetParameters, SetParameters, USEF0);
            win.ShowDialog();
            win.Dispose();
        }

        private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("MoeGoe_GUI.config"))
                foreach (KeyValuePair<string, ExList<string>> pair in DEFAULTS)
                    sw.WriteLine(pair.Key + ">" + pair.Value);
        }

        private void GetHistory(TextBox box, string key, KeyEventArgs e)
        {
            if (!DEFAULTS.TryGetValue(key, out ExList<string> list))
                return;
            if (e.KeyCode == Keys.Up)
                box.Text = list.Next();
            else if (e.KeyCode == Keys.Down)
                box.Text = list.Previous();
        }

        private void EXEPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(EXEPath, "EXEPATHS", e);
        }

        private void ModelPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(modelPath, "MODELPATHS", e);
        }

        private void ConfigPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(configPath, "CONFIGPATHS", e);
        }

        private void HModelPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(HModelPath, "HMODELPATHS", e);
        }

        private void HConfigPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(HConfigPath, "HCONFIGPATHS", e);
        }

        private void HubertPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(hubertPath, "HUBERTPATHS", e);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (!e.Control)
                return;
            switch (e.KeyCode)
            {
                case Keys.D1:
                    box.Paste(DEFAULTS["D1"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D2:
                    box.Paste(DEFAULTS["D2"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D3:
                    box.Paste(DEFAULTS["D3"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D4:
                    box.Paste(DEFAULTS["D4"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D5:
                    box.Paste(DEFAULTS["D5"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D6:
                    box.Paste(DEFAULTS["D6"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D7:
                    box.Paste(DEFAULTS["D7"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D8:
                    box.Paste(DEFAULTS["D8"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D9:
                    box.Paste(DEFAULTS["D9"].Next());
                    box.SelectionStart -= 4;
                    break;
                case Keys.D0:
                    box.Paste(DEFAULTS["D0"].Next());
                    box.SelectionStart -= 4;
                    break;
            }
        }

        private void SymbolsButton_Click(object sender, EventArgs e)
        {
            TextBox box = GetTextBox();
            SymbolsWin win = new SymbolsWin(SYMBOLS, box);
            win.Show();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                player = new SoundPlayer(SAVEPATH);
                player.Play();
            }
            catch
            {
                MessageBox.Show("文件不存在！", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ResaveButton_Click(object sender, EventArgs e)
        {
            SaveAudio();
        }

        private void SavePath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DEFAULTS["SAVEPATHS"].Add(SAVEPATH = savePath.Text);
                SaveAudio();
            }
        }

        private void SavePath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(savePath, "SAVEPATHS", e);
        }

        private void OriginPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(originPath, "AUDIOPATHS", e);
        }

        private void HOriginPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(HOriginPath, "AUDIOPATHS", e);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            playButton.Enabled = false;
            stopButton.Enabled = false;
            File.Delete(SAVEPATH);
        }

        private void CheckModelW2V2()
        {
            ClearW2V2Mode();
            if (MODELPATH != null && CONFIGPATH != null && W2V2PATH != null)
                InitializeSpeakers();
        }

        private void W2V2Model_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|model.onnx"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DEFAULTS["W2V2PATHS"].Add(W2V2PATH = W2V2Path.Text = ofd.FileName);
                CheckModelW2V2();
            }
            ofd.Dispose();
        }

        private void W2V2Path_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(W2V2Path.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DEFAULTS["W2V2PATHS"].Add(W2V2PATH = W2V2Path.Text);
                    CheckModelW2V2();
                }
        }

        private void WModelPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(WModelPath, "WMODELPATHS", e);
        }

        private void WConfigPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(WConfigPath, "WCONFIGPATHS", e);
        }

        private void W2V2Path_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(W2V2Path, "W2V2PATHS", e);
        }

        private void EmotionButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "数据文件|*.npy|音频文件|*.wav;*.mp3;*.ogg;*.opus"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                DEFAULTS["EMOTIONPATHS"].Add(EMOTIONPATH = emotionPath.Text = ofd.FileName);
            ofd.Dispose();
        }

        private void EmotionPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(emotionPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    DEFAULTS["EMOTIONPATHS"].Add(EMOTIONPATH = emotionPath.Text);
        }

        private void EmotionPath_KeyDown(object sender, KeyEventArgs e)
        {
            GetHistory(emotionPath, "EMOTIONPATHS", e);
        }

        private void SearchSpeakers(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                SelectSpeakers(sender as ComboBox);
        }

        private void SelectSpeakers(ComboBox box)
        {
            box.Items.Clear();
            if (!SPEAKERIDDICT.ContainsKey(box))
                SPEAKERIDDICT.Add(box, new Dictionary<int, int>());
            else
                SPEAKERIDDICT[box].Clear();
            for (int i = 0; i < SPEAKERS.Count; i++)
                if (SPEAKERS[i].Contains(box.Text))
                {
                    box.Items.Add(SPEAKERS[i]);
                    SPEAKERIDDICT[box].Add(box.Items.Count - 1, i);
                }
        }
    }

    public class ExList<T>
    {
        private int index;
        private readonly int range;
        private readonly List<T> list;

        public int Index
        {
            get { return index; }
            set
            {
                int limit = range < list.Count ? range : list.Count;
                if (value < 0)
                    index = limit - 1;
                else if (value >= limit)
                    index = 0;
                else
                    index = value;
            }
        }

        public ExList(int range)
        {
            index = -1;
            this.range = range;
            list = new List<T>();
        }

        public ExList(IEnumerable<T> collection, int range)
        {
            index = -1;
            this.range = range;
            list = new List<T>(collection);
        }

        public void Add(T item)
        {
            list.Remove(item);
            list.Insert(0, item);
        }

        public T Next()
        {
            ++Index;
            return list[Index];
        }

        public T Previous()
        {
            --Index;
            return list[Index];
        }

        public override string ToString()
        {
            int limit = range < list.Count ? range : list.Count;
            return string.Join("|", list.GetRange(0, limit));
        }
    }

    public class ExDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private readonly Func<TValue> generator;

        public ExDictionary(Func<TValue> generator)
        {
            this.generator = generator;
        }

        public new TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                    Add(key, generator());
                return base[key];
            }
        }
    }
}
