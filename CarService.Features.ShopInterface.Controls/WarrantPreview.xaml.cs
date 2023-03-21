using CarService.Features.ShopInterface.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CarService.Features.ShopInterface.Controls
{
    /// <summary>
    /// Interaction logic for WarrantPreview.xaml
    /// </summary>
    public partial class WarrantPreview : UserControl, INotifyPropertyChanged
    {
        private static AnimationClock? displaySubjectClock;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Warrant Warrant
        {
            get { return (Warrant)GetValue(WarrantProperty); }
            set { SetValue(WarrantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WarrantProperty =
            DependencyProperty.Register(nameof(Warrant), typeof(Warrant), typeof(WarrantPreview), new PropertyMetadata(propertyChangedCallback: new PropertyChangedCallback(OnWarrantChanged)));


        public Func<Warrant, Task> RollbackAction
        {
            get { return (Func<Warrant, Task>)GetValue(RollbackActionProperty); }
            set { SetValue(RollbackActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviousStepCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RollbackActionProperty =
            DependencyProperty.Register(nameof(RollbackAction), typeof(Func<Warrant, Task>), typeof(WarrantPreview));


        public Func<Warrant, Task> AdvanceAction
        {
            get { return (Func<Warrant, Task>)GetValue(AdvanceActionProperty); }
            set { SetValue(AdvanceActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdvanceAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdvanceActionProperty =
            DependencyProperty.Register("AdvanceAction", typeof(Func<Warrant, Task>), typeof(WarrantPreview));

        public string? WarrantDeadline => Warrant?.IsUrgent == true ? "Hitni nalog" : Warrant?.DeadLine.ToString();
        public string LabelContent => (displaySubjectClock?.State == true ? Warrant?.Subject : WarrantDeadline) ?? string.Empty;
        public bool DisplayRollbackButton => Warrant?.CurrentStep?.BackTransition != null;
        public bool DisplayAdvanceButton => Warrant?.CurrentStep?.ForwardTransition != null;
        public bool PlayUpdatedAnimation { get; private set; }

        public WarrantPreview()
        {
            InitializeComponent();

            displaySubjectClock ??= new AnimationClock(5);

            displaySubjectClock.Tick += (s, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelContent"));
            };
        }

        [RelayCommand]
        public void Initialize()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LabelContent)));
        }

        [RelayCommand]
        public async Task Rollback()
        {
            await RollbackAction.Invoke(Warrant);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayRollbackButton)));
        }

        [RelayCommand]
        public async Task Advance()
        {
            await AdvanceAction.Invoke(Warrant);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayAdvanceButton)));
        }

        public void OnExternalWarrantUpdate()
        {
            PlayUpdatedAnimation = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayUpdatedAnimation)));
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            PlayUpdatedAnimation = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayUpdatedAnimation)));
        }

        private static void OnWarrantChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Warrant newWarrant)
            {
                WarrantPreview warrantPreview = (WarrantPreview)depObj;
                Warrant oldWarrant = (Warrant)e.OldValue;

                if (oldWarrant != null) oldWarrant.OnExternalUpdate -= warrantPreview.OnExternalWarrantUpdate;
                if (newWarrant != null) newWarrant.OnExternalUpdate += warrantPreview.OnExternalWarrantUpdate;
            }
        }

        private class AnimationClock : DispatcherTimer
        {
            public bool State { get; private set; } = true;

            public AnimationClock(int intervalInSeconds)
            {
                Interval = new TimeSpan(0, 0, intervalInSeconds);

                Tick += (s, e) => State = !State;

                Start();
            }
        }
    }
}
