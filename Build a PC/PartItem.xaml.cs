using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Build_a_PC.Annotations;

namespace Build_a_PC
{
    /// <summary>
    /// Interaction logic for PartItem.xaml
    /// </summary>
    public partial class PartItem : INotifyPropertyChanged
    {

        public Dictionary<RecommendationType,Part> Parts { get; set; }

        public double SelectedPrice
        {
            get => _selectedPrice;
            private set
            {
                if (value.Equals(_selectedPrice)) return;
                PreviousPrice = _selectedPrice;
                _selectedPrice = value;
                OnPropertyChanged();
            }
        }

        public double PreviousPrice { get; private set; }

        public readonly CurrentPart CurrentPartItem;

        private double _selectedPrice;

        public PartItem()
        {
            InitializeComponent();
            CurrentPartItem = new CurrentPart();
            DataContext = CurrentPartItem;
        }

        public void SetTitle(string title, string desc)
        {
            CurrentPartItem.Title = title;
            CurrentPartItem.Desc = desc;
        }

        public void LoadParts(IDictionary<RecommendationType, Part> parts, RecommendationType selectedType = RecommendationType.Ok)
        {
            Parts =new Dictionary<RecommendationType, Part>(parts);

            foreach (var p in Parts.Values)
            {
                CurrentPartItem.PriceList.Add(p.Recommendation, p.Price);
            }

            CurrentPartItem.Part = Parts[selectedType];

            switch (selectedType)
            {
                case RecommendationType.Basic:
                    BasicRadioButton.IsChecked = true;
                    break;
                case RecommendationType.Ok:
                    OkRadioButton.IsChecked = true;
                    break;
                case RecommendationType.Best:
                    BestRadioButton.IsChecked = true;
                    break;
                default:
                    break;
            }

            SelectedPrice = CurrentPartItem.CurrentPrice;

            // update price
            //var dictVals = Parts.Values.ToList();
            //var taskIEnumerable = from p in Parts.Values select p.GetRealTimePriceAsync();
            //var taskList = taskIEnumerable.ToList();
            //while (taskList.Count > 0)
            //{
            //    // Identify the first task that completes.  
            //    var firstFinishedTask = await Task.WhenAny(taskList);

            //    // ***Remove the selected task from the list so that you don't  
            //    // process it more than once.  

            //    // Await the completed task.  
            //    var realPrice = await firstFinishedTask;
            //    var idx = taskList.IndexOf(firstFinishedTask);
            //    dictVals[idx].Price = realPrice;
            //    dictVals.RemoveAt(idx);
            //    taskList.Remove(firstFinishedTask);
            //}
        }

        public void ShowChoices(bool[] choices)
        {
            BasicRadioButton.Visibility = choices[0] ? Visibility.Visible : Visibility.Collapsed;
            OkRadioButton.Visibility = choices[1] ? Visibility.Visible : Visibility.Collapsed;
            BestRadioButton.Visibility = choices[2] ? Visibility.Visible : Visibility.Collapsed;

            if (choices.Any(x => x)) return;
            ContentBorder1.Visibility = Visibility.Collapsed;
            ContentBorder2.Visibility = Visibility.Collapsed;
        }
        
        private void RadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Uid)
            {
                case "basic":
                    SelectChoice(RecommendationType.Basic);
                    break;
                case "ok":
                    SelectChoice(RecommendationType.Ok);
                    break;
                case "best":
                    SelectChoice(RecommendationType.Best);
                    break;
            }
        }

        public void SelectChoice(RecommendationType selectedType, bool changeRadioButton = false)
        {
            CurrentPartItem.Part = Parts[selectedType];

            if (!changeRadioButton) return;

            switch (selectedType)
            {
                case RecommendationType.Basic:
                    BasicRadioButton.IsChecked = true;
                    //OkRadioButton.IsChecked = false;
                    //BestRadioButton.IsChecked = false;
                    break;
                case RecommendationType.Ok:
                    BasicRadioButton.IsChecked = false;
                    OkRadioButton.IsChecked = true;
                    BestRadioButton.IsChecked = false;
                    break;
                case RecommendationType.Best:
                    BasicRadioButton.IsChecked = false;
                    OkRadioButton.IsChecked = false;
                    BestRadioButton.IsChecked = true;
                    break;
            }
            SelectedPrice = CurrentPartItem.CurrentPrice;
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        public class CurrentPart : INotifyPropertyChanged
        {
            private Part _part;
            
            private double _currentPrice;

            public string Title
            {
                get => _title;
                set
                {
                    if (value == _title) return;
                    _title = value;
                    OnPropertyChanged();
                }
            }

            public string Desc
            {
                get => _desc;
                set
                {
                    if (value == _desc) return;
                    _desc = value;
                    OnPropertyChanged();
                }
            }

            public double CurrentPrice
            {
                get => _currentPrice;
                set
                {
                    if (value.Equals(_currentPrice)) return;
                    _currentPrice = value;
                    OnPropertyChanged(nameof(PriceDiffBasic));
                    OnPropertyChanged(nameof(PriceDiffOk));
                    OnPropertyChanged(nameof(PriceDiffBest));
                }
            }

            public Dictionary<RecommendationType, double> PriceList = new Dictionary<RecommendationType, double>();
            private string _title;
            private string _desc;

            public double PriceDiffBasicVal => PriceList.ContainsKey(RecommendationType.Basic) ? (PriceList[RecommendationType.Basic] - CurrentPrice) : 0;
            public double PriceDiffOkVal => PriceList.ContainsKey(RecommendationType.Ok) ? (PriceList[RecommendationType.Ok] - CurrentPrice) : 0;
            public double PriceDiffBestVal => PriceList.ContainsKey(RecommendationType.Best) ? (PriceList[RecommendationType.Best] - CurrentPrice) : 0;

            public string PriceDiffBasic => Math.Abs(PriceDiffBasicVal) > 0.0001 ? PriceDiffBasicVal.ToString("+#.##;-#.##;+0") : "";
            public string PriceDiffOk => Math.Abs(PriceDiffOkVal) > 0.0001 ? (PriceList[RecommendationType.Ok] - CurrentPrice).ToString("+#.##;-#.##;+0") : "";
            public string PriceDiffBest => Math.Abs(PriceDiffBestVal) > 0.0001 ? (PriceList[RecommendationType.Best] - CurrentPrice).ToString("+#.##;-#.##;+0") : "";
            public Brush PriceDiffBrushBasic => PriceDiffBasicVal > 0 ? Brushes.Green : Brushes.Crimson;
            public Brush PriceDiffBrushOk => PriceDiffBasicVal > 0 ? Brushes.Green : Brushes.Crimson;
            public Brush PriceDiffBrushBest => PriceDiffBasicVal > 0 ? Brushes.Green : Brushes.Crimson;


            public Part Part
            {
                get => _part;
                set
                {
                    if (Equals(value, _part)) return;
                    _part = value;
                    CurrentPrice = value.Price;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
