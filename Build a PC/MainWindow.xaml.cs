using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Build_a_PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private double _totalCost;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ListenToPartItems();
            LoadData();
            PresetSlider.Value = 3;
        }

        private void LoadData()
        {
            var nl = Environment.NewLine;
            var caseDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "海盗船 400Q",
                        Description = "更好地静音，冷却，防尘设计。尺寸较小：425mm（长）x 215mm（宽）x 464mm（高）。",
                        Brand = "海盗船 Corsair",
                        PartType = PartType.Case,
                        Price = 699,
                        Recommendation = RecommendationType.Best,
                        Pid = "5272478",
                        ImageUrl = "pack://application:,,,/Resources/case_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "先马 黑洞3",
                        Description = "静音机箱。530（长）*232（宽）*466（高）mm。",
                        Brand = "先马",
                        PartType = PartType.Case,
                        Price = 399,
                        Recommendation = RecommendationType.Ok,
                        Pid = "2917029",
                        ImageUrl = "pack://application:,,,/Resources/case_ok.jpg"
                    }
                }
            };
            Case.LoadParts(caseDict);
            Case.SetTitle("机箱", $"机箱主要考虑扩展性、防尘、机箱内通风设计、静音和外观。{nl}扩展性对于普通办公不重要。防尘和通风关系到硬件的寿命，静音和外观关系到工作环境。");
            Case.ShowChoices(new[] {false, true, true});

            var cpuDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "i5 8600K",
                        Description = "8代酷睿 6核6线程 3.6GHz。",
                        Brand = "英特尔 Intel",
                        PartType = PartType.CPU,
                        Price = 1799,
                        Recommendation = RecommendationType.Best,
                        Pid = "5008425",
                        ImageUrl = "pack://application:,,,/Resources/cpu_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "i5 8500",
                        Description = "8代酷睿 6核6线程 3.0GHz。",
                        Brand = "英特尔 Intel",
                        PartType = PartType.CPU,
                        Price = 1399,
                        Recommendation = RecommendationType.Ok,
                        Pid = "6405178",
                        ImageUrl = "pack://application:,,,/Resources/cpu_ok.jpg"
                    }
                },
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "i5 8400",
                        Description = "8代酷睿 6核6线程 2.8GHz",
                        Brand = "英特尔 Intel",
                        PartType = PartType.CPU,
                        Price = 1349,
                        Recommendation = RecommendationType.Basic,
                        Pid = "5008397",
                        ImageUrl = "pack://application:,,,/Resources/cpu_basic.jpg"
                    }
                }
            };
            CPU.LoadParts(cpuDict);
            CPU.SetTitle("中央处理器 CPU",
                $"CPU主要看核心数、频率（xxxGHz）。数字越大越好。{nl}我觉得我们没有必要买“最佳”（i5 8600K），普通办公i5 8500“够用”就好，i5 8400“基本”也能凑合。");

            var mbDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "华擎 Z370 Pro4",
                        Description = "一块够用且便宜的主板。",
                        Brand = "华擎",
                        PartType = PartType.Motherboard,
                        Price = 759,
                        Recommendation = RecommendationType.Ok,
                        Pid = "5221329",
                        ImageUrl = "pack://application:,,,/Resources/mb_ok.jpg"
                    }
                }
            };
            MotherBoard.LoadParts(mbDict);
            MotherBoard.SetTitle("主板", "主板的选择主要是考虑CPU的适配，电路保护功能，扩展功能和电脑配件的接口。我们不需要太复杂的功能，也不需要安装任何配件，选一块便宜够用的就好。");
            MotherBoard.ShowChoices(new[] {false, true, false});

            var gpuDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "无",
                        PartType = PartType.Graphic_Card,
                        Price = 0,
                        Recommendation = RecommendationType.Basic,
                    }
                }
            };
            GPU.LoadParts(gpuDict, RecommendationType.Basic);
            GPU.SetTitle("显卡", $"除非是玩游戏，或者有工作需要（图像处理、复杂3D模型设计等），不需要显卡。");
            GPU.ShowChoices(new[] {false, false, false});

            var hdd1Dict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "三星 固态硬盘 960 EVO 500G M.2 NVMe",
                        Description = "500G容量，读取速度 3200 MB/秒，写入速度 1800 MB/秒。",
                        Brand = "三星 Samsung",
                        PartType = PartType.Hard_Drive_1,
                        Price = 1399,
                        Recommendation = RecommendationType.Best,
                        Pid = "4173878",
                        ImageUrl = "pack://application:,,,/Resources/hdd1_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "三星 固态硬盘 860 EVO 500G",
                        Description = "500G容量，读取速度 550 MB/秒，写入速度 520 MB/秒。",
                        Brand = "三星 Samsung",
                        PartType = PartType.Hard_Drive_1,
                        Price = 899,
                        Recommendation = RecommendationType.Ok,
                        Pid = "6212482",
                        ImageUrl = "pack://application:,,,/Resources/hdd1_ok.jpg"
                    }
                },
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "希捷 固态机械混合硬盘 1TB SATA3 ST1000DX002",
                        Description = "1TB （1000GB）容量，读取写入速度 150MB/秒。",
                        Brand = "希捷 SEAGATE",
                        PartType = PartType.Hard_Drive_1,
                        Price = 569,
                        Recommendation = RecommendationType.Basic,
                        Pid = "3052959",
                        ImageUrl = "pack://application:,,,/Resources/hdd1_basic.jpg"
                    }
                }
            };
            HDD1.LoadParts(hdd1Dict);
            HDD1.SetTitle("硬盘 1",
                $"硬盘主要看容量和读写速度。500G-1T的容量一般来说够用了，除非要存大量的音乐和电影。{nl}读写速度对于电脑的性能提升很重要。现代电脑的卡顿多半是因为读写速度低造成的。");

            var hdd2Dict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "无",
                        PartType = PartType.Hard_Drive_2,
                        Price = 0,
                        Recommendation = RecommendationType.Basic,
                    }
                }
            };
            HDD2.LoadParts(hdd2Dict, RecommendationType.Basic);
            HDD2.SetTitle("硬盘 2",
                $"如果以后第一块硬盘放满了，再买就是。");
            HDD2.ShowChoices(new[] {false, false, false});

            var ramDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "金士顿 Fury系列 DDR4 2400 16G (8GBx2)",
                        Description = "最便宜的双通道16G 2400内存条。",
                        Brand = "金士顿 Kingston",
                        PartType = PartType.Ram,
                        Price = 1399,
                        Recommendation = RecommendationType.Best,
                        Pid = "2121093",
                        ImageUrl = "pack://application:,,,/Resources/ram_best.jpg"
                    }
                },
{
                    RecommendationType.Ok, new Part
                    {
                        Name = "金士顿 Fury系列 DDR4 2400 8G (4GBx2)",
                        Description = "最便宜的双通道8G 2400内存条。",
                        Brand = "金士顿 Kingston",
                        PartType = PartType.Ram,
                        Price = 799,
                        Recommendation = RecommendationType.Ok,
                        Pid = "2551101",
                        ImageUrl = "pack://application:,,,/Resources/ram_ok.jpg"
                    }
                }
            };
            Ram.LoadParts(ramDict);
            Ram.SetTitle("内存条", $"网页开太多了，卡？程序开太多了，卡？多半是内存不够引起的。16G的内存目前看来，在家用的情形下绝对够用了。8G的内存也基本够用，记得别开太多网页或程序就好。");
            Ram.ShowChoices(new[] { false, true, true });

            var psuDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "酷冷至尊 额定550W V550",
                        Description = "550W 功率，金牌认证，5年质保",
                        Brand = "酷冷至尊",
                        PartType = PartType.PSU,
                        Price = 499,
                        Recommendation = RecommendationType.Best,
                        Pid = "1852502",
                        ImageUrl = "pack://application:,,,/Resources/psu_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "安钛克 VP500 额定500W",
                        Description = "500W 功率，铜牌认证，3年质保",
                        Brand = "安钛克",
                        PartType = PartType.PSU,
                        Price = 329,
                        Recommendation = RecommendationType.Ok,
                        Pid = "7408072",
                        ImageUrl = "pack://application:,,,/Resources/psu_ok.jpg"
                    }
                }

            };
            PSU.LoadParts(psuDict);
            PSU.SetTitle("电源",
                $"电源主要考虑额定功率，认证级别和质保。我们不会用到很大的功率，所以500W足矣。认证级别有铜、金、铂金等。认证级别越高，电压越稳定，电流越纯净，对设备的损害也越小。普通用用的话，铜牌就够用了。");
            PSU.ShowChoices(new[] {false, true, true});

            var mouseDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "罗技 MX Master 2S 无线蓝牙",
                        Description =
                            "无线或蓝牙连接，厚实，手感舒适。可同时连接并控制3台计算机，有9个扩展键。罗技暗区激光技术-业界最高精确度，可在任何桌面（包括玻璃）上使用。我在英国用的就是这个。",
                        Brand = "罗技 Logitech",
                        PartType = PartType.Mouse,
                        Price = 599,
                        Recommendation = RecommendationType.Best,
                        Pid = "4294661",
                        ImageUrl = "pack://application:,,,/Resources/mouse_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "罗技 M720 无线蓝牙",
                        Description = "无线或蓝牙连接。可同时连接并控制3台计算机，有8个扩展键。罗技高精度光电。",
                        Brand = "罗技 Logitech",
                        PartType = PartType.Mouse,
                        Price = 299,
                        Recommendation = RecommendationType.Ok,
                        Pid = "15942194222",
                        ImageUrl = "pack://application:,,,/Resources/mouse_ok.jpg"
                    }
                },
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "罗技 M100r 光电鼠标",
                        Description = "普通的消费类鼠标。",
                        Brand = "罗技 Logitech",
                        PartType = PartType.Mouse,
                        Price = 45,
                        Recommendation = RecommendationType.Basic,
                        Pid = "692919",
                        ImageUrl = "pack://application:,,,/Resources/mouse_basic.jpg"
                    }
                }
            };
            Mouse.LoadParts(mouseDict, RecommendationType.Best);
            Mouse.SetTitle("鼠标",
                $"鼠标是最重要的配件之一了。主要考虑舒适度和精确性。罗技的鼠标是我用过最舒适的鼠标了，所以我就没考虑其他品牌。鼠标的精确性越高，鼠标用起来越顺手，指针操作起来越不费力。罗技的暗区激光技术是目前最好的。{nl}强烈推荐罗技的MX Master 2S。");

            var keyboardDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "樱桃 MX-BOARD 2.0 G80-3800 白色 茶轴",
                        Description = "机械键盘，茶轴，特色是安静，反馈适当，适合打字。",
                        Brand = "樱桃 Cherry",
                        PartType = PartType.Keyboard,
                        Price = 459,
                        Recommendation = RecommendationType.Best,
                        Pid = "2300073",
                        ImageUrl = "pack://application:,,,/Resources/keyboard_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "罗技 K840 机械键盘",
                        Description = "机械键盘，Romer G轴，特色是声音清脆，键程短（轻按一下就出字，没习惯前容易按错）。我用的是这个。",
                        Brand = "罗技 Logitech",
                        PartType = PartType.Keyboard,
                        Price = 349,
                        Recommendation = RecommendationType.Ok,
                        Pid = "5025970",
                        ImageUrl = "pack://application:,,,/Resources/keyboard_ok.jpg"
                    }
                },
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "罗技 K120 薄膜键盘",
                        Description = "薄膜键盘，没什么特别的。",
                        Brand = "罗技 Logitech",
                        PartType = PartType.Keyboard,
                        Price = 45,
                        Recommendation = RecommendationType.Basic,
                        Pid = "262214",
                        ImageUrl = "pack://application:,,,/Resources/keyboard_basic.jpg"
                    }
                }
            };
            Keyboard.LoadParts(keyboardDict, RecommendationType.Best);
            Keyboard.SetTitle("键盘",
                $"主要是打字的舒适度。笔记本电脑上的键盘都是薄膜键盘，按起来手感一般，没什么反馈，但是便宜。机械键盘是老科技，那些噼里啪啦响的老键盘都是机械键盘。薄膜键盘的出现一定程度上导致机械键盘濒临灭绝。但是最近大家开始还念机械键盘打字的手感，所以机械键盘又开始流行了。");

            var monitorDict = new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "戴尔 U2518DR",
                        Description = "25寸，高清2K分辨率屏（像素颗粒比较小），色彩精准，可调节视角、高度，屏幕可90度旋转。",
                        Brand = "戴尔 Dell",
                        PartType = PartType.Monitor,
                        Price = 2199,
                        Recommendation = RecommendationType.Best,
                        Pid = "4396371",
                        ImageUrl = "pack://application:,,,/Resources/monitor_best.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "Dell U2417H",
                        Description = "24寸，普通 1920 x 1080 分辨率屏，色彩精准，窄边框，可调节视角、高度，屏幕可90度旋转。",
                        Brand = "戴尔 Dell",
                        PartType = PartType.Monitor,
                        Price = 1499,
                        Recommendation = RecommendationType.Ok,
                        Pid = "2316993",
                        ImageUrl = "pack://application:,,,/Resources/monitor_ok.jpg"
                    }
                },
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "松人",
                        Description = "24寸，普通 1920 x 1080 分辨率屏，色彩一般。",
                        Brand = "松人",
                        PartType = PartType.Monitor,
                        Price = 579,
                        Recommendation = RecommendationType.Basic,
                        Pid = "26654068529",
                        ImageUrl = "pack://application:,,,/Resources/monitor_basic.jpg"
                    }
                }
            };
            Monitor.LoadParts(monitorDict);
            Monitor.SetTitle("显示器",
                $"显示器主要考虑接口数量、分辨率、色彩精准度、屏幕的可调节度和外观。这三款显示器的接口数量都没问题，差异主要在其他几项上：{nl}分辨率越高，字和图片越清晰。色彩精准度在编辑照片时很重要，对于看图看视频也有一定的影响。屏幕的调节度有2点可以考虑：1. 俯仰角，2. 屏幕旋转。俯仰角关系到观看的舒适度，屏幕旋转是指可以把屏幕旋转90度，横屏变为竖屏，对看文字、编辑文字有用。{nl}我个人不赞成购买太差的屏幕，因为用电脑时99%的时间都在看屏幕。");

            Accessory1.LoadParts(new Dictionary<RecommendationType, Part>
            {
                {
                    RecommendationType.Best, new Part
                    {
                        Name = "TP-Link TL-WDN8280 3200M双频无线PCI-E网卡",
                        Description =
                            "名称中的3200M是指传输速度是3200M/s。如果家里的网络没有那么快的话，买1900M甚至1300M的也够了。不过需注意的是，1300M和1900M的信号接受能力也有所下降。如果房间离路由器远的话，要注意一下。",
                        Brand = "普联 TP-Link",
                        PartType = PartType.Accessory,
                        Price = 579,
                        Recommendation = RecommendationType.Best,
                        Pid = "26654068529",
                        ImageUrl = "pack://application:,,,/Resources/wirelesscard.jpg"
                    }
                },
                {
                    RecommendationType.Ok, new Part
                    {
                        Name = "TP-LINK TL-PA1000套装 1000M 千兆有线电力线",
                        Description =
                            "工作原理是把家里的电线当成网线传输网络数据。优点是速度比无线网络稳定，缺点是电力线布置不恰当的话，会影响信号稳定性。我建议，如果路由器离电脑远（15米开外还隔着墙）的话，买这个比较好。否则，无线网卡省心.",
                        Brand = "普联 TP-Link",
                        PartType = PartType.Accessory,
                        Price = 579,
                        Recommendation = RecommendationType.Ok,
                        Pid = "6627771",
                        ImageUrl = "pack://application:,,,/Resources/powerline.jpg"
                    }
                },
                {
                    RecommendationType.Basic, new Part
                    {
                        Name = "无",
                        PartType = PartType.Accessory,
                        Price = 0,
                        Recommendation = RecommendationType.Basic
                    }
                }
            }, RecommendationType.Basic);
            Accessory1.SetTitle("网络", $"如果拉网线太麻烦的话，可以买一张无线网卡，也可以买有线电力线。");
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var pi in FindVisualChildren<PartItem>(this))
            {
                var part = pi.CurrentPartItem.Part;
                if (part?.Pid == null || part.Pid == "")
                    continue;
                Process.Start(new ProcessStartInfo(part.UrlAddToBasket));
                await Task.Delay(1000);
            }
        }

        private void PresetSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch ((int)e.NewValue)
            {
                case 0:
                    ApplyPresetItemChoices(new[] { 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0 });
                    PresetDesc.Text = "预设一：简单便宜。什么都选最便宜的。";
                    break;
                case 1:
                    ApplyPresetItemChoices(new[] { 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0 });
                    PresetDesc.Text = "预设二：提升舒适。硬件和键盘都选最基本的，显示器和鼠标选第二档次的。";
                    break;
                case 2:
                    ApplyPresetItemChoices(new[] { 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0 });
                    PresetDesc.Text = "预设三：舒适和性能。CPU和硬盘都选第二档次的。外设（显示器，鼠标和键盘）都选第二档次的。";
                    break;
                case 3:
                    ApplyPresetItemChoices(new[] { 1, 0, 1, 0, 1, 1, 0, 0, 2, 2, 2, 0 });
                    PresetDesc.Text = "预设四：注重舒适。硬件都选最基本的，外设（显示器，鼠标和键盘）都选最佳的。";
                    break;
                case 4:
                    ApplyPresetItemChoices(new[] { 1, 1, 1, 0, 1, 1, 1, 0, 2, 2, 2, 0 });
                    PresetDesc.Text = "预设五：注重舒适，提升性能。CPU和硬盘都选第二档次的；外设（显示器，鼠标和键盘）都选最佳的。";
                    break;
                case 5:
                    ApplyPresetItemChoices(new[] { 2, 1, 1, 0, 2, 1, 1, 0, 2, 2, 2, 0 });
                    PresetDesc.Text = "预设六：注重舒适和性能。在预设五的基础上，内存加倍，机箱也换个更好的。";
                    break;
                case 6:
                    ApplyPresetItemChoices(new[] { 2, 2, 1, 0, 2, 2, 2, 0, 2, 2, 2, 0 });
                    PresetDesc.Text = "预设七：奢侈。全选最好的。";
                    break;
            }
        }

        private void ApplyPresetItemChoices(int[] choices)
        {
            var controls = FindVisualChildren<PartItem>(this).ToArray();
            for (var i = 0; i < choices.Length; i++)
            {
                controls[i].SelectChoice((RecommendationType)choices[i], true);
            }
        }

        public void ListenToPartItems()
        {
            foreach (var pi in FindVisualChildren<PartItem>(this))
                pi.PropertyChanged += PartItemOnPropertyChanged;
        }

        private void PartItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedPrice")
            {
                var s = sender as PartItem;
                _totalCost -= s.PreviousPrice;
                _totalCost += s.SelectedPrice;
                TotalCost.Text = _totalCost.ToString("F2");
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T variable)
                {
                    yield return variable;
                }

                foreach (var childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }

        private void UnCollapseButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var i in FindVisualChildren<Expander>(this))
                i.IsExpanded = true;
        }

        private void CollapseButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var i in FindVisualChildren<Expander>(this))
                i.IsExpanded = false;
        }
    }
}

//= new Dictionary<RecommendationType, Part>
//{
//{
//    RecommendationType.Best, new Part
//    {
//        Name = "",
//        Description = "",
//        Brand = "",
//        PartType = PartType.,
//        Price = ,
//        Recommendation = RecommendationType.Best,
//        Pid = "",
//        ImageUrl = "pack://application:,,,/Resources/_best.jpg"
//    }
//},
//{
//RecommendationType.Ok, new Part
//{
//    Name = "",
//    Description = "",
//    Brand = "",
//    PartType = PartType.,
//    Price = ,
//    Recommendation = RecommendationType.Ok,
//    Pid = "",
//    ImageUrl = "pack://application:,,,/Resources/_ok.jpg"
//}
//},
//{
//RecommendationType.Basic, new Part
//{
//    Name = "",
//    Description = "",
//    Brand = "",
//    PartType = PartType.,
//    Price = ,
//    Recommendation = RecommendationType.Basic,
//    Pid = "",
//    ImageUrl = "pack://application:,,,/Resources/_basic.jpg"
//}
//}
//};
//.LoadParts();
//.SetTitle("", $"");
//.ShowChoices(new [] {true, true, true});