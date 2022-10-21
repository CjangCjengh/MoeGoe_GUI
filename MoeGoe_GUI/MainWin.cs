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

            LENGTHSCALE = 1;
            NOISESCALE = 0.667M;
            NOISESCALEW = 0.8M;
            F0SCALE = 1;

            SYMBOLS = new List<string>();
            SHOWLOG = true;
            isSeeking = false;
        }

        private CommandLine cmd;

        private readonly ExDictionary<string, ExList<string>> DEFAULTS;

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

        private readonly List<string> SYMBOLS;
        private bool USEF0;
        private bool SHOWLOG;

        private bool isSeeking;
        private SoundPlayer player;

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
            playButton.Enabled = false;
            stopButton.Enabled = false;
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

        private void OpenModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "模型文件|*.pth|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DEFAULTS["MODELPATHS"].Add(MODELPATH = modelPath.Text = ofd.FileName);
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
                    DEFAULTS["MODELPATHS"].Add(MODELPATH = modelPath.Text);
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
                DEFAULTS["CONFIGPATHS"].Add(CONFIGPATH = configPath.Text = ofd.FileName);
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
                    DEFAULTS["CONFIGPATHS"].Add(CONFIGPATH = configPath.Text);
                    CheckModel();
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
            if (!LoadJsonList(json, "speakers", AddSpeaker))
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
            if (speakerBox.Items.Count > 100)
                SHOWLOG = false;
            SYMBOLS.Clear();
            LoadJsonList(json, "symbols", SYMBOLS.Add);
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
            if (SHOWLOG)
                cmd.OutputHandler += Cmd_OutputHandler;
            cmd.Write($"\"{EXEPATH}\" --escape");
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
            Invoke(new Action(() =>
            {
                try { consoleBox.Text += Regex.Unescape(e); } catch { consoleBox.Text += e; }
            }));
        }

        private void OpenOrigin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "音频文件|*.wav;*.mp3;*.ogg;*.opus"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                DEFAULTS["AUDIOPATHS"].Add(ORIGINPATH = originPath.Text = ofd.FileName);
            ofd.Dispose();
        }

        private void OriginPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(originPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    DEFAULTS["AUDIOPATHS"].Add(ORIGINPATH = originPath.Text);
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
            playButton.Enabled = false;
            stopButton.Enabled = false;
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
            if (SHOWLOG)
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
                DEFAULTS["HMODELPATHS"].Add(MODELPATH = HModelPath.Text = ofd.FileName);
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
                    DEFAULTS["HMODELPATHS"].Add(MODELPATH = HModelPath.Text);
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
                DEFAULTS["HCONFIGPATHS"].Add(CONFIGPATH = HConfigPath.Text = ofd.FileName);
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
                    DEFAULTS["HCONFIGPATHS"].Add(CONFIGPATH = HConfigPath.Text);
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

        private void HOpenOrigin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "音频文件|*.wav;*.mp3;*.ogg;*.opus"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                DEFAULTS["AUDIOPATHS"].Add(ORIGINPATH = HOriginPath.Text = ofd.FileName);
            ofd.Dispose();
        }

        private void HOriginPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (!File.Exists(HOriginPath.Text))
                    MessageBox.Show("文件不存在！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    DEFAULTS["AUDIOPATHS"].Add(ORIGINPATH = HOriginPath.Text);
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
            if (!e.Control)
                return;
            switch (e.KeyCode)
            {
                case Keys.D1:
                    textBox.Paste(DEFAULTS["D1"].Next());
                    textBox.SelectionStart -= 4;
                    break;
                case Keys.D2:
                    textBox.Paste(DEFAULTS["D2"].Next());
                    textBox.SelectionStart -= 4;
                    break;
                case Keys.D3:
                    textBox.Paste(DEFAULTS["D3"].Next());
                    textBox.SelectionStart -= 4;
                    break;
                case Keys.D4:
                    textBox.Paste(DEFAULTS["D4"].Next());
                    textBox.SelectionStart -= 4;
                    break;
                case Keys.D5:
                    textBox.Paste(DEFAULTS["D5"].Next());
                    textBox.SelectionStart -= 4;
                    break;
            }
        }

        private void SymbolsButton_Click(object sender, EventArgs e)
        {
            SymbolsWin win = new SymbolsWin(SYMBOLS, textBox);
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
            index = 0;
            this.range = range;
            list = new List<T>();
        }

        public ExList(IEnumerable<T> collection, int range)
        {
            index = 0;
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
            return list[Index++];
        }

        public T Previous()
        {
            return list[Index--];
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
